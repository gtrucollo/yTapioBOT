namespace yTapioBOT.Servicos.Twitch
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Base;
    using yTapioBOT.BancoDados.Database;
    using yTapioBOT.Entidade.Database;

    /// <summary>
    /// Classe Service
    /// </summary>
    public sealed class Servico : ServicoBase
    {
        #region Construtor
        /// <summary>
        /// Inicia uma nova instância de <seealso cref="Servico"/>
        /// </summary>
        /// <param name="user">Controle para o usuario</param>
        /// <param name="password">Controle para a senha</param>
        /// <param name="clientId">Controle para o clientId</param>
        /// <param name="refreshToken">Controle para o refreshToken</param>
        public Servico(string user, string password, string clientId, string refreshToken)
            : base(user, password)
        {
            this.ClientId = clientId;
            this.RefreshToken = refreshToken;
        }
        #endregion

        #region Propriedades
        /// <summary>
        /// Obtém User
        /// </summary>
        public string User => this.usuario;

        /// <summary>
        /// Obtém Token
        /// </summary>
        public string Token => this.senha;

        /// <summary>
        /// Obtém ClientId
        /// </summary>
        public string ClientId { get; }

        /// <summary>
        /// Obtém AcessToken
        /// </summary>
        public string RefreshToken { get; }
        #endregion

        #region Métodos
        #region Públicos
        /// <inheritdoc />
        public override void Executar()
        {
            // Selecionar canais
            IList<Plataforma> listaRetorno = this.Database.Make<PlataformaDb, IList<Plataforma>>(bo => bo.SelecionarTodos());

            this.ListChannel.Add(new Canal("yTapioca", this));

            // Executar
            foreach (Canal channel in this.ListChannel)
            {
                new Task(() => channel.Executar()).Start();
            }
        }
        #endregion
        #endregion
    }
}