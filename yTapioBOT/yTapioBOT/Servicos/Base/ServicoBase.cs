namespace yTapioBOT.Servicos.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Classe ServicoBase
    /// </summary>
    public abstract class ServicoBase : IDisposable
    {
        #region Campos
        /// <summary>
        /// Controle para usuario
        /// </summary>
        protected readonly string usuario;

        /// <summary>
        /// Controle para senha
        /// </summary>
        protected readonly string senha;

        /// <summary>
        /// Controle para o listChannel
        /// </summary>
        private IList<CanalBase> listChannel;
        #endregion

        #region Construtor
        /// <summary>
        /// Inicia uma nova instância de <seealso cref="ServicoBase"/>
        /// </summary>
        /// <param name="usuario">Controle para o usuario</param>
        /// <param name="senha">Controle para a senha</param>
        public ServicoBase(string usuario, string senha)
        {
            this.usuario = usuario;
            this.senha = senha;
        }
        #endregion

        #region Propriedades
        /// <summary>
        /// Obtém ou define ListChannel
        /// </summary>
        public IList<CanalBase> ListChannel
        {
            get
            {
                if (this.listChannel == null)
                {
                    this.listChannel = new List<CanalBase>();
                }

                return this.listChannel;
            }

            set
            {
                this.listChannel = value;
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

            // Liberar memória
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

            // Disposing dos canais
            this.ListChannel.ToList().ForEach(x => x.Dispose());
        }
        #endregion

        #region Públicos
        /// <summary>
        /// Iniciar o serviço
        /// </summary>
        public abstract void Executar();
        #endregion
        #endregion
    }
}