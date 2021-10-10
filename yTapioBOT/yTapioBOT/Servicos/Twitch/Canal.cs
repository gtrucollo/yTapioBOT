﻿namespace yTapioBOT.Servicos.Twitch
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Base;

    /// <summary>
    /// Classe Canal
    /// </summary>
    public sealed class Canal : CanalBase
    {
        #region Campos
        /// <summary>
        /// Controle para Canal
        /// </summary>
        private TwitchClient client;

        /// <summary>
        /// Controle para twitch
        /// </summary>
        private TwitchAPI twitch;
        #endregion

        #region Construtor
        /// <summary>
        /// Inicia uma nova instância de <seealso cref="Canal"/>
        /// </summary>
        /// <param name="Canal">Controle para o usuario</param>
        /// <param name="password">Controle para a senha</param>
        public Canal(string channel, Servico service)
            : base(channel)
        {
            // Iniciar
            this.client = new TwitchClient();
            this.client.Initialize(new ConnectionCredentials(service.User, service.Token), channel);

            // API
            this.twitch = new TwitchAPI();
            this.twitch.Settings.ClientId = Propriedades.Env.TwitchClientId;
            this.twitch.Settings.AccessToken = this.twitch.Auth.GetAccessToken(Propriedades.Env.TwitchRefreshToken);

            // Atualizar
            this.client.AddChatCommandIdentifier('!');
            this.client.AddChatCommandIdentifier('-');
            this.client.AddChatCommandIdentifier('+');

            // Eventos
            this.client.OnConnected += this.Client_OnConnected;
            this.client.OnFailureToReceiveJoinConfirmation += this.Client_OnFailureToReceiveJoinConfirmation;
            this.client.OnConnectionError += this.Client_OnConnectionError;
            this.client.OnMessageReceived += this.Client_OnMessageReived;
            this.client.OnChatCommandReceived += this.Client_OnChatCommandReceived;
            this.client.OnJoinedChannel += this.Client_OnJoinedChannel;


            this.GetChannelStatus();
        }
        #endregion

        #region Métodos
        #region Dispose
        /// <inheritdoc />
        public override void Dispose(bool disposing)
        {
            // Base
            base.Dispose(disposing);

            // Validar
            if (!disposing)
            {
                return;
            }

            // Atualizar
            this.client.Disconnect();
            this.client = null;
        }
        #endregion

        #region Públicos
        /// <inheritdoc />
        public override void Executar()
        {
            // Validar
            if (this.client == null)
            {
                throw new Exception("Ocorreu um erro ao instânciar os dados da cliente");
            }

            // Connectar
            this.client.Connect();
        }

        /// <inheritdoc />
        public override void SendChannelMessage(string message)
        {
            this.client.SendMessage(this.Name, message);
        }

        public string GetChannelStatus()
        {
            return this.twitch.V5.Channels.GetChannelAsync(this.client.ConnectionCredentials.TwitchOAuth).Result.Status;
        }
        #endregion

        #region Privados
        /// <summary>
        /// Método Client_OnConnected
        /// </summary>
        /// <param name="sender">Parâmetro sender</param>
        /// <param name="e">Parâmetro OnConnectedArgs</param>
        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            this.client.JoinChannel(this.Name);
        }

        /// <summary>
        /// Método OnJoinedChannel
        /// </summary>
        /// <param name="sender">Parâmetro sender</param>
        /// <param name="e">Parâmetro OnJoinedChannelArgs</param>
        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            Console.WriteLine($"[Joined Channel] .: {e.Channel}");
        }

        /// <summary>
        /// Método Client_OnConnected
        /// </summary>
        /// <param name="sender">Parâmetro sender</param>
        /// <param name="e">Parâmetro OnConnectionErrorArgs</param>
        private void Client_OnConnectionError(object sender, OnConnectionErrorArgs e)
        {
            Console.WriteLine($"[Connection Error] .: {this.Name}");
        }

        /// <summary>
        /// Método OnFailureToReceiveJoinConfirmation
        /// </summary>
        /// <param name="sender">Parâmetro sender</param>
        /// <param name="e">Parâmetro OnFailureToReceiveJoinConfirmationArgs</param>
        private void Client_OnFailureToReceiveJoinConfirmation(object sender, OnFailureToReceiveJoinConfirmationArgs e)
        {
            Console.WriteLine("[Failure Join]");
            Console.WriteLine($"     Channel .:{e.Exception.Channel}");
            Console.WriteLine($"     Details .:{e.Exception.Details}");
        }

        /// <summary>
        /// Método Client_OnMessageReived
        /// </summary>
        /// <param name="sender">Parâmetro sender</param>
        /// <param name="e">Parâmetro OnMessageReceivedArgs</param>
        private void Client_OnMessageReived(object sender, OnMessageReceivedArgs e)
        {
        }

        /// <summary>
        /// Método OnChatCommandReceived
        /// </summary>
        /// <param name="sender">Parâmetro sender</param>
        /// <param name="e">Parâmetro OnChatCommandReceivedArgs</param>
        private void Client_OnChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            try
            {
                // Validar
                if (string.IsNullOrWhiteSpace(e.Command.CommandText))
                {
                    this.SendChannelMessage("Argumentos inválidos, não foi informado o nome do comando.");
                }

                // Obter classe
                Type typeClasseComando = Assembly.GetAssembly(typeof(ComandoBase))
                    .GetTypes()
                    .Where(x => x.GetCustomAttribute<ComandoBase.IdAttribute>()?.Id == e.Command.CommandIdentifier)
                    .Where(x => x.GetCustomAttribute<ComandoBase.NomeAttribute>(true)?.Nome == e.Command.CommandText)
                    .Select(x => x)
                    .FirstOrDefault();
                if (typeClasseComando == null)
                {
                    throw new Exception("Nenhuma implementação foi localizada.");
                }

                // Instânciar classe
                string[] argumentos = e.Command.ArgumentsAsList.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                if (Activator.CreateInstance(typeClasseComando, this, argumentos) is not ComandoBase classeComando)
                {
                    throw new Exception("Não foi possivel instânciar a classe");
                }

                // Executar
                classeComando.Executar();
            }
            catch (Exception exp)
            {
                Console.WriteLine(string.Format(
                   "Não foi possivel executar o comando: {1}{0}" +
                   "Detalhes .: {2}{0}",
                   Environment.NewLine,
                   e.Command.CommandText,
                   exp.Message));
            }
        }
        #endregion
        #endregion
    }
}