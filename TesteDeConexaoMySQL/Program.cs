using MySqlConnector;
using System.Diagnostics;

namespace TesteDeConexaoMySQL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Início do Teste de conexão com banco de dados");
            var tempo = new Stopwatch();
            try
            {
                MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();

                builder.Server = "endereco_servidor";
                builder.Port = 4406;
                builder.UserID = "seu_usuario";
                builder.Password = "sua_senha";
                builder.Database = "sua_base_de_dados";

                tempo.Start();
                using (MySqlConnection connection = new MySqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    var sql = "SELECT * FROM sua_tabela Limit 1";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"A consulta foi executada com sucesso e retornou o valor de {reader.GetValue(0)}");
                                tempo.Stop();
                            }
                        }

                        Console.WriteLine($"O tempo de resposta da consulta no banco de dados foi de {tempo.Elapsed.TotalSeconds} segundos");
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Ocorreu um erro ao tentar se conectar ao banco de dados {e.ToString()}");
            }
        }
    }
}