using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PandemicWeb.Data;
using PandemicWeb.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Linq;

namespace PandemicWeb.Controllers
{
    public class ClienteController : BaseController
    {
        [HttpGet]
        [Route("nova-conta")]
        public IActionResult Create(){
            ViewBag.LogoutVisible = "none";
            return View(new Cliente());
        }

        [HttpPost]
        [Route("nova-conta")]
        public IActionResult Create(Cliente model)
        {
            if(!ModelState.IsValid) return View(model);            
            using(ClienteData data = new ClienteData()) data.Create(model);
            if(HttpContext.Session.GetString("pedido_data")!=null){                
                SetClienteSessao(model);
                return RedirectToAction("Finalizar", "Pedido");
            }
            SetNovoUsuario(true);
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        [Route("perfil")]
        public IActionResult Update(){
            Cliente cliente = GetClienteSessao();
            if(cliente!=null)
            {
                ViewBag.LogoutVisible = "block";
                ViewBag.ShowAlert = false;
                ViewBag.Cliente = GetClienteSessao();
                ViewBag.UserName = ViewBag.Cliente.Usuario.Nome;                
                return View(GetClienteSessao());
            }else return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        [Route("perfil")]        
        public IActionResult Update(Cliente model)
        {        
            Cliente cliente = GetClienteSessao();
            if(cliente!=null)
            {            
                if(!string.IsNullOrEmpty(model.Usuario.Senha))
                {
                    int flag = 0;                
                    if(model.Usuario.Senha != GetClienteSessao().Usuario.Senha){flag++; ModelState.AddModelError("Usuario.Senha", "Senha incorreta");}
                    else if(string.IsNullOrEmpty(model.Usuario.NovaSenha)){
                        Console.WriteLine("Nova senha vazia");
                        flag++; ModelState.AddModelError("Usuario.NovaSenha", "Campo obrigatório");
                    }
                    else if(model.Usuario.NovaSenha.Length<8)
                    {flag++; ModelState.AddModelError("Usuario.NovaSenha", "A nova senha deve ter mínimo de oito caracteres");}
                    else if(model.Usuario.NovaSenha.Equals(model.Usuario.Senha))
                    {flag++; ModelState.AddModelError("Usuario.NovaSenha", "Digite uma senha diferente da senha atual");}
                    ViewBag.UserName = cliente.Usuario.Nome;
                    if(flag>0)
                    {
                        ViewBag.ShowAlert = false;
                        return View(model);
                    }    
                    else{                                        
                        cliente.Usuario.Senha = model.Usuario.NovaSenha;
                        using(UsuarioData data = new UsuarioData()) data.UpdateSenha(cliente.Usuario);
                        SetClienteSessao(cliente);
                        ViewBag.ShowAlert = true;
                        ViewBag.Mensagem = "Senha alterada com sucesso";
                        return View(cliente);
                    }
                }else{
                    if(
                        string.IsNullOrEmpty(model.Usuario.Nome) ||
                        model.Usuario.Nome.Length<3 ||
                        string.IsNullOrEmpty(model.CPF) ||
                        model.CPF.Length<14 ||
                        string.IsNullOrEmpty(model.Telefone) ||
                        model.Telefone.Length<14 ||
                        string.IsNullOrEmpty(model.Endereco) ||
                        model.Endereco.Length<3 ||
                        string.IsNullOrEmpty(model.Usuario.Email) ||
                        model.Usuario.Email.Length<7
                    )
                    {                                                 
                        ViewBag.ShowAlert = false;
                    }else{                    
                        cliente.Usuario.Nome = model.Usuario.Nome;
                        cliente.CPF = model.CPF;
                        cliente.Telefone = model.Telefone;
                        cliente.Endereco = model.Endereco;
                        cliente.Usuario.Email = model.Usuario.Email;
                        using(ClienteData data = new ClienteData()) data.Update(cliente);
                        SetClienteSessao(cliente);
                        ViewBag.ShowAlert = true;
                        ViewBag.Mensagem = "Perfil atualizado com sucesso";
                    }
                    ViewBag.UserName = cliente.Usuario.Nome;
                    return View(cliente);
                }
            }else return RedirectToAction("Index", "Login");                
        }
    }
}