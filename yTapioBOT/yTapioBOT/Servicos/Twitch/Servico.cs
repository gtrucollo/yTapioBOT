namespace yTapioBOT.Servicos.Twitch
{
    using System.Threading.Tasks;
    using Base;

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
        public Servico(string user, string password, string clientId, string acessToken)
            : base(user, password)
        {
            this.ClientId = clientId;
            this.AcessToken = acessToken;
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
        public string AcessToken { get; }
        #endregion

        #region Métodos
        #region Públicos
        /// <inheritdoc />
        public override void Executar()
        {
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