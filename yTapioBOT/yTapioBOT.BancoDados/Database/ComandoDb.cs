namespace yTapioBOT.BancoDados.Database
{
    using Base;
    using Entidade.Database;
    using Npgsql;

    /// <summary>
    /// Classe ComandoDb
    /// </summary>
    public class ComandoDb : BaseDb<Comando>
    {
        #region Construtor
        /// <summary>
        /// Inicia uma nova instância de <seealso cref="ComandoDb"/>
        /// </summary>
        /// <param name="sessaoControle">Sessão com o banco de dados</param>
        public ComandoDb(NpgsqlConnection sessaoControle)
            : base(sessaoControle)
        {
        }
        #endregion

        #region Métodos
        #region Públicos
        #endregion

        #region Diversos
        /// <inheritdoc />
        public override string ObterTabelaNome()
        {
            return "comando";
        }
        #endregion
        #endregion
    }
}