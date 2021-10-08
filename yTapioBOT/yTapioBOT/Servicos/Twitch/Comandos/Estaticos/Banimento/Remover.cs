namespace yTapioBOT.Servicos.Twitch.Comandos.Estaticos.Banimento
{
    using System.Linq;
    using Base.Comandos.Estaticos;

    /// <summary>
    /// Classe Remover
    /// </summary>
    [Nome("b")]
    public sealed class Remover : RemoverBase
    {
        #region Construtor
        /// <summary>
        /// Incia uma nova instância de <seealso cref="Adicionar"/>
        /// </summary>
        /// <param name="canal">Objeto com as informações do canal</param>
        /// <param name="argumentos">Relação de argumentos do comando</param>
        public Remover(Canal canal, params string[] argumentos)
            : base(canal, argumentos)
        {
        }
        #endregion

        #region Propriedades
        /// <summary>
        /// Obtém Nome
        /// </summary>
        private string Nome
        {
            get
            {
                return this.Argumentos.FirstOrDefault();
            }
        }
        #endregion

        #region Métodos
        #region Públicos
        /// <inheritdoc/>
        public override void Executar()
        {
            // Validar
            if (string.IsNullOrWhiteSpace(this.Nome))
            {
                this.Canal.SendChannelMessage("Argumentos inválidos, não foi informado o nome do usuário.");
                return;
            }

            // Executar
            this.Canal.SendChannelMessage(string.Format("/unban {0}", this.Nome));
        }
        #endregion
        #endregion
    }
}