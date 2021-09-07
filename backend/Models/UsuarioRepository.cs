using System.Collections.Generic;
using MySqlConnector;
using System;

namespace atividade2.Models
{
    public class UsuarioRepository
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

        public Usuario ValidarLogin(Usuario user){

            //abrir a conexão com o banco de dados
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();

             //query em sql para buscar por Login e Senha
            String QuerySql= "select * from Usuario WHERE Login=@Login and Senha=@Senha";

            //preparar um comando, passando: sql +conexão com o banco de dados
            MySqlCommand Comando = new MySqlCommand(QuerySql,Conexao);

            //tratamento devido ao slq injection 
            Comando.Parameters.AddWithValue("@Login",user.Login);
            Comando.Parameters.AddWithValue("@Senha",user.Senha);

            //Executo no banco de dados dados, que retorna único usuario quando encontrado
            MySqlDataReader Reader = Comando.ExecuteReader();

            // iremos inicializar o obj usuario encontrado como null
            //pq essa estrategia? Caso o obj Reader (49) não encontrar
            //registros o objt retornara null, caso encontre (57) retorna o 
            //conjunto de dados    
            Usuario UsuarioEncontrado = null;

            //aqui esta a validação do obj reader
            if(Reader.Read()){

                // se entrar aqui, significa que encontrou o usuario com login e senha informado
                UsuarioEncontrado = new Usuario();
                UsuarioEncontrado.id= Reader.GetInt32("id");

            if (!Reader.IsDBNull(Reader.GetOrdinal("Nome")))
                 UsuarioEncontrado.Nome= Reader.GetString("Nome");

            if (!Reader.IsDBNull(Reader.GetOrdinal("Login")))     
                UsuarioEncontrado.Login =Reader.GetString("Login");

            if (!Reader.IsDBNull(Reader.GetOrdinal("Senha")))
                UsuarioEncontrado.Senha = Reader.GetString("Senha");
                
            UsuarioEncontrado.DataNascimento = Reader.GetDateTime("DataNascimento");
            }

            //fechar a conexão com o banco
            Conexao.Close();

            //retornar o usuário encontrado
            return UsuarioEncontrado;

        }
        
        public Usuario BuscarPorId(int Id){

            //abrir a conexão com o banco de dados
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();

             //query em sql para buscar por ID (select)
            String QuerySql= "select * from Usuario WHERE id=@id";

            //preparar um comando, passando: sql +conexão com o banco de dados
            MySqlCommand Comando = new MySqlCommand(QuerySql,Conexao);

            //tratamento devido ao slq injection 
            Comando.Parameters.AddWithValue("@id",Id);

            //Executo no banco de dados dados, que retorna único usuario quando encontrado
            MySqlDataReader Reader = Comando.ExecuteReader();

            Usuario UsuarioEncontrado = new Usuario();

            if(Reader.Read()){
                UsuarioEncontrado.id= Reader.GetInt32("id");

            if (!Reader.IsDBNull(Reader.GetOrdinal("Nome")))
                 UsuarioEncontrado.Nome= Reader.GetString("Nome");

            if (!Reader.IsDBNull(Reader.GetOrdinal("Login")))     
                UsuarioEncontrado.Login =Reader.GetString("Login");

            if (!Reader.IsDBNull(Reader.GetOrdinal("Senha")))
                UsuarioEncontrado.Senha = Reader.GetString("Senha");
                
            UsuarioEncontrado.DataNascimento = Reader.GetDateTime("DataNascimento");
            }

            //fechar a conexão com o banco
            Conexao.Close();

            //retornar o usuário encontrado
            return UsuarioEncontrado;

        }
        
        public List<Usuario> Listar(){

            //abrir a conexão com o banco de dados
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();

             //query em sql para listagem (select)
            String QuerySql= "select * from Usuario";

            //preparar um comando, passando: sql +conexão com o banco de dados
            MySqlCommand Comando = new MySqlCommand(QuerySql,Conexao);

            //Executo no banco de dados dados, que retorna uma lista.Indicado para comandos select
            MySqlDataReader Reader = Comando.ExecuteReader();

            //simplesmente criando uma lista de usuario
            List<Usuario> Lista = new List<Usuario>();

            //percorre todos os registros retornados no bando de dados(obj. Reader) 
            while(Reader.Read()){

                Usuario userEncontrado = new Usuario();

                userEncontrado.id= Reader.GetInt32("id");

                //Tratamento somente para campo string, demais campos não.
                if (!Reader.IsDBNull(Reader.GetOrdinal("Nome")))
                     userEncontrado.Nome= Reader.GetString("Nome");

                if (!Reader.IsDBNull(Reader.GetOrdinal("Login")))
                    userEncontrado.Login= Reader.GetString("Login");

                if (!Reader.IsDBNull(Reader.GetOrdinal("Senha")))   
                    userEncontrado.Senha= Reader.GetString("Senha");

                userEncontrado.DataNascimento= Reader.GetDateTime("DataNascimento");

                // add na lista de usuarios    
                Lista.Add(userEncontrado);

            }

            //fechar a conexão com o banco
            Conexao.Close();

            //retornamos a lista com todos os registros armazenados no banco de dados
            return Lista;

        }

        public void Inserir(Usuario user){

            //abrir a conexão com o banco de dados
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();

            //query em sql para inserir (insert)
            String QuerySql = "insert into Usuario (Nome,Login,Senha,DataNascimento) values (@Nome,@Login,@Senha,@DataNascimento)";

            //preparar um comando, passando: sql +conexão com o banco de dados
            MySqlCommand Comando = new MySqlCommand(QuerySql,Conexao);

            //tratamento devido ao slq injection 
            Comando.Parameters.AddWithValue("@Nome",user.Nome);
            Comando.Parameters.AddWithValue("@Login",user.Login);
            Comando.Parameters.AddWithValue("@Senha",user.Senha);
            Comando.Parameters.AddWithValue("@DataNascimento",user.DataNascimento);

            //executar o comando no banco de dados
            Comando.ExecuteNonQuery();

            //fechar a conexão com o banco
            Conexao.Close();
        }

        public void Alterar(Usuario user){

            //abrir a conexão com o banco de dados
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();

            //query em sql para alterar (update)
            String QuerySql= "update Usuario set  Nome=@Nome, Login=@Login, Senha=@Senha,DataNascimento=@DataNascimento WHERE id=@id";

            //preparar um comando, passando: sql +conexão com o banco de dados
            MySqlCommand Comando = new MySqlCommand(QuerySql,Conexao);

            //tratamento devido ao slq injection 
            Comando.Parameters.AddWithValue("@id",user.id);
            Comando.Parameters.AddWithValue("@Nome",user.Nome);
            Comando.Parameters.AddWithValue("@Login",user.Login);
            Comando.Parameters.AddWithValue("@Senha",user.Senha);
            Comando.Parameters.AddWithValue("@DataNascimento",user.DataNascimento);

            //executar o comando no banco de dados
            Comando.ExecuteNonQuery();

            //fechar a conexão com o banco
            Conexao.Close();
        }

        public void Excluir(Usuario user){

            //abrir a conexão com o banco de dados
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();

            //query em sql para excluir (delete)
            String QuerySql = "delete from Usuario WHERE id=@id";

            //preparar um comando, passando: sql +conexão com o banco de dados
            MySqlCommand Comando = new MySqlCommand(QuerySql,Conexao);

            //tratamento devido ao slq injection 
            Comando.Parameters.AddWithValue("@id",user.id);

            //executar o comando no banco de dados
            Comando.ExecuteNonQuery();

            //fechar a conexão com o banco
            Conexao.Close();
        }

    }
}