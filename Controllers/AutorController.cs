using System.Collections.Generic;
using CRUD_LTI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_LTI.Controllers
{
    public class AutorController : Controller
    {
        public IActionResult Lista()
        {
            // if(HttpContext.Session.GetInt32("IdAutor") == null)
            //     return RedirectToAction("Login");

            AutorBanco autorbanco = new AutorBanco();
            List<Autor> Lista = autorbanco.Listar();
            return View(Lista);
        }
        public IActionResult Aviso()
        {
            return View();    
        }

        public IActionResult AvisoSaiu()
        {
            return View();    
        }

        public IActionResult Cadastro()
        {
            if(HttpContext.Session.GetInt32("IdUsuario") == null)
                return RedirectToAction("Aviso");
            return View();    
        }

        [HttpPost]
        public IActionResult Cadastro(Autor autor)
        {
            if(HttpContext.Session.GetInt32("IdUsuario") == null)
                return RedirectToAction("Aviso");
            AutorBanco autorbanco = new AutorBanco();
            autorbanco.Inserir(autor);
            ViewBag.Mensagem = "Cadastro realizado com sucesso!";
            return View();    
        }

        public IActionResult Editar(int Id)
        {
            if(HttpContext.Session.GetInt32("IdUsuario") == null)
                return RedirectToAction("Aviso");
            AutorBanco autorbanco = new AutorBanco();
            Autor autor = autorbanco.BuscarPorId(Id);    
            return View(autor);    
        }

        [HttpPost]
        public IActionResult Editar(Autor autor)
        {
            if(HttpContext.Session.GetInt32("IdUsuario") == null)
                return RedirectToAction("Aviso");
            AutorBanco autorbanco = new AutorBanco();
            autorbanco.Atualizar(autor);
            ViewBag.Mensagem = "Autor atualizado com sucesso!";
            return RedirectToAction("Lista");    
        }

        public IActionResult Remover(int Id)
        {
            if(HttpContext.Session.GetInt32("IdUsuario") == null)
                return RedirectToAction("Aviso");
            AutorBanco autorbanco = new AutorBanco();
            autorbanco.Remover(Id);
            return RedirectToAction("Lista"); 
        }


        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Login login)
        {
            AutorBanco autorbanco = new AutorBanco();
            Login autorSessao = autorbanco.ValidarUsuario(login);

            if(login != null)
            {
                ViewBag.Mensagem="Você está logado!";
                HttpContext.Session.SetInt32("IdUsuario", autorSessao.Id);
                HttpContext.Session.SetString("NomeUsuario", autorSessao.Usuario);

                return Redirect("Lista");
            } else {
                ViewBag.Mensagem = "Falha no Login";
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Autor");
        }
    }
}