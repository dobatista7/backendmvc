using System;
namespace atividade2.Models
{
    public class Usuario
    {
        public int id {get;set;}
        public string Nome{get;set;}

        public string Login{get;set;}
        public string Senha{get;set;}

        public DateTime DataNascimento {get;set;}
    }
}