using Microsoft.AspNetCore.Mvc;
using PandemicWeb.Data;
using PandemicWeb.Models;

namespace PandemicWeb.Controllers
{
    public class DashboardController : BaseController
    {
        [Route("dashboard")]
        public IActionResult Index()
        {                                    
            if(GetClienteSessao()!=null) 
            {                
                ViewBag.Cliente =  GetClienteSessao();
                ViewBag.Pedidos = ViewBag.Cliente.Pedidos;
                ViewBag.Voucher = ViewBag.Cliente.Voucher;
                ViewBag.LogoutVisible = "block";
                ViewBag.UserName = ViewBag.Cliente.Usuario.Nome;
                return View(ViewBag.Cliente.Usuario);
            }else if(GetUsuarioSessao()!=null)
            {
                using(PedidoData data = new PedidoData()) ViewBag.Pedidos = data.Read();
                using(ClienteData data = new ClienteData()) ViewBag.Clientes = data.Read();
                using(ProdutoData data = new ProdutoData()) ViewBag.Produtos = data.Read();
                ViewBag.LogoutVisible = "block";
                ViewBag.UserName = GetUsuarioSessao().Nome;
                return View(GetUsuarioSessao());
            }else return RedirectToAction("Index", "Login");
        }

        [Route("comprar-voucher/{tipoVoucher}")]
        public IActionResult ComprarVoucher(string tipoVoucher)
        {
            Cliente cliente = GetClienteSessao();
            if(cliente!=null)
            {
                if(tipoVoucher.Equals("ouro")) cliente.Voucher.Credito+=(int)TipoVoucher.Ouro;    
                else if(tipoVoucher.Equals("prata")) cliente.Voucher.Credito+=(int)TipoVoucher.Prata;
                else if(tipoVoucher.Equals("bronze")) cliente.Voucher.Credito+=(int)TipoVoucher.Bronze;
                else cliente.Voucher.Credito+=(int)TipoVoucher.Platina;
                using(VoucherData data = new VoucherData()) data.Update(cliente.Voucher);
                SetClienteSessao(cliente);             
                return RedirectToAction("Index");
            }else return RedirectToAction("Index", "Login");
        }
    }
}