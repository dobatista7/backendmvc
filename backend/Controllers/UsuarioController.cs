using atividade2.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace atividade2.Controllers
{
    public class UsuarioController : Controller
    {
     
     // controller: objetivo é ter as rotas com as actions crud

    public IActionResult Login(){
        return View();
    }

    [HttpPost]
     public IActionResult Login(Usuario user){

         UsuarioRepository ur = new UsuarioRepository();
         Usuario usuarioEncontrado = ur.ValidarLogin(user);

         if(usuarioEncontrado==null){
             ViewBag.Mensagem = "Falha no login";
             return View();
         }else {

             //iremos gravar na sessao - nossas credenciais
             HttpContext.Session.SetInt32("IdUsuario",usuarioEncontrado.id);
             HttpContext.Session.SetString("NomeUsuario",usuarioEncontrado.Nome);

             return RedirectToAction("Lista","Usuario");// action, controller
         }
     }

    public IActionResult Logout(){
        //objetivo é limpar os dados da sessão
        HttpContext.Session.Clear();
        return View("Login");
    }
     public IActionResult Editar(int Id){

         if(HttpContext.Session.GetInt32("IdUsuario")==null){
             return RedirectToAction("Login","Usuario");

         }
          UsuarioRepository ur = new UsuarioRepository();
          Usuario userLocalizado = ur.BuscarPorId(Id);
          return View(userLocalizado);
     }

     [HttpPost]
     public IActionResult Editar (Usuario user){

         UsuarioRepository ur = new UsuarioRepository();
         ur.Alterar(user);
         return RedirectToAction("Lista","Usuario");       

     }

     public IActionResult Remover(int Id){

         if(HttpContext.Session.GetInt32("IdUsuario")==null){
             return RedirectToAction("Login","Usuario");

         }

         UsuarioRepository ur = new UsuarioRepository();
         Usuario userLocalizado = ur.BuscarPorId(Id);
         ur.Excluir(userLocalizado);
         return RedirectToAction("Lista","Usuario");
     }

     public IActionResult Lista(){

         if(HttpContext.Session.GetInt32("IdUsuario")==null){
             return RedirectToAction("Login","Usuario");

         }

         UsuarioRepository ur = new UsuarioRepository();
         List<Usuario> listagemDeUsuarios = ur.Listar();
         return View(listagemDeUsuarios);

     }

     public IActionResult Cadastro(){

         return View();
     }

    [HttpPost]
    public IActionResult Cadastro (Usuario user){

        UsuarioRepository ur = new UsuarioRepository();

        ur.Inserir(user);
        ViewBag.Mensagem = "Cadastro realizado com sucesso!";
        return View();
    }
        
    }
}