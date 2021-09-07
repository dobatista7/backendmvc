using System.Collections.Generic;
using MySqlConnector;
using System;

namespace atividade2.Models
{
    public class PacotesTuristicosRepository
    {
        //ter as credenciais de acesso ao banco de dados

        private const string DadosConexao= "Database=atividade_2;Data Source=localhost;User Id=root";

        public void TestarConexao(){

            //Informar a credencial de acesso
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            //abrir uma conexao
            Conexao.Open();
            //imprimir uma mensagem de tudo funcionando
            Console.WriteLine("Banco de dados funcionando!");
            //fechar uma conexao
            Conexao.Close();
        }

        public PacotesTuristicos BuscarPorId(int Id){

            //abrir a conexão com o banco de dados
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();

             //query em sql para listagem (select)
            String QuerySql= "select * from PacotesTuristicos WHERE Id=@Id";

            //preparar um comando, passando: sql +conexão com o banco de dados
            MySqlCommand Comando = new MySqlCommand(QuerySql,Conexao);

            //tratamento devido ao slq injection 
            Comando.Parameters.AddWithValue("@Id",Id);

            //Executo no banco de dados dados, que retorna único pacote quando encontrado
            MySqlDataReader Reader = Comando.ExecuteReader();

            PacotesTuristicos pacoteEncontrado = new PacotesTuristicos();

            
            //Tratamento somente para campo string, demais campos não.
            if(Reader.Read()){
            
                pacoteEncontrado.Id= Reader.GetInt32("Id");
         
            if (!Reader.IsDBNull(Reader.GetOrdinal("Nome")))
                 pacoteEncontrado.Nome= Reader.GetString("Nome");

          if (!Reader.IsDBNull(Reader.GetOrdinal("Origem")))
                    pacoteEncontrado.Origem= Reader.GetString("Origem");

                if (!Reader.IsDBNull(Reader.GetOrdinal("Destino")))   
                    pacoteEncontrado.Destino= Reader.GetString("Destino");

                if (!Reader.IsDBNull(Reader.GetOrdinal("Atrativos")))   
                    pacoteEncontrado.Atrativos= Reader.GetString("Atrativos");

                if (!Reader.IsDBNull(Reader.GetOrdinal("Saida")))   
                    pacoteEncontrado.Saida= Reader.GetDateTime("Saida");

                if (!Reader.IsDBNull(Reader.GetOrdinal("Retorno")))   
                    pacoteEncontrado.Retorno= Reader.GetDateTime("Retorno");

                pacoteEncontrado.Usuario= Reader.GetInt32("Usuario");

            }

            //fechar a conexão com o banco
            Conexao.Close();

            //retornar o usuário encontrado
            return pacoteEncontrado;
        }


        public List<PacotesTuristicos> Listar(){

            //abrir a conexão com o banco de dados
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();

             //query em sql para listagem (select)
            String QuerySql= "select * from PacotesTuristicos";

            //preparar um comando, passando: sql +conexão com o banco de dados
            MySqlCommand Comando = new MySqlCommand(QuerySql,Conexao);

            //Executo no banco de dados dados, que retorna uma lista.Indicado para comandos select
            MySqlDataReader Reader = Comando.ExecuteReader();

            //simplesmente criando uma lista de pacotes turisticos
            List<PacotesTuristicos> Lista = new List<PacotesTuristicos>();

            //percorre todos os registros retornados no bando de dados(obj. Reader) 
            while(Reader.Read()){

                PacotesTuristicos pacoteEncontrado = new PacotesTuristicos();

                pacoteEncontrado.Id= Reader.GetInt32("Id");

                //Tratamento somente para campo string, demais campos não.
                if (!Reader.IsDBNull(Reader.GetOrdinal("Nome")))
                     pacoteEncontrado.Nome= Reader.GetString("Nome");

                if (!Reader.IsDBNull(Reader.GetOrdinal("Origem")))
                    pacoteEncontrado.Origem= Reader.GetString("Origem");

                if (!Reader.IsDBNull(Reader.GetOrdinal("Destino")))   
                    pacoteEncontrado.Destino= Reader.GetString("Destino");

                if (!Reader.IsDBNull(Reader.GetOrdinal("Atrativos")))   
                    pacoteEncontrado.Atrativos= Reader.GetString("Atrativos");

                if (!Reader.IsDBNull(Reader.GetOrdinal("Saida")))   
                    pacoteEncontrado.Saida= Reader.GetDateTime("Saida");

                if (!Reader.IsDBNull(Reader.GetOrdinal("Retorno")))   
                    pacoteEncontrado.Retorno= Reader.GetDateTime("Retorno");

                pacoteEncontrado.Usuario= Reader.GetInt32("Usuario");

                // add na lista de usuarios    
                Lista.Add(pacoteEncontrado);
            }

            //fechar a conexão com o banco
            Conexao.Close();

            //retornamos a lista com todos os registros armazenados no banco de dados
            return Lista;
        }


        public void Inserir(PacotesTuristicos pct){

            //abrir a conexão com o banco de dados
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();

            //query em sql para inserir (insert)
            String QuerySql= "insert into PacotesTuristicos (Nome,Origem,Destino,Atrativos,Saida,Retorno,Usuario) values(@Nome,@Origem,@Destino,@Atrativos,@Saida,@Retorno,@Usuario)";

            //preparar um comando, passando: sql +conexão com o banco de dados
            MySqlCommand Comando = new MySqlCommand(QuerySql,Conexao);

            //tratamento devido ao slq injection 
            Comando.Parameters.AddWithValue("@Nome",pct.Nome);
            Comando.Parameters.AddWithValue("@Origem",pct.Origem);
            Comando.Parameters.AddWithValue("@Destino",pct.Destino);
            Comando.Parameters.AddWithValue("@Atrativos",pct.Destino);
            Comando.Parameters.AddWithValue("@Saida",pct.Saida);
            Comando.Parameters.AddWithValue("@Retorno",pct.Retorno);
            Comando.Parameters.AddWithValue("@Usuario",pct.Usuario);

            //executar o comando no banco de dados
            Comando.ExecuteNonQuery();

            //fechar a conexão com o banco
            Conexao.Close();
        }



        public void Alterar(PacotesTuristicos pct){

            //abrir a conexão com o banco de dados
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();

            //query em sql para alterar (update)
            String QuerySql= "update PacotesTuristicos set  Nome=@Nome, Origem=@Origem, Destino=@Destino,Atrativos=@Atrativos,Saida=@Saida,Retorno=@Retorno,Usuario=@Usuario WHERE Id=@Id";

            //preparar um comando, passando: sql +conexão com o banco de dados
            MySqlCommand Comando = new MySqlCommand(QuerySql,Conexao);

            //tratamento devido ao slq injection 
            Comando.Parameters.AddWithValue("@Id",pct.Id);
            Comando.Parameters.AddWithValue("@Nome",pct.Nome);
            Comando.Parameters.AddWithValue("@Origem",pct.Origem);
            Comando.Parameters.AddWithValue("@Destino",pct.Destino);
            Comando.Parameters.AddWithValue("@Atrativos",pct.Atrativos);
            Comando.Parameters.AddWithValue("@Saida",pct.Saida);
            Comando.Parameters.AddWithValue("@Retorno",pct.Retorno);
            Comando.Parameters.AddWithValue("@Usuario",pct.Usuario);


            //executar o comando no banco de dados
            Comando.ExecuteNonQuery();

            //fechar a conexão com o banco
            Conexao.Close();
        }

        public void Excluir(PacotesTuristicos pct){

            //abrir a conexão com o banco de dados
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();

            //query em sql para excluir (delete)
            String QuerySql= "delete from PacotesTuristicos WHERE Id=@Id";

            //preparar um comando, passando: sql +conexão com o banco de dados
            MySqlCommand Comando = new MySqlCommand(QuerySql,Conexao);

            //tratamento devido ao slq injection 
            Comando.Parameters.AddWithValue("@Id",pct.Id);

            //executar o comando no banco de dados
            Comando.ExecuteNonQuery();

            //fechar a conexão com o banco
            Conexao.Close();
        }
    }
}