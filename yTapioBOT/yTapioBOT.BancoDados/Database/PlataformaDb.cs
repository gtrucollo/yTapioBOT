namespace yTapioBOT.BancoDados.Database
{
    using Base;
    using Entidade.Database;
    using Npgsql;

    /// <summary>
    /// Classe PlataformaDb
    /// </summary>
    public class PlataformaDb : BaseDb<Plataforma>
    {
        #region Construtor
        /// <summary>
        /// Inicia uma nova instância de <seealso cref="PlataformaDb"/>
        /// </summary>
        /// <param name="sessaoControle">Sessão com o banco de dados</param>
        public PlataformaDb(NpgsqlConnection sessaoControle)
            : base(sessaoControle)
        {
        }
        #endregion

        #region Métodos
        /// <inheritdoc />
        public override string ObterTabelaNome()
        {
            return "plataforma";
        }
        #endregion
    }
}