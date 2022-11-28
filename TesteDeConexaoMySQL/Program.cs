using MySqlConnector;
using System.Diagnostics;

namespace TesteDeConexaoMySQL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Início do Teste de conexão com banco de dados");
            var tempoConexao = new Stopwatch();
            var tempoDeExecucaoQuery = new Stopwatch();
            try
            {
                MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();

                builder.Server = "endereco_servidor";
                builder.Port = 4406;
                builder.UserID = "seu_usuario";
                builder.Password = "sua_senha";
                builder.Database = "sua_base_de_dados";

                tempoDeExecucaoQuery.Start();
                using (MySqlConnection connection = new MySqlConnection(builder.ConnectionString))
                {
                    tempoConexao.Start();
                    connection.Open();
                    tempoConexao.Stop();

                    var sql = "SELECT * FROM sua_tabela Limit 1";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"A consulta foi executada com sucesso e retornou o valor de {reader.GetValue(0)}");
                                tempoDeExecucaoQuery.Stop();
                            }
                        }

                        Console.WriteLine($"O tempo para conectar na base foi de {tempoConexao.Elapsed.TotalSeconds} segundos e o tempo de resposta da consulta no banco de dados foi de {tempoDeExecucaoQuery.Elapsed.TotalSeconds} segundos");
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