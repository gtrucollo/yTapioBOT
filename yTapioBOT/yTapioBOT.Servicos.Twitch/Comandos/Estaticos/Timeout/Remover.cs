namespace yTapioBOT.Servicos.Twitch.Comandos.Estaticos.Timeout
{
    using System.Linq;
    using Base;

    /// <summary>
    /// Classe Remover
    /// </summary>
    [Comando(Id.Remover, "p", "Remove se existir o timeout no usuário informado")]
    public sealed class Remover : ComandoBase
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
            this.Canal.SendChannelMessage(string.Format("/untimeout {0}", this.Nome));
        }
        #endregion
        #endregion
    }
}