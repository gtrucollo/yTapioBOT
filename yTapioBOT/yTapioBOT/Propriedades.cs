namespace yTapioBOT
{
    using Arquivos;

    /// <summary>
    /// Classe Propriedades
    /// </summary>
    public static class Propriedades
    {
        #region Campos
        /// <summary>
        /// Controle para env
        /// </summary>
        private static Environment env;
        #endregion

        #region Propriedades
        /// <summary>
        /// Obtém Environment
        /// </summary>
        public static Environment Env
        {
            get
            {
                // Carregar
                if (env == null)
                {
                    env = Environment.Carregar();
                }

                // Retorno
                return env;
            }
        }
        #endregion
    }
}