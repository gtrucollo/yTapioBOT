namespace yTapioBOT.Servicos.Twitch.Comandos.Live
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Base;
    using yTapioBOT.BancoDados.Database;
    using yTapioBOT.Entidade.Database;

    /// <summary>
    /// Classe Status
    /// </summary>
    [Comando(Id.Exclamacao, "comandos", "Mostra todos os comandos disponiveis")]
    public sealed class Comandos : ComandoBase
    {
        #region Construtor
        /// <summary>
        /// Incia uma nova instância de <seealso cref="Adicionar"/>
        /// </summary>
        /// <param name="canal">Objeto com as informações do canal</param>
        /// <param name="argumentos">Relação de argumentos do comando</param>
        public Comandos(Canal canal, params string[] argumentos)
            : base(canal, argumentos)
        {
        }
        #endregion

        #region Propriedades
        /// <inheritdoc/>
        public override bool Moderador => false;
        #endregion

        #region Métodos
        #region Públicos
        /// <inheritdoc/>
        public override void Executar()
        {
            IList<ComandoAttribute> listaComandos = Assembly.GetAssembly(this.GetType())
                .GetTypes()
                .Where(x => x.GetCustomAttributes<ComandoAttribute>(true).Any())
                .Select(x => x.GetCustomAttributes<ComandoAttribute>(true).FirstOrDefault())
                .ToList();
            if (listaComandos.Count <= 0)
            {
                this.Canal.SendChannelMessage("Nenhuma implementação de comandos foi localizada.");
                return;
            }

            // Atualizar
            string mensagem = string.Empty;
            foreach (ComandoAttribute comando in listaComandos)
            {
                mensagem += string.Format("{0}{1} - ({2})", comando.IdDescricao, comando.Nome, comando.Descricao);
            }

            foreach (Comando comando in this.Canal.Database.Make<ComandoDb, IList<Comando>>(bo => bo.SelecionarTodos()))
            {
                mensagem += string.Format("!{0}", comando.Nome);
            }

            // Mensagem
            this.Canal.SendChannelMessage(mensagem);
        }
        #endregion
        #endregion
    }
}