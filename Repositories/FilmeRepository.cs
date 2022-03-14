using FilmesAPIServer.Data;
using FilmesAPIServer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace FilmesAPIServer.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        private readonly SqlConnection sqlConnection;

        public FilmeRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Filmes>> Obter()
        {
            var filmesLista = new List<Filmes>();

            var query = "select * from Filmes order by Id";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                filmesLista.Add(new Filmes
                {
                    Id = (int)sqlDataReader["Id"],
                    Title = (string)sqlDataReader["Titulo"],
                    Desc = (string)sqlDataReader["Desc"],
                    Ano = (int)sqlDataReader["Ano"],
                    CapaUrl = (string)sqlDataReader["Capa"]
                });
            }

            await sqlConnection.CloseAsync();
            return filmesLista;
        }

        public async Task<Filmes> Obter(int id)
        {
            Filmes filme = null;

            var query = $"select * from Filmes where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(query,sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            if (sqlDataReader.Read())
            {
                filme = new Filmes
                {
                    Id = (int)sqlDataReader["Id"],
                    Title = (string)sqlDataReader["Titulo"],
                    Desc = (string)sqlDataReader["Desc"],
                    Ano = (int)sqlDataReader["Ano"],
                    CapaUrl = (string)sqlDataReader["Capa"]
                };
            }
            await sqlConnection.CloseAsync();
            return filme;
        }

        public async Task<List<Filmes>> ObterAno(int ano)
        {
            var filmes = new List<Filmes>();

            var query = $"select * from Filmes where Ano = '{ano}' order by Titulo ASC";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                filmes.Add(new Filmes
                {
                    Id = (int)sqlDataReader["Id"],
                    Title = (string)sqlDataReader["Titulo"],
                    Desc = (string)sqlDataReader["Desc"],
                    Ano = (int)sqlDataReader["Ano"],
                    CapaUrl = (string)sqlDataReader["Capa"]
                });
            }

            await sqlConnection.CloseAsync();

            return filmes;
        }

        public async Task<List<Filmes>> Obter(string title)
        {
            var filme = new List<Filmes>();
            var comando = $"select * from Filmes where Titulo = '{title}";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                filme.Add(new Filmes
                {
                    Id = (int)sqlDataReader["Id"],
                    Title = (string)sqlDataReader["Titulo"],
                    Desc = (string)sqlDataReader["Desc"],
                    Ano = (int)sqlDataReader["Ano"],
                    CapaUrl = (string)sqlDataReader["Capa"]
                });
            }

            await sqlConnection.CloseAsync();

            return filme;
        }

        public async Task Inserir(Filmes filmes)
        {
            var comando = "insert Filmes (Titulo, Desc, Ano, Capa) values" +
                $"('{filmes.Title}','{filmes.Desc}','{filmes.Ano}','{filmes.CapaUrl}')";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Atualizar( Filmes filme)
        {
            var comando = $"update Filmes set Titulo = '{filme.Title}', Desc = '{filme.Desc}', " +
                $"Ano = '{filme.Ano}', Capa = '{filme.CapaUrl}' where Id = '{filme.Id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Remover(int id)
        {
            var comando = $"delete from Filmes where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }
    }
}
