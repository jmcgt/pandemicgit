using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PandemicWeb.Models;

namespace PandemicWeb.Controllers
{
    public abstract class BaseController : Controller
    {        
        public void SetNovoUsuario(bool value){
            if(value) HttpContext.Session.SetString("new_user", value.ToString());
            else HttpContext.Session.Remove("new_user");
        }
        public bool IsNovoUsuario(){            
            return HttpContext.Session.GetString("new_user")!=null;
        }
        public Cliente GetClienteSessao()
        {
            if(HttpContext.Session.GetString("cliente_data")!=null){
                return JsonConvert.DeserializeObject<Cliente>(HttpContext.Session.GetString("cliente_data"));
            }else return null;
        }
        public void SetClienteSessao(Cliente cliente){HttpContext.Session.SetString("cliente_data", JsonConvert.SerializeObject(cliente));}
        public Usuario GetUsuarioSessao()
        {
            if(HttpContext.Session.GetString("user_data")!=null){
                return JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("user_data"));
            }else return null;
        }
        public void SetUsuarioSessao(Usuario usuario){
            HttpContext.Session.SetString("user_data", JsonConvert.SerializeObject(usuario));
        }
        public Pedido GetPedidoSessao()
        {
            if(HttpContext.Session.GetString("pedido_data")!=null){
                return JsonConvert.DeserializeObject<Pedido>(HttpContext.Session.GetString("pedido_data"));
            }else return null;
        }
        public void SetPedidoSessao(Pedido pedido){HttpContext.Session.SetString("pedido_data", JsonConvert.SerializeObject(pedido));}
        public void Logout(){
            if(HttpContext.Session.GetString("user_data")!=null) HttpContext.Session.Remove("user_data"); 
            if(HttpContext.Session.GetString("cliente_data")!=null) HttpContext.Session.Remove("cliente_data");
            if(HttpContext.Session.GetString("pedido_data")!=null) HttpContext.Session.Remove("pedido_data");
        }
    }
}