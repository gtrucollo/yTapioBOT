namespace yTapioBOT.Entidade.Base
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Classe EntidadeBase
    /// </summary>
    public abstract class EntidadeBase
    {
        #region Construtor
        /// <summary>
        /// Inicia uma nova Instância de <seealso cref="EntidadeBase"/>
        /// </summary>
        public EntidadeBase()
        {
            this.Id = Guid.NewGuid();
        }
        #endregion

        #region Propriedades
        /// <summary>
        /// Obtém ou define Id
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        #endregion
    }
}