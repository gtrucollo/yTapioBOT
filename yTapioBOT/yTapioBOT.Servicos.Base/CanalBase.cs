namespace yTapioBOT.Servicos.Base
{
    using System;
    using BancoDados;
    using Database;

    /// <summary>
    /// Classe CanalBase
    /// </summary>
    public abstract class CanalBase : IDisposable
    {
        #region Campos
        /// <summary>
        /// Controle para channel
        /// </summary>
        private readonly string channel;

        /// <summary>
        /// Controle para o Banco de Dados
        /// </summary>
        private FactoryDatabase database;
        #endregion

        #region Construtor
        /// <summary>
        /// Inicia uma nova instância de <seealso cref="CanalBase"/>
        /// </summary>
        /// <param name="channel">Controle para o canal a ser conectado</param>
        public CanalBase(string channel)
        {
            this.channel = channel;
        }
        #endregion

        #region Propriedades
        /// <summary>
        /// Obtém Name
        /// </summary>
        public string Name => this.channel;

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