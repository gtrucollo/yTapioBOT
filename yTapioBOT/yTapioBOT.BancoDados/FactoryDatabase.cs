namespace yTapioBOT.Database
{
    using System;
    using BancoDados;
    using Npgsql;

    /// <summary>
    /// Classe FactoryDatabase
    /// </summary>
    public class FactoryDatabase : IDisposable
    {
        #region Campos
        /// <summary>
        /// Sessão de controle
        /// </summary>
        private readonly NpgsqlConnection sessaoControle;
        #endregion

        #region Construtor
        /// <summary>
        /// Inicia uma nova instância da classe <see cref="FactoryBo"/>
        /// </summary>
        public FactoryDatabase()
            : this(null)
        {
        }

        /// <summary>
        /// Inicia uma nova instância da classe <see cref="FactoryBo"/>
        /// </summary>
        /// <param name="sessaoControle">Sessão de controle a ser utilizada</param>
        public FactoryDatabase(NpgsqlConnection sessaoControle)
        {
            if (sessaoControle == null)
            {
                this.sessaoControle = Sessao.SessaoControle;
                return;
            }

            this.sessaoControle = sessaoControle;
        }
        #endregion

        #region Métodos
        #region Database
        /// <summary>
        /// Obter a instância do banco de dados
        /// </summary>
        /// <typeparam name="TReturn">Tipo do retorno</typeparam>
        /// <param name="code">Ação a ser executada</param>
        /// <returns>Retorno da ação</returns>
        public TReturn Make<TType, TReturn>(Func<TType, TReturn> code)
        {
            return code((TType)Activator.CreateInstance(typeof(TType), this.sessaoControle));
        }

        /// <summary>
        /// Obter a instância do banco de dados
        /// </summary>
        /// <param name="code">Ação a ser executada</param>
        public void Make<TType>(Action<TType> code)
        {
            code((TType)Activator.CreateInstance(typeof(TType), this.sessaoControle));
        }
        #endregion

        #region IDisposable Members
        /// <summary>
        /// Método Dispose
        /// </summary>
        public void Dispose()
        {
            if (this.sessaoControle != null)
            {
                this.sessaoControle.Dispose();
            }

            // Formar limpeza da memoria
            GC.Collect();
            GC.SuppressFinalize(this);
        }
        #endregion
        #endregion
    }
}