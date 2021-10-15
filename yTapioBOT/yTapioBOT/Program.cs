namespace yTapioBOT
{
    using System;
    using System.Diagnostics;
    using Microsoft.Extensions.DependencyInjection;
    using yTapioBOT.BancoDados;

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
                // Banco de dados
                Sessao.Inicializar(new ServiceCollection(), Propriedades.Env.DatabaseUrl);

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