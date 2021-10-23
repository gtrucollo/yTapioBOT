namespace yTapioBOT.Entidade.Database
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Dapper.Database.Attributes;
    using Enumeradores;

    /// <summary>
    /// Classe Plataforma
    /// </summary>
    [Table("plataforma")]
    public class Plataforma
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
        /// Obtém ou define Id
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Obtém ou define Lancamento
        /// </summary>
        [Column("lancamento")]
        public DateTimeOffset Lancamento { get; set; }

        /// <summary>
        /// Obtém ou define Alteracao
        /// </summary>
        [Column("alteracao")]
        public DateTimeOffset Alteracao { get; set; }

        /// <summary>
        /// Obtém ou define Tipo
        /// </summary>
        [Column("tipo")]
        public byte Tipo { get; set; }

        /// <summary>
        /// Obtém TipoFormatado
        /// </summary>
        [Ignore]
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
        [Column("status")]
        public byte Status { get; set; }

        /// <summary>
        /// Obtém StatusFormatado
        /// </summary>
        [Ignore]
        public AtivoInativo StatusFormatado
        {
            get
            {
                return (AtivoInativo)this.Status;
            }
        }

        /// <summary>
        /// Obtém ou define Url
        /// </summary>
        [Column("url")]
        public string Url { get; set; }
        #endregion
    }
}