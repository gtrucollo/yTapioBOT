namespace yTapioBOT.Servicos.Base.Comandos.Estaticos
{
    /// <summary>
    /// Classe AdicionarBase
    /// </summary>
    [Id('+')]
    public abstract class AdicionarBase : ComandoBase
    {
        #region Construtor
        /// <summary>
        /// Incia uma nova instância de <seealso cref="AdicionarBase"/>
        /// </summary>
        /// <param name="canal">Objeto com as informações do canal</param>
        /// <param name="argumentos">Relação de argumentos do comando</param>
        public AdicionarBase(CanalBase canal, params string[] argumentos)
            : base(canal, argumentos)
        {
        }
        #endregion
    }
}