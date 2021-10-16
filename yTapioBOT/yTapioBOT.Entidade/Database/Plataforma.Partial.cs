namespace yTapioBOT.Entidade.Database
{
    using Enumeradores;

    /// <summary>
    /// Classe Plataforma
    /// </summary>
    public partial class Plataforma
    {
        #region Enumeradores
        /// <summary>
        /// Enumerador TipoEnum
        /// </summary>
        public enum TipoEnum : byte
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
        /// Obtém StatusFormatado
        /// </summary>
        public AtivoInativo StatusFormatado
        {
            get
            {
                return (AtivoInativo)this.Status;
            }
        }

        /// <summary>
        /// Obtém TipoFormatado
        /// </summary>
        public TipoEnum TipoFormatado
        {
            get
            {
                return (TipoEnum)this.Tipo;
            }
        }
        #endregion
    }
}