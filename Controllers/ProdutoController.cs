using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PandemicWeb.Data;
using PandemicWeb.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace PandemicWeb.Controllers
{
    public class ProdutoController : BaseController
    {        
        private readonly IWebHostEnvironment _env;
        public ProdutoController(IWebHostEnvironment env)
        {
            _env = env;            
        }
        
        [Route("produto/novo")]
        public IActionResult Create()
        {
            if(GetUsuarioSessao()!=null)
            {
                ViewBag.UserName = "<span class='text-light'>" + GetUsuarioSessao().Nome + "</span>";
                return View(new Produto());
            }else return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        [Route("produto/novo")]
        public IActionResult Create(Produto model)
        {                        
            int flag = 0;
            if(!ModelState.IsValid) flag++;
            if(model.Imagem==null)
            {
                ModelState.AddModelError("Imagem", "Escolha uma imagem");
                flag++;
            }
            else if(model.Imagem!=null && model.Imagem.Length > (5*1024*1024))
            {
                ModelState.AddModelError("Imagem", "O tamanho da imagem não pode ultrapassar 5 MB");
                flag++;
            }
            if(flag>0)
            {
                ViewBag.UserName = "<span class='text-light'>" + GetUsuarioSessao().Nome + "</span>";
                return View(model);
            }
            else{                
                if(GetUsuarioSessao()!=null)
                {
                    string fileName = GetUniqueFileName(model.Imagem.FileName);
                    var image = Image.Load(model.Imagem.OpenReadStream());
                    image.Mutate(x => x.Crop(262, 262));
                    image.Save(fileName);
                    string path = Path.Combine(Path.Combine(_env.WebRootPath, "imagens"), fileName);                                      
                    FileStream fs = new FileStream(path, FileMode.Create);
                    model.Imagem.CopyTo(fs);
                    fs.Flush();
                    fs.Close();                    
                    model.ImagePath = fileName;
                    using(ProdutoData data = new ProdutoData()) data.Create(model);
                    return RedirectToAction("Index", "Dashboard");
                }else return RedirectToAction("Index", "Login");
            }
        }

        [Route("produto/apagar/{id}")]
        public IActionResult Delete(int id)
        {
            if(!ModelState.IsValid)

            using(ProdutoData data = new ProdutoData())
            {
                data.Delete(id);
            }

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        [Route("produto/editar/{id}")]
        public IActionResult Update(int id)
        {
            if(GetUsuarioSessao()!=null)
            {
                ViewBag.UserName = "<span class='text-light'>" + GetUsuarioSessao().Nome + "</span>";
                using(ProdutoData data = new ProdutoData()) return View(data.Read(id));
            }else return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        [Route("produto/editar/{id}")]
        public IActionResult Update(int id, Produto model)
        {
            int flag = 0;
            if(!ModelState.IsValid) flag++;            
            if(model.Imagem!=null && model.Imagem.Length > (5*1024*1024))
            {
                ModelState.AddModelError("Imagem", "O tamanho da imagem não pode ultrapassar 5 MB");
                flag++;
            }
            if(flag>0)
            {
                ViewBag.UserName = "<span class='text-light'>" + GetUsuarioSessao().Nome + "</span>";
                return View(model);
            }
            else{
                if(GetUsuarioSessao()!=null)
                {                    
                    using(ProdutoData data = new ProdutoData()) model.ImagePath = data.Read(id).ImagePath;
                    if(model.Imagem!=null)
                    {
                        //string fileName = GetUniqueFileName(model.Imagem.FileName);
                        string path = Path.Combine(Path.Combine(_env.WebRootPath, "imagens"), model.ImagePath);
                        FileStream fs = new FileStream(path, FileMode.Create);                        
                        model.Imagem.CopyTo(fs);
                        fs.Flush();
                        fs.Close();                        
                    }
                    model.Id = id;
                    using(ProdutoData data = new ProdutoData()) data.Update(model);
                    return RedirectToAction("Index", "Dashboard");
                }else return RedirectToAction("Index", "Login");
            }
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return  Path.GetFileNameWithoutExtension(fileName)
                    + "_" 
                    + Guid.NewGuid().ToString().Substring(0, 4) 
                    + fileName.Replace(fileName.Split(".")[0], "");
        }

        [Route("")]
        public IActionResult Index(string busca)
        {
            if(GetUsuarioSessao()!=null)
            {
                ViewBag.UserName = "<span class='text-light'>" + GetUsuarioSessao().Nome + "</span>";
            }

            if (!String.IsNullOrEmpty(busca))
            {
                using (ProdutoData data = new ProdutoData())
                    return View(data.Search(busca));
            }

            using (ProdutoData data = new ProdutoData())
                return View(data.Catalogo());
        }

        private void DeleteImage(string path)
        {
            if(System.IO.File.Exists(path) && path != "imagens/indisponivel.jpg")
                System.IO.File.Delete(path);
        }  
    }
}