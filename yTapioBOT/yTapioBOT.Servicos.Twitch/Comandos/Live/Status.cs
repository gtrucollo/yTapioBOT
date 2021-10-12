namespace yTapioBOT.Servicos.Twitch.Comandos.Live
{
    using Base;

    /// <summary>
    /// Classe Status
    /// </summary>
    [Comando(Id.Exclamacao, "status", "Faz alguma coisa ai que ainda não sei nessa porra")]
    public sealed class Status : ComandoBase
    {
        #region Construtor
        /// <summary>
        /// Incia uma nova instância de <seealso cref="Adicionar"/>
        /// </summary>
        /// <param name="canal">Objeto com as informações do canal</param>
        /// <param name="argumentos">Relação de argumentos do comando</param>
        public Status(Canal canal, params string[] argumentos)
            : base(canal, argumentos)
        {
        }
        #endregion

        #region Propriedades
        /// <inheritdoc/>
        public override bool Moderador => false;
        #endregion

        #region Métodos
        #region Públicos
        /// <inheritdoc/>
        public override void Executar()
        {
            // TODO: Implementar comando
        }
        #endregion
        #endregion
    }
}