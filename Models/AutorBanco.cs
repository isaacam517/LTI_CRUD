using System;
using System.Collections.Generic;
using MySqlConnector;

namespace CRUD_LTI.Models
{
    public class AutorBanco
    {
        //Inserindo os dados para fazer a conexão com o banco
        private const string DadosConexao = "Database=db_crud_lti; Data Source=localhost; User Id=root;";

        //Para testar a conexão
        public void TestarConexao()
        {
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();
            Console.WriteLine("Banco de dados funcionando!");
            Conexao.Close();
        }

        //Validar login
        public Login ValidarUsuario(Login login)
        {
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();
            string Query = "SELECT * FROM Usuario WHERE Usuario=@Usuario AND Senha=@Senha";
            MySqlCommand Comando = new MySqlCommand(Query, Conexao);
            Comando.Parameters.AddWithValue("@Usuario", login.Usuario); 
            Comando.Parameters.AddWithValue("@Senha", login.Senha);
            MySqlDataReader Reader = Comando.ExecuteReader(); 

            Login UsuarioEncontrado = new Login();

            if(Reader.Read())
            {
                UsuarioEncontrado.Id = Reader.GetInt32("Id");

                if(!Reader.IsDBNull(Reader.GetOrdinal("Usuario")))
                    UsuarioEncontrado.Usuario = Reader.GetString("Usuario");
                if(!Reader.IsDBNull(Reader.GetOrdinal("Senha")))
                    UsuarioEncontrado.Senha = Reader.GetString("Senha");

            }
            Conexao.Close();
            return UsuarioEncontrado;
        }

        //Listar Autores
        public List<Autor> Listar()
        {
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();
            string Query = "SELECT * FROM Autor";
            MySqlCommand Comando = new MySqlCommand(Query, Conexao);
            MySqlDataReader Reader = Comando.ExecuteReader(); 

            List<Autor> Lista = new List<Autor>();

            while(Reader.Read())
            {
                Autor AutorEncontrado = new Autor();
                AutorEncontrado.Id = Reader.GetInt32("Id");

                if(!Reader.IsDBNull(Reader.GetOrdinal("Nome")))
                    AutorEncontrado.Nome = Reader.GetString("Nome");
                if(!Reader.IsDBNull(Reader.GetOrdinal("DataNascimento")))
                    AutorEncontrado.DataNascimento = Reader.GetString("DataNascimento");
                if(!Reader.IsDBNull(Reader.GetOrdinal("NomeLivro")))
                    AutorEncontrado.NomeLivro = Reader.GetString("NomeLivro");

                Lista.Add(AutorEncontrado);    
            }
            Conexao.Close();
            return Lista;
        }

        //Inserir Autor
        public void Inserir(Autor autor)
        {
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();
            string Query = "INSERT INTO Autor (Nome, DataNascimento, NomeLivro) VALUES (@Nome, @DataNascimento, @NomeLivro)";
            MySqlCommand Comando = new MySqlCommand(Query, Conexao);
            Comando.Parameters.AddWithValue("@Nome", autor.Nome); 
            Comando.Parameters.AddWithValue("@DataNascimento", autor.DataNascimento);
            Comando.Parameters.AddWithValue("@NomeLivro", autor.NomeLivro);
            Comando.ExecuteNonQuery();
            Conexao.Close();
        }

        //Atualizar um dado do Autor
        public void Atualizar(Autor autor)
        {
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();
            string Query = "UPDATE Autor SET Nome=@Nome, DataNascimento=@DataNascimento, NomeLivro=@NomeLivro WHERE Id=@Id";
            MySqlCommand Comando = new MySqlCommand(Query, Conexao);
            Comando.Parameters.AddWithValue("@Id", autor.Id); 
            Comando.Parameters.AddWithValue("@Nome", autor.Nome); 
            Comando.Parameters.AddWithValue("@DataNascimento", autor.DataNascimento);
            Comando.Parameters.AddWithValue("@NomeLivro", autor.NomeLivro);
            Comando.ExecuteNonQuery();
            Conexao.Close();
        }

        //Remover Autor
        public void Remover(int Id)
        {
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();
            string Query = "DELETE FROM Autor WHERE Id=@Id";
            MySqlCommand Comando = new MySqlCommand(Query, Conexao);
            Comando.Parameters.AddWithValue("@Id", Id); 
            Comando.ExecuteNonQuery();
            Conexao.Close();
        }
        //Buscar por Id
        public Autor BuscarPorId(int Id)
        {
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();
            string Query = "SELECT * FROM Autor WHERE Id=@Id";
            MySqlCommand Comando = new MySqlCommand(Query, Conexao);
            Comando.Parameters.AddWithValue("@Id", Id); 
            MySqlDataReader Reader = Comando.ExecuteReader(); 

            Autor AutorEncontrado = new Autor();

            while(Reader.Read())
            {
                AutorEncontrado.Id = Reader.GetInt32("Id");

                if(!Reader.IsDBNull(Reader.GetOrdinal("Nome")))
                    AutorEncontrado.Nome = Reader.GetString("Nome");
                if(!Reader.IsDBNull(Reader.GetOrdinal("DataNascimento")))
                    AutorEncontrado.DataNascimento = Reader.GetString("DataNascimento");
                if(!Reader.IsDBNull(Reader.GetOrdinal("NomeLivro")))
                    AutorEncontrado.NomeLivro = Reader.GetString("NomeLivro");    

            }
            Conexao.Close();
            return AutorEncontrado;
        }
    }
}