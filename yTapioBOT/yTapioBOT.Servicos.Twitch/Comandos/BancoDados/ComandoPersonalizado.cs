namespace yTapioBOT.Servicos.Twitch.Comandos.BancoDados
{
    using System;
    using System.Linq;
    using Base;
    using yTapioBOT.BancoDados.Database;

    /// <summary>
    /// Classe ComandoPersonalizado
    /// </summary>
    [Comando(Id.Exclamacao, "comando", "Controla os comandos personalizados (add, update, del)")]
    public class ComandoPersonalizado : ComandoBase
    {
        #region Enumerador
        /// <summary>
        /// Enumerador Ação
        /// </summary>
        public enum AcaoEnum
        {
            /// <summary>
            /// Ação Adicionar
            /// </summary>
            Adicionar = 0,

            /// <summary>
            /// Ação Atualizar
            /// </summary>
            Atualizar = 1,

            /// <summary>
            /// Ação Remover
            /// </summary>
            Remover = 2
        }
        #endregion

        #region Construtor
        /// <summary>
        /// Incia uma nova instância de <seealso cref="ComandoPersonalizado"/>
        /// </summary>
        /// <param name="canal">Objeto com as informações do canal</param>
        /// <param name="argumentos">Relação de argumentos do comando</param>
        public ComandoPersonalizado(Canal canal, params string[] argumentos)
            : base(canal, argumentos)
        {
        }
        #endregion

        #region Propriedades
        /// <summary>
        /// Obtém Acaos
        /// </summary>
        private AcaoEnum Acao
        {
            get
            {
                return this.Argumentos.FirstOrDefault() switch
                {
                    "add" => AcaoEnum.Adicionar,
                    "update" => AcaoEnum.Atualizar,
                    "del" or "delete" => AcaoEnum.Remover,
                    _ => throw new Exception($"Ação { this.Argumentos.FirstOrDefault()} não existe"),
                };
            }
        }

        /// <summary>
        /// Obtém Nome
        /// </summary>
        private string Nome
        {
            get
            {
                // Validar
                if (this.Argumentos.Length < 2)
                {
                    return null;
                }

                // Retorno
                return this.Argumentos[1];
            }
        }

        /// <summary>
        /// Obtém Conteudo
        /// </summary>
        private string Conteudo
        {
            get
            {
                // Validar
                if (this.Argumentos.Length < 3)
                {
                    return null;
                }

                // Retorno
                return this.Argumentos[2];
            }
        }
        #endregion

        #region Métodos
        /// <inheritdoc/>
        public override void Executar()
        {
            // Validar
            if (string.IsNullOrWhiteSpace(this.Nome))
            {
                this.Canal.SendChannelMessage("Argumentos inválidos, não foi informado o nome do comando.");
                return;
            }

            switch (this.Acao)
            {
                case AcaoEnum.Adicionar:
                case AcaoEnum.Atualizar:
                    // Validar
                    if (string.IsNullOrWhiteSpace(this.Conteudo))
                    {
                        this.Canal.SendChannelMessage("Argumentos inválidos, não foi informado o conteudo do comando.");
                        return;
                    }

                    // Gravar/Atualizar
                    this.Canal.Database.Make<ComandoDb, Guid?>(bo => bo.GravarAtualizarComando(this.Canal.Id, this.Nome, this.Conteudo));

                    // Mensagem no chat
                    this.Canal.SendChannelMessage($"Comando {this.Nome} criado/atualizado com sucesso!");
                    break;

                case AcaoEnum.Remover:
                    // Remover
                    this.Canal.Database.Make<ComandoDb>(bo => bo.Remover(this.Canal.Id, this.Nome));

                    // Mensagem no chat
                    this.Canal.SendChannelMessage($"Comando {this.Nome} removido com sucesso!");
                    break;
            }
        }
        #endregion
    }
}