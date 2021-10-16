namespace yTapioBOT.BancoDados
{
    using FluentMigrator.Runner;
    using Microsoft.Extensions.DependencyInjection;
    using Migrations;
    using Npgsql;

    /// <summary>
    /// Classe Sessao
    /// </summary>
    public static class Sessao
    {
        #region Campos
        /// <summary>
        /// Controle para a conexão com o banco de dados
        /// </summary>
        private static NpgsqlConnection sessaoControle;
        #endregion

        #region Propriedades
        /// <summary>
        /// Obtém ou define SessaoControle
        /// </summary>
        public static string Url { get; set; }

        /// <summary>
        /// Obtém DatabaseName
        /// </summary>
        public static string DatabaseName
        {
            get
            {
                // Validar
                if (!Sessao.Url.Contains("Database="))
                {
                    return null;
                }

                // Selecionar nome do banco de dados
                int indexDatabaseName = (Sessao.Url.IndexOf("Database=") + 9);

                // Retorno
                return Sessao.Url[indexDatabaseName..Sessao.Url.IndexOf(";", indexDatabaseName)];
            }
        }

        /// <summary>
        /// Obtém SessaoControle
        /// </summary>
        public static NpgsqlConnection SessaoControle
        {
            get
            {
                if (sessaoControle == null)
                {
                    sessaoControle = new NpgsqlConnection(Sessao.Url);
                }

                return sessaoControle;
            }
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Inicializar conexão com o banco de dados
        /// </summary>
        /// <param name="url">Url de conexão com o banco de dados</param>
        public static void Inicializar(IServiceCollection services, string url)
        {
            // Atualizar
            Sessao.Url = url;

            // Validar banco de dados
            if (!VerificarBancoDadosExistante(Sessao.DatabaseName))
            {
                CriarBancoDados(Sessao.DatabaseName);
            }

            // Liberar conexões anteriores
            NpgsqlConnection.ClearAllPools();

            // Iniciar conexão
            SessaoControle.Open();

            // Migrations
            services.AddFluentMigratorCore()
                .ConfigureRunner(conf => conf
                    .AddPostgres()
                    .WithGlobalConnectionString(Sessao.Url)
                    .ScanIn(typeof(Sessao).Assembly).For.Migrations());
            services.AddTransient<MigrationController>();
            services.BuildServiceProvider()
                    .GetService<MigrationController>().Executar();
        }

        /// <summary>
        /// Verificar se o banco de dados existe
        /// </summary>
        /// <param name="bancoDados">Nome do banco de dados</param>
        /// <returns>Se o banco de dados existe</returns>
        private static bool VerificarBancoDadosExistante(string bancoDados)
        {
            // Comandos
            using NpgsqlConnection connection = new(Sessao.Url.Replace(string.Format("Database={0};", Sessao.DatabaseName), string.Empty));
            connection.Open();
            using NpgsqlCommand command = new(string.Format("SELECT COUNT(*) FROM PG_DATABASE WHERE DATNAME = '{0}'", bancoDados), connection);

            // Retorno
            return (long)command.ExecuteScalar() > 0;
        }

        /// <summary>
        /// Criar o banco de dados
        /// </summary>
        /// <param name="bancoDados">Nome do banco de dados</param>
        private static void CriarBancoDados(string bancoDados)
        {
            // Comando
            using NpgsqlConnection connection = new(Sessao.Url.Replace(string.Format("Database={0};", Sessao.DatabaseName), string.Empty));
            connection.Open();

            using NpgsqlCommand command = new(string.Format("CREATE DATABASE {0} WITH OWNER = postgres ENCODING = 'UTF8';", bancoDados), connection);

            // Retorno
            command.ExecuteNonQuery();
        }
        #endregion
    }
}