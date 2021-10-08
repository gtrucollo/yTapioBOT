namespace yTapioBOT.Servicos.Twitch.Comandos.Estaticos.Timeout
{
    using System.Linq;
    using Base.Comandos.Estaticos;

    /// <summary>
    /// Classe Adicionar
    /// </summary>
    [Nome("p")]
    public sealed class Adicionar : AdicionarBase
    {
        #region Construtor
        /// <summary>
        /// Incia uma nova instância de <seealso cref="Adicionar"/>
        /// </summary>
        /// <param name="canal">Objeto com as informações do canal</param>
        /// <param name="argumentos">Relação de argumentos do comando</param>
        public Adicionar(Canal canal, params string[] argumentos)
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

        /// <summary>
        /// Obtém Tempo
        /// </summary>
        private string Tempo
        {
            get
            {
                // Validar
                if (this.Argumentos.Length < 2)
                {
                    return "1";
                }

                // Retorno
                return this.Argumentos[1];
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
            this.Canal.SendChannelMessage(string.Format("/timeout {0} {1}", this.Nome, this.Tempo));
        }
        #endregion
        #endregion
    }
}