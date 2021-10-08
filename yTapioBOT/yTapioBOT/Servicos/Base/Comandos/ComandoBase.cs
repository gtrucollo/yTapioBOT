namespace yTapioBOT.Servicos
{
    using System;
    using Base;

    /// <summary>
    /// Classe ComandoBase
    /// </summary>
    public abstract class ComandoBase
    {
        #region Construtor
        /// <summary>
        /// Incia uma nova instância de <seealso cref="ComandoBase"/>
        /// </summary>
        /// <param name="canal">Objeto com as informações do canal</param>
        /// <param name="argumentos">Relação de argumentos do comando</param>
        public ComandoBase(CanalBase canal, params string[] argumentos)
        {
            this.Canal = canal;
            this.Argumentos = argumentos;
        }
        #endregion

        #region Propriedades
        /// <summary>
        /// Obtém Canal
        /// </summary>
        protected CanalBase Canal { get; }

        /// <summary>
        /// Obém Argumentos
        /// </summary>
        protected string[] Argumentos { get; }

        /// <summary>
        /// Obtém um valor que indica se é somente o comando é somente para moderadores
        /// </summary>
        protected virtual bool Moderador
        {
            get
            {
                return true;
            }
        }
        #endregion

        #region Métodos
        #region Públicos
        /// <summary>
        /// Executar
        /// </summary>
        /// <param name="channel">Objeto Services.ChannelBase</param>
        /// <param name="relacaoArgumentos">Relação de argumentos do comando</param>
        public abstract void Executar(CanalBase channel, params string[] relacaoArgumentos);
        #endregion
        #endregion

        #region Classes
        /// <summary>
        /// Classe IdAttribute
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
        public class IdAttribute : Attribute
        {
            #region Construtor
            /// <summary>
            /// Inicia uma nova Instância de <seealso cref="CommandAttribute"/>
            /// </summary>
            /// <param name="id">Valor do comando</param>
            public IdAttribute(char id)
            {
                this.Id = id;
            }
            #endregion

            #region Propriedades
            /// <summary>
            /// Obtém ou define Id
            /// </summary>
            public char Id { get; set; }
            #endregion
        }

        /// <summary>
        /// Classe NomeAttribute
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
        public class NomeAttribute : Attribute
        {
            #region Construtor
            /// <summary>
            /// Inicia uma nova Instância de <seealso cref="NomeAttribute"/>
            /// </summary>
            /// <param name="nome">Nome do comando</param>
            public NomeAttribute(string nome)
            {
                this.Nome = nome;
            }
            #endregion

            #region Propriedades
            /// <summary>
            /// Obtém ou define Id
            /// </summary>
            public string Nome { get; set; }
            #endregion
        }
        #endregion
    }
}