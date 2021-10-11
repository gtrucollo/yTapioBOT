namespace yTapioBOT.Servicos.Twitch.Comandos.Live
{
    using Base.Comandos.Estaticos;

    /// <summary>
    /// Classe Status
    /// </summary>
    [Nome("status")]
    public sealed class Status : LiveBase
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

        #region Métodos
        #region Públicos
        /// <inheritdoc/>
        public override void Executar()
        {

        }
        #endregion
        #endregion
    }
}