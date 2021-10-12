namespace yTapioBOT.Servicos.Twitch.Comandos.Live
{
    /// <summary>
    /// Classe Status
    /// </summary>
    [Comando(Id.Exclamacao, "comandos", "Mostra todos os comandos disponiveis")]
    public sealed class Comandos : ComandoBase
    {
        #region Construtor
        /// <summary>
        /// Incia uma nova instância de <seealso cref="Adicionar"/>
        /// </summary>
        /// <param name="canal">Objeto com as informações do canal</param>
        /// <param name="argumentos">Relação de argumentos do comando</param>
        public Comandos(Canal canal, params string[] argumentos)
            : base(canal, argumentos)
        {
        }
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