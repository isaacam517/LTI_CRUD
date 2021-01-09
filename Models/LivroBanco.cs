using System;
using System.Collections.Generic;
using MySqlConnector;

namespace CRUD_LTI.Models
{
    public class LivroBanco
    {
        //Inserindo os dados para fazer a conexão com o banco
        private const string DadosConexao = "Database=db_crud_lti; Data Source=localhost; User Id=root;";
        
        //Listar Livros
        public List<Livro> Listar()
        {
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();
            string Query = "SELECT * FROM Livro";
            MySqlCommand Comando = new MySqlCommand(Query, Conexao);
            MySqlDataReader Reader = Comando.ExecuteReader(); 

            List<Livro> Lista = new List<Livro>();

            while(Reader.Read())
            {
                Livro LivroEncontrado = new Livro();
                LivroEncontrado.Id = Reader.GetInt32("Id");

                if(!Reader.IsDBNull(Reader.GetOrdinal("Titulo")))
                    LivroEncontrado.Titulo = Reader.GetString("Titulo");
                if(!Reader.IsDBNull(Reader.GetOrdinal("Descricao")))
                    LivroEncontrado.Descricao = Reader.GetString("Descricao");
                if(!Reader.IsDBNull(Reader.GetOrdinal("NomeAutor")))
                    LivroEncontrado.NomeAutor = Reader.GetString("NomeAutor");

                Lista.Add(LivroEncontrado);    
            }
            Conexao.Close();
            return Lista;
        }

        //Inserir Livro
        public void Inserir(Livro livro)
        {
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();
            string Query = "INSERT INTO Livro (Titulo, Descricao, NomeAutor) VALUES (@Titulo, @Descricao, @NomeAutor)";
            MySqlCommand Comando = new MySqlCommand(Query, Conexao);
            Comando.Parameters.AddWithValue("@Titulo", livro.Titulo); 
            Comando.Parameters.AddWithValue("@Descricao", livro.Descricao);
            Comando.Parameters.AddWithValue("@NomeAutor", livro.NomeAutor);
            Comando.ExecuteNonQuery();
            Conexao.Close();
        }

        //Atualizar um dado do Livro
        public void Atualizar(Livro livro)
        {
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();
            string Query = "UPDATE Livro SET Titulo=@Titulo, Descricao=@Descricao, NomeAutor=@NomeAutor WHERE Id=@Id";
            MySqlCommand Comando = new MySqlCommand(Query, Conexao);
            Comando.Parameters.AddWithValue("@Id", livro.Id); 
            Comando.Parameters.AddWithValue("@Titulo", livro.Titulo); 
            Comando.Parameters.AddWithValue("@Descricao", livro.Descricao);
            Comando.Parameters.AddWithValue("@NomeAutor", livro.NomeAutor);
            Comando.ExecuteNonQuery();
            Conexao.Close();
        }

        //Remover Livro
        public void Remover(int Id)
        {
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();
            string Query = "DELETE FROM Livro WHERE Id=@Id";
            MySqlCommand Comando = new MySqlCommand(Query, Conexao);
            Comando.Parameters.AddWithValue("@Id", Id); 
            Comando.ExecuteNonQuery();
            Conexao.Close();
        }

        //Buscar por Id
        public Livro BuscarPorId(int Id)
        {
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();
            string Query = "SELECT * FROM Livro WHERE Id=@Id";
            MySqlCommand Comando = new MySqlCommand(Query, Conexao);
            Comando.Parameters.AddWithValue("@Id", Id); 
            MySqlDataReader Reader = Comando.ExecuteReader(); 

            Livro LivroEncontrado = new Livro();

            while(Reader.Read())
            {
                LivroEncontrado.Id = Reader.GetInt32("Id");

                if(!Reader.IsDBNull(Reader.GetOrdinal("Titulo")))
                    LivroEncontrado.Titulo = Reader.GetString("Titulo");
                if(!Reader.IsDBNull(Reader.GetOrdinal("Descricao")))
                    LivroEncontrado.Descricao = Reader.GetString("Descricao");
                if(!Reader.IsDBNull(Reader.GetOrdinal("NomeAutor")))
                    LivroEncontrado.NomeAutor = Reader.GetString("NomeAutor");
            }
            Conexao.Close();
            return LivroEncontrado;
        }
    }

}