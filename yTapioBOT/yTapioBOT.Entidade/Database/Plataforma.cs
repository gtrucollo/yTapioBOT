namespace yTapioBOT.Entidade.Database
{
    using System;
    using Base;

    /// <summary>
    /// Classe Plataforma
    /// </summary>
    public partial class Plataforma : EntidadeBase
    {
        #region Propriedades
        /// <summary>
        /// Obtém ou define Lancamento
        /// </summary>
        public DateTimeOffset Lancamento { get; set; }

        /// <summary>
        /// Obtém ou define Alteracao
        /// </summary>
        public DateTimeOffset Alteracao { get; set; }

        /// <summary>
        /// Obtém ou define Tipo
        /// </summary>
        public byte Tipo { get; set; }

        /// <summary>
        /// Obtém ou define Status
        /// </summary>
        public byte Status { get; set; }

        /// <summary>
        /// Obtém ou define Url
        /// </summary>
        public string Url { get; set; }
        #endregion
    }
}