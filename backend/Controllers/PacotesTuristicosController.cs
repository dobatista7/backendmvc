using atividade2.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace atividade2.Controllers
{
    public class PacotesTuristicosController : Controller

    {
        //controller: objetivo é ter as rotas com as actions = crud

         public IActionResult Editar (int Id){

        if(HttpContext.Session.GetInt32("IdUsuario")==null){
             return RedirectToAction("Login","Usuario");

         }

         PacotesTuristicosRepository ur = new PacotesTuristicosRepository();
         PacotesTuristicos pctLocalizado = ur.BuscarPorId(Id);
        return View(pctLocalizado);
     }

     
     [HttpPost]
     public IActionResult Editar (PacotesTuristicos pct){

         PacotesTuristicosRepository ur = new PacotesTuristicosRepository();
        ur.Alterar(pct);        
        return RedirectToAction("Lista","PacotesTuristicos");       

     }

        
     public IActionResult Remover(int Id){

         if(HttpContext.Session.GetInt32("IdUsuario")==null){
             return RedirectToAction("Login","Usuario");

         }

         PacotesTuristicosRepository ur = new PacotesTuristicosRepository();
         PacotesTuristicos pctLocalizado = ur.BuscarPorId(Id);
         ur.Excluir(pctLocalizado);
         return RedirectToAction("Lista","PacotesTuristicos");
     }
  
     public IActionResult Lista(){

         if(HttpContext.Session.GetInt32("IdUsuario")==null){
             return RedirectToAction("Login","Usuario");

         }

         PacotesTuristicosRepository ur = new PacotesTuristicosRepository();
         List<PacotesTuristicos> listagemDePacotes = ur.Listar();
         return View(listagemDePacotes);

     }

     
     public IActionResult Cadastro(){

         return View();
     }

    [HttpPost]
    public IActionResult Cadastro (PacotesTuristicos pct){

        PacotesTuristicosRepository ur = new PacotesTuristicosRepository();

       ur.Inserir(pct);
        ViewBag.Mensagem = "Pacote Turístico cadastrado com sucesso!";
        return View();
    }
  }
}