namespace yTapioBOT.BancoDados.Migrations
{
    using System;
    using FluentMigrator.Runner;

    /// <summary>
    /// Classe MigrationController
    /// </summary>
    public class MigrationController
    {
        #region Campos
        /// <summary>
        /// Controle para migrationRunner
        /// </summary>
        private readonly IMigrationRunner migrationRunner;
        #endregion

        #region Construtor
        /// <summary>
        /// Inicia uma nova instância de <seealso cref="MigrationController"/>
        /// </summary>
        /// <param name="migrationRunner">Parâmetro IMigrationRunner</param>
        public MigrationController(IMigrationRunner migrationRunner)
        {
            this.migrationRunner = migrationRunner;
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Executar ações das migrations
        /// </summary>
        public void Executar()
        {
            try
            {
                this.migrationRunner.MigrateUp();
            }
            catch (Exception exp)
            {
                Console.WriteLine(string.Format(
                  "Ocorreu um erro não tratado na execução das migragions.{0}" +
                  "Detalhes do erro .: {1}",
                  Environment.NewLine,
                  exp.Message));
            }
        }
        #endregion
    }
}
