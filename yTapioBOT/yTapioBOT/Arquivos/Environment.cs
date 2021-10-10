namespace yTapioBOT.Arquivos
{
    using System;
    using System.Reflection;
    using DotNetEnv;

    /// <summary>
    /// Classe Environment
    /// </summary>
    public class Environment
    {
        #region Propriedades
        /// <summary>
        /// Obtém ou define TwitchUserName
        /// </summary>
        public string TwitchUserName { get; set; }

        /// <summary>
        /// Obtém ou define TwitchClientId
        /// </summary>
        public string TwitchClientId { get; set; }

        /// <summary>
        /// Obtém ou define TwitchAcessToken
        /// </summary>
        public string TwitchAcessToken { get; set; }

        /// <summary>
        /// Obtém ou define TwitchRefreshToken
        /// </summary>
        public string TwitchRefreshToken { get; set; }

        /// <summary>
        /// Obtém ou define TwitchAccountToken
        /// </summary>
        public string TwitchToken { get; set; }

        /// <summary>
        /// Obtém ou define DatabaseHost
        /// </summary>
        public string DatabaseHost { get; set; }

        /// <summary>
        /// Obtém ou define DatabaseHost
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// Obtém ou define DatabaseHost
        /// </summary>
        public string DatabaseUser { get; set; }

        /// <summary>
        /// Obtém ou define DatabaseHost
        /// </summary>
        public string DatabasePassword { get; set; }

        /// <summary>
        /// Obtém ou define DatabaseHost
        /// </summary>
        public string DatabasePort { get; set; }
        #endregion

        #region Métodos
        /// <summary>
        /// Faz o carregamento dos dados do arquivo .env
        /// </summary>
        /// <returns>A classe Environment instânciada</returns>
        public static Environment Carregar()
        {
            Environment env = new();

            // Carregar .env
            Env.Load();

            // Atualizar classe com os dados do arquivo carregado
            foreach (PropertyInfo propriedade in env.GetType().GetProperties())
            {
                try
                {
                    propriedade.SetValue(env, Env.GetString(propriedade.Name));
                }
                catch (Exception exp)
                {
                    Console.WriteLine(string.Format(
                        "Ocorreu um erro ao carregar os dados do .env{0}" +
                        "Campo .:{1}{0}" +
                        "Erro  .:{2}",
                        System.Environment.NewLine,
                        propriedade.Name,
                        exp.Message));
                }
            }

            // Retorno
            return env;
        }
        #endregion
    }
}