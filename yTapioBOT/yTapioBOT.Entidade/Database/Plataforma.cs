namespace yTapioBOT.Entidade.Database
{
    using System;
    using Base;
    using Dapper.Contrib.Extensions;

    /// <summary>
    /// Classe Plataforma
    /// </summary>
    public class Plataforma : EntidadeBase
    {
        #region Enumeradores
        /// <summary>
        /// Enumerador TipoEnum
        /// </summary>
        public enum TipoEnum
        {
            /// <summary>
            /// Tipo Twitch
            /// </summary>
            Twitch = 0,

            /// <summary>
            /// Tipo Discord
            /// </summary>
            Discord = 1
        }
        #endregion

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
        /// Obtém TipoFormatado
        /// </summary>
        [Write(false)]
        public TipoEnum TipoFormatado
        {
            get
            {
                return (TipoEnum)this.Tipo;
            }
        }

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