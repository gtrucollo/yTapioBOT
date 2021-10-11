namespace yTapioBOT.Servicos.Base.Comandos.Estaticos
{
    /// <summary>
    /// Classe BancoDadosBase
    /// </summary>
    [Id('!')]
    public abstract class LiveBase : ComandoBase
    {
        #region Construtor
        /// <summary>
        /// Incia uma nova instância de <seealso cref="LiveBase"/>
        /// </summary>
        /// <param name="canal">Objeto com as informações do canal</param>
        /// <param name="argumentos">Relação de argumentos do comando</param>
        public LiveBase(CanalBase canal, params string[] argumentos)
            : base(canal, argumentos)
        {
        }
        #endregion
    }
}