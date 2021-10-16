namespace yTapioBOT.BancoDados.Base
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Dapper;
    using Entidade.Base;
    using Npgsql;

    /// <summary>
    /// Classe BaseDatabase
    /// </summary>
    public abstract class BaseDb<TType> where TType : EntidadeBase
    {
        #region Campos
        /// <summary>
        /// Controle para a conexão com o banco de dados
        /// </summary>
        private NpgsqlConnection sessaoControle;
        #endregion

        #region Construtor
        /// <summary>
        /// Inicia uma nova instância de <seealso cref="BaseDatabase"/>
        /// </summary>
        /// <param name="sessaoControle">Sessão com o banco de dados</param>
        public BaseDb(NpgsqlConnection sessaoControle)
        {
            this.sessaoControle = sessaoControle;
        }
        #endregion

        #region Propriedades
        /// <summary>
        /// Obtém SessaoControle
        /// </summary>
        protected NpgsqlConnection SessaoControle
        {
            get
            {
                if (sessaoControle == null)
                {
                    sessaoControle = Sessao.SessaoControle;
                    if (sessaoControle.State == ConnectionState.Open)
                    {
                        sessaoControle.Open();
                    }
                }

                return sessaoControle;
            }
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Obter o nome da tabela
        /// </summary>
        /// <returns>O nome da tabela no banco de dados</returns>
        public abstract string ObterTabelaNome();

        /// <summary>
        /// Obter o valor total de registros
        /// </summary>
        /// <returns>O Valor total de registros</returns>
        public long ObterRelacaoTotal()
        {
            // Using
            using NpgsqlCommand comando = new(null, this.SessaoControle);

            // Comando
            comando.CommandText = string.Format("SELECT COUNT(*) FROM {0}", this.ObterTabelaNome());

            // Retorno
            return (long)comando.ExecuteScalar();
        }

        /// <summary>
        /// Selecionar todos os registros
        /// </summary>
        /// <returns>A lista de registros encontrada</returns>
        public IList<TType> SelecionarTodos()
        {
            return this.SessaoControle.Query<TType>(string.Format("SELECT * FROM {0}", this.ObterTabelaNome())).ToList();
        }
        #endregion
    }
}