namespace yTapioBOT.Servicos.Base
{
    using System;
    using BancoDados;
    using Database;
    using Entidade.Database;

    /// <summary>
    /// Classe CanalBase
    /// </summary>
    public abstract class CanalBase : IDisposable
    {
        #region Campos
        /// <summary>
        /// Controle para canal
        /// </summary>
        private readonly Plataforma canal;

        /// <summary>
        /// Controle para o Banco de Dados
        /// </summary>
        private FactoryDatabase database;
        #endregion

        #region Construtor
        /// <summary>
        /// Inicia uma nova instância de <seealso cref="CanalBase"/>
        /// </summary>
        /// <param name="canal">Controle para o canal a ser conectado</param>
        public CanalBase(Plataforma canal)
        {
            this.canal = canal;
        }
        #endregion

        #region Propriedades
        /// <summary>
        /// Obtém Id
        /// </summary>
        public Guid Id => this.canal.Id;

        /// <summary>
        /// Obtém Name
        /// </summary>
        public string Name => this.canal.Url;

        // <summary>
        /// Controle para o banco de dados
        /// </summary>
        public FactoryDatabase Database
        {
            get
            {
                if (this.database == null)
                {
                    this.database = new FactoryDatabase(Sessao.SessaoControle);
                }

                // Retorno
                return this.database;
            }
        }
        #endregion

        #region Métodos
        #region Dispose
        /// <summary>
        /// Método dispose
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Método dispose
        /// </summary>
        /// <param name="disposing">Controle para informar se está no dispose</param>
        public virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }
        }
        #endregion

        #region Públicos
        /// <summary>
        /// Iniciar o serviço
        /// </summary>
        public abstract void Executar();

        /// <summary>
        /// Enviar mensagem no chat do serviço
        /// </summary>
        /// <param name="message">Mensagem a ser enviada</param>
        public abstract void SendChannelMessage(string message);
        #endregion
        #endregion
    }
}