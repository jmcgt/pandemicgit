using Microsoft.AspNetCore.Mvc;
using PandemicWeb.Data;
using PandemicWeb.Models;

namespace PandemicWeb.Controllers
{
    public class LoginController : BaseController
    {
        [Route("login")]
        [Route("logout")]
        //[Route("")]
        public IActionResult Index()
        {
            ViewBag.LogoutVisible = "none";
            ViewBag.NovoUsuario = IsNovoUsuario();
            ViewBag.UsuarioNotFound = false;
            SetNovoUsuario(false);
            Logout();
            return View(new Usuario());
        }

        [HttpPost]
        [Route("login")]        
        public IActionResult Index(Usuario usuario, bool NovoUsuario)
        {
            ViewBag.LogoutVisible = "none";
            ViewBag.NovoUsuario = IsNovoUsuario();
            if(string.IsNullOrEmpty(usuario.Email) || string.IsNullOrEmpty(usuario.Senha) || usuario.Senha.Length<8){
                if(string.IsNullOrEmpty(usuario.Senha)) ModelState.AddModelError("Senha", "Campo obrigatório");
                else if(usuario.Senha.Length<8) ModelState.AddModelError("Senha", "A senha deve ter mínimo de oito caracteres");
                ViewBag.UsuarioNotFound = false;
                return View(usuario);
            }  
            else{                
                using(UsuarioData data = new UsuarioData()) usuario = data.Read(usuario.Email, usuario.Senha);
                if(usuario==null){
                    ViewBag.NovoUsuario = IsNovoUsuario();
                    ViewBag.UsuarioNotFound = true;
                    return View(usuario);
                }else{
                    if(usuario.Tipo==TipoUsuario.Cliente)
                    {
                        using(ClienteData data = new ClienteData()) SetClienteSessao(data.Read(usuario));
                        if(GetPedidoSessao()!=null) return RedirectToAction("Index", "Pedido");
                    }else SetUsuarioSessao(usuario);
                    return RedirectToAction("Index", "Dashboard");
                } 
            }            
        }
    }
}