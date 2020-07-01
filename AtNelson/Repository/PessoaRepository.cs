using AtNelson.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;


namespace AtNelson.Repository
{
    public class PessoaRepository
    {
        private string ConnectionString { get; set; }

        public PessoaRepository(IConfiguration configuration)
        {
            this.ConnectionString = configuration.GetConnectionString("Gerenciamento");
        }

        public void Save(Pessoa pessoa)
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                string sql = "INSERT INTO PESSOA(NOME, SOBRENOME, DATANASCIMENTO) VALUES (@P1, @P2, @P3)";

                connection.Execute(sql, new { P1 = pessoa.Nome, P2 = pessoa.Sobrenome, P3 = pessoa.DataNascimento });
            }
        }

        public void Update(Pessoa pessoa)
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                string sql = @"
                                UPDATE PESSOA
                                SET NOME = @P1,
                                SOBRENOME = @P2,
                                DATANASCIMENTO = @P3
                                WHERE ID = @P4;
                ";

                connection.Execute(sql, new { P1 = pessoa.Nome, P2 = pessoa.Sobrenome, P3 = pessoa.DataNascimento, P4 = pessoa.Id });
            }
        }

        public void Delete(Pessoa pessoa)
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                string sql = @"
                        DELETE FROM PESSOA
                        WHERE ID = @P1
                ";

                connection.Execute(sql, new { P1 = pessoa.Id });
            }
        }

        public List<Pessoa> GetAll()
        {
            List<Pessoa> result = new List<Pessoa>();

            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                string sql = @"
                        SELECT ID, NOME, SOBRENOME, DATANASCIMENTO
                        FROM PESSOA
                        ORDER BY DATANASCIMENTO ASC
                ";

                result = connection.Query<Pessoa>(sql).ToList();
            }
                
            return result;
        }

        public Pessoa GetPessoaById(int id)
        {
            Pessoa result = null;

            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                string sql = @"
                        SELECT ID, NOME, SOBRENOME, DATANASCIMENTO
                        FROM PESSOA
                        WHERE ID = @P1
                ";
                result = connection.QueryFirstOrDefault<Pessoa>(sql, new { P1 = id });
            }

            return result;
        }

        public List<Pessoa> Search(string query)
        {
            List<Pessoa> result = new List<Pessoa>();

            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                string sql = @"
                        SELECT ID, NOME, SOBRENOME, DATANASCIMENTO
                        FROM PESSOA
                        WHERE (NOME LIKE '%' + @P1 +'%' OR SOBRENOME LIKE '%' + @P2 + '%')
                ";

                result = connection.Query<Pessoa>(sql, new {P1 = query, P2 = query }).ToList();
            }

            return result;
        }

    }
}
