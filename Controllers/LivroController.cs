using System.Collections.Generic;
using CRUD_LTI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_LTI.Controllers
{
    public class LivroController : Controller
    {
        public IActionResult Lista()
        {
            LivroBanco autorbanco = new LivroBanco();
            List<Livro> Lista = autorbanco.Listar();
            return View(Lista);
        }

        public IActionResult Cadastro()
        {
             if(HttpContext.Session.GetInt32("IdUsuario") == null)
                return RedirectToAction("Aviso", "Autor");
            return View();    
        }

        [HttpPost]
        public IActionResult Cadastro(Livro livro)
        {
            if(HttpContext.Session.GetInt32("IdUsuario") == null)
                return RedirectToAction("Aviso", "Autor");

            LivroBanco livrobanco = new LivroBanco();
            livrobanco.Inserir(livro);

            
            ViewBag.Mensagem = "Cadastro de NOVO LIVRO realizado com sucesso!";
            return View();    
        }

        public IActionResult Editar(int Id)
        {
            if(HttpContext.Session.GetInt32("IdUsuario") == null)
                return RedirectToAction("Aviso", "Autor");
            LivroBanco livrobanco = new LivroBanco();
            Livro livro = livrobanco.BuscarPorId(Id);    
            return View(livro);    
        }

        [HttpPost]
        public IActionResult Editar(Livro livro)
        {
            if(HttpContext.Session.GetInt32("IdUsuario") == null)
                return RedirectToAction("Aviso", "Autor");
            LivroBanco livrobanco = new LivroBanco();
            livrobanco.Atualizar(livro);
            ViewBag.Mensagem = "Livro atualizado com sucesso!";
            return RedirectToAction("Lista");    
        }

        public IActionResult Remover(int Id)
        {
            if(HttpContext.Session.GetInt32("IdUsuario") == null)
                return RedirectToAction("Login", "Autor");
            LivroBanco livrobanco = new LivroBanco();
            livrobanco.Remover(Id);
            return RedirectToAction("Lista"); 
        }


    }
}