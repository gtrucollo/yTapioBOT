namespace yTapioBOT.Servicos.Base.Comandos
{
    /// <summary>
    /// Classe RemoverBase
    /// </summary>
    [Id('-')]
    public abstract class RemoverBase : ComandoBase
    {
        #region Construtor
        /// <summary>
        /// Incia uma nova instância de <seealso cref="RemoverBase"/>
        /// </summary>
        /// <param name="canal">Objeto com as informações do canal</param>
        /// <param name="argumentos">Relação de argumentos do comando</param>
        public RemoverBase(CanalBase canal, params string[] argumentos)
            : base(canal, argumentos)
        {
        }
        #endregion
    }
}