namespace yTapioBOT.BancoDados.Database
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Dapper.Database.Extensions;
    using Entidade.Database;
    using Enumeradores;
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
        #region Públicos
        /// <summary>
        /// Selecionar todos os registros
        /// </summary>
        /// <param name="status">Enumerador AtivoInativo</param>
        /// <param name="tipo">Enumerador Tipo</param>
        /// <returns>A lista de registros</returns>
        public IList<Plataforma> SelecionarTodos(AtivoInativo status, Plataforma.TipoEnum tipo)
        {
            // Query
            return this.SessaoControle
                .GetList<Plataforma>("WHERE (status = @Status) AND (tipo = @Tipo)", new { Status = (byte)status, Tipo = (byte)tipo })
                .ToList();
        }
        #endregion
        #endregion
    }
}