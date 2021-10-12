namespace yTapioBOT.Servicos.Base
{
    using System;
    using System.ComponentModel;
    using Library;

    /// <summary>
    /// Classe ComandoBase
    /// </summary>
    public abstract class ComandoBase
    {
        #region Enumeradores
        /// <summary>
        /// Enumerador Id
        /// </summary>
        public enum Id
        {
            /// <summary>
            /// Id !
            /// </summary>
            [Description("!")]
            Exclamacao = 0,

            /// <summary>
            /// Id +
            /// </summary>
            [Description("+")]
            Adicionar = 1,

            /// <summary>
            /// Id -
            /// </summary>
            [Description("-")]
            Remover = 2
        }
        #endregion

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
        /// Obtém um valor que indica se o comando é somente para moderadores
        /// </summary>
        public virtual bool Moderador => true;

        /// <summary>
        /// Obtém Canal
        /// </summary>
        protected CanalBase Canal { get; }

        /// <summary>
        /// Obém Argumentos
        /// </summary>
        protected string[] Argumentos { get; }
        #endregion

        #region Métodos
        #region Públicos
        /// <summary>
        /// Executar ações do comando
        /// </summary>
        public virtual void Executar()
        {
        }
        #endregion
        #endregion

        #region Classes
        /// <summary>
        /// Classe ComandoAttribute
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
        public class ComandoAttribute : Attribute
        {
            #region Construtor
            /// <summary>
            /// Inicia uma nova Instância de <seealso cref="NomeAttribute"/>
            /// </summary>
            /// <param name="id">Enumerador Id</param>
            /// <param name="nome">Nome do comando</param>
            /// <param name="complemento">Breve descrição sobre o comando</param>
            public ComandoAttribute(Id id, string nome, string complemento)
            {
                this.Id = id;
                this.Nome = nome;
                this.Descricao = complemento;
            }
            #endregion

            #region Propriedades
            /// <summary>
            /// Obtém ou define Id
            /// </summary>
            public Id Id { get; set; }

            /// <summary>
            /// Obtém o valor de IdDescricao
            /// </summary>
            public string IdDescricao
            {
                get
                {
                    return Enumeradores.ObterDescricao(this.Id);
                }
            }

            /// <summary>
            /// Obtém ou define Nome
            /// </summary>
            public string Nome { get; set; }

            /// <summary>
            /// Obtém ou define Descricao
            /// </summary>
            public string Descricao { get; set; }
            #endregion
        }
        #endregion
    }
}