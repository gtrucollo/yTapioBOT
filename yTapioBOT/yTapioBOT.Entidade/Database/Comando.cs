namespace yTapioBOT.Entidade.Database
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Classe Comando
    /// </summary>
    [Table("comando")]
    public class Comando
    {
        #region Propriedades
        /// <summary>
        /// Obtém ou define Id
        /// </summary>
        [Key]
        [Column("id")]
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
        /// Obtém IdPlataforma
        /// </summary>
        [Column("id_plataforma")]
        public Guid IdPlataforma { get; set; }

        /// <summary>
        /// Obtém ou define Nome
        /// </summary>
        [Column("nome")]
        public string Nome { get; set; }

        /// <summary>
        /// Obtém ou define Conteudo
        /// </summary>
        [Column("conteudo")]
        public string Conteudo { get; set; }

        /// <summary>
        /// Obtém ou define Contagem
        /// </summary>
        [Column("contagem")]
        public int? Contagem { get; set; }

        /// <summary>
        /// Obtém ou define Administrador
        /// </summary>
        [Column("administrador")]
        public bool Administrador { get; set; }
        #endregion
    }
}