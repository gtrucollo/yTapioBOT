namespace yTapioBOT.Servicos.Twitch
{
    using System.ComponentModel;
    using System.Linq;
    using Entidade.Database;
    using Library;
    using TwitchLib.Client.Models;

    /// <summary>
    /// Classe GerenciadorToken
    /// </summary>
    public static class GerenciadorToken
    {
        #region Enumeradores
        /// <summary>
        /// Token Enum
        /// </summary>
        public enum Token
        {
            /// <summary>
            /// User - Name
            /// </summary>
            [Description("%USER_NAME%")]
            USER_NAME,

            /// <summary>
            /// User - Name
            /// </summary>
            [Description("%USER_ID%")]
            USER_ID,

            /// <summary>
            /// Command - Count
            /// </summary>
            [Description("%COMMAND_COUNT%")]
            COMMAND_COUNT,

            /// <summary>
            /// Command - Created At
            /// </summary>
            [Description("%COMMAND_CREATED_AT%")]
            COMMAND_CREATED_AT,
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Formatar o conteudo
        /// </summary>
        /// <param name="conteudo">Conteudo a ser formatado</param>
        /// <param name="objetos">Relação de objetos</param>
        public static string FormatarConteudo(string conteudo, params object[] objetos)
        {
            // Validar
            if (string.IsNullOrWhiteSpace(conteudo))
            {
                return null;
            }

            // Objetos
            Comando comando = null;
            ChatMessage chatMessage = null;

            // Obter objetos
            foreach (object objeto in objetos.Where(x => x != null))
            {
                // Comando
                if (objeto is Comando)
                {
                    comando = objeto as Comando;
                }

                // ChatMessage
                if (objeto is ChatMessage)
                {
                    chatMessage = objeto as ChatMessage;
                }
            }

            // User - ID
            if (conteudo.Contains(Enumeradores.ObterDescricao(Token.USER_ID)))
            {
                conteudo = conteudo.Replace(Enumeradores.ObterDescricao(Token.USER_ID), string.Format("{0:N0}", long.Parse(chatMessage?.UserId)));
            }

            // User - Name
            if (conteudo.Contains(Enumeradores.ObterDescricao(Token.USER_NAME)))
            {
                conteudo = conteudo.Replace(Enumeradores.ObterDescricao(Token.USER_NAME), chatMessage?.Username);
            }

            // Command - Count
            if (conteudo.Contains(Enumeradores.ObterDescricao(Token.COMMAND_COUNT)))
            {
                conteudo = conteudo.Replace(Enumeradores.ObterDescricao(Token.COMMAND_COUNT), comando?.Contagem.HasValue == true ? comando?.Contagem?.ToString("N0") : "0");
            }

            // Command - Created at
            if (conteudo.Contains(Enumeradores.ObterDescricao(Token.COMMAND_CREATED_AT)))
            {
                conteudo = conteudo.Replace(Enumeradores.ObterDescricao(Token.COMMAND_CREATED_AT), string.Format("{0:dd/mm/yyyy}", comando?.Lancamento));
            }

            // Retorno
            return conteudo;
        }
        #endregion
    }
}