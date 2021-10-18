namespace yTapioBOT.Entidade.Database
{
    using System;
    using Base;

    /// <summary>
    /// Classe Comando
    /// </summary>
    public class Comando : EntidadeBase
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
        /// Obtém ou define Conteudo
        /// </summary>
        public string Conteudo { get; set; }

        /// <summary>
        /// Obtém ou define Contagem
        /// </summary>
        public int Contagem { get; set; }

        /// <summary>
        /// Obtém ou define Administrador
        /// </summary>
        public bool Administrador { get; set; }
        #endregion
    }
}