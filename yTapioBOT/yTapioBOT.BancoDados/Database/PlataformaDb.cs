namespace yTapioBOT.BancoDados.Database
{
    using System.Collections.Generic;
    using System.Data;
    using Base;
    using Dapper;
    using Entidade.Database;
    using Enumeradores;
    using Library;
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
            // Parâmetros
            DynamicParameters parametros = new();
            parametros.Add(nameof(Plataforma.Status).ToPascalCase(), (byte)status, DbType.Byte);
            parametros.Add(nameof(Plataforma.Tipo).ToPascalCase(), (byte)tipo, DbType.Byte);

            // Query
            return this.SessaoControle
                .Query<Plataforma>($"SELECT * FROM {this.ObterTabelaNome()} WHERE (status = @Status) AND (tipo = @Tipo)", parametros)
                .AsList();
        }
        #endregion

        #region Diversos
        /// <inheritdoc />
        public override string ObterTabelaNome()
        {
            return "plataforma";
        }
        #endregion
        #endregion
    }
}