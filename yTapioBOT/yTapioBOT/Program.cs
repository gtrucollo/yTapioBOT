namespace yTapioBOT
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Classe Program
    /// </summary>
    public sealed class Program
    {
        #region Métodos
        /// <summary>
        /// Método Main
        /// </summary>
        public static void Main()
        {
            try
            {
                // Iniciar serviços
                new Servicos.Twitch.Servico(
                    Propriedades.Env.TwitchUserName,
                    Propriedades.Env.TwitchToken,
                    Propriedades.Env.TwitchClientId,
                    Propriedades.Env.TwitchRefreshToken)
                    .Executar();
            }
            catch (Exception exp)
            {
                Console.WriteLine(string.Format(
                    "Ocorreu um erro não tratado na execução da aplicacação.{0}" +
                    "Detalhes do erro .: {1}",
                    Environment.NewLine,
                    exp.Message));
            }
            finally
            {
                // Aguardar procoesso ser finalizado
                Process.GetCurrentProcess().WaitForExit();
            }
        }
        #endregion
    }
}