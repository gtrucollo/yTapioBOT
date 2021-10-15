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

            // Migrations
            services.AddFluentMigratorCore()
                .ConfigureRunner(conf => conf
                    .AddPostgres()
                    .WithGlobalConnectionString(Sessao.Url)
                    .ScanIn(typeof(Sessao).Assembly).For.Migrations());
            services.AddTransient<MigrationController>();
            services.BuildServiceProvider()
                    .GetService<MigrationController>().Executar();

            // Iniciar conexão
            SessaoControle.Open();
        }
        #endregion
    }
}