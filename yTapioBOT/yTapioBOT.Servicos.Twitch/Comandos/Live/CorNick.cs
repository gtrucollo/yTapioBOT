namespace yTapioBOT.Servicos.Twitch.Comandos.Estaticos.Banimento
{
    using System.Linq;
    using Base;

    /// <summary>
    /// Classe CorNick
    /// </summary>
    [Comando(Id.Exclamacao, "c", "Altera a cor do meu nick")]
    public sealed class CorNick : ComandoBase
    {
        #region Construtor
        /// <summary>
        /// Incia uma nova instância de <seealso cref="Adicionar"/>
        /// </summary>
        /// <param name="canal">Objeto com as informações do canal</param>
        /// <param name="argumentos">Relação de argumentos do comando</param>
        public CorNick(Canal canal, params string[] argumentos)
            : base(canal, argumentos)
        {
        }
        #endregion

        #region Propriedades
        /// <inheritdoc/>
        public override bool Moderador => false;

        /// <summary>
        /// Obtém Cor
        /// </summary>
        private string Cor
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
            if (string.IsNullOrWhiteSpace(this.Cor))
            {
                this.Canal.SendChannelMessage("Argumentos inválidos, não foi informado o a cor a ser utilizada no meu nick.");
                return;
            }

            // Executar
            this.Canal.SendChannelMessage(string.Format("/color {0}", this.Cor));
        }
        #endregion
        #endregion
    }
}