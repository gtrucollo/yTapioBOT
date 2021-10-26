namespace yTapioBOT.BancoDados.Database
{
    using System;
    using Base;
    using Dapper.Database.Extensions;
    using Entidade.Database;
    using Npgsql;

    /// <summary>
    /// Classe ComandoAutoRespostaDb
    /// </summary>
    public class ComandoAutoRespostaDb : BaseDb<ComandoAutoResposta>
    {
        #region Construtor
        /// <summary>
        /// Inicia uma nova instância de <seealso cref="ComandoAutoRespostaDb"/>
        /// </summary>
        /// <param name="sessaoControle">Sessão com o banco de dados</param>
        public ComandoAutoRespostaDb(NpgsqlConnection sessaoControle)
            : base(sessaoControle)
        {
        }
        #endregion

        #region Métodos
        #region Públicos
        /// <summary>
        /// Selecionar comando por nome
        /// </summary>
        /// <param name="idPlataforma">Identificador da plataforma</param>
        /// <param name="nome">Nome do comando</param>
        /// <returns>O comando localizado</returns>
        public ComandoAutoResposta SelecionarComando(Guid idPlataforma, string nome)
        {
            // Retorno
            return this.SessaoControle.Get<ComandoAutoResposta>("WHERE (id_plataforma = @IdPlataforma) AND (strpos(@Nome, nome) > 0) ORDER BY id LIMIT 1", new { IdPlataforma = idPlataforma, Nome = string.Format("'{0}'", nome?.Trim()) });
        }

        /// <summary>
        /// Gravar/Atualizar comando
        /// </summary>
        /// <param name="idPlataforma">Identificador da plataforma</param>
        /// <param name="nome">Nome do comando</param>
        /// <param name="conteudo">Conteudo do comando</param>
        /// <returns>O identificador do comando</returns>
        public Guid? GravarAtualizarComando(Guid idPlataforma, string nome, string conteudo)
        {
            // Selecionar comando
            ComandoAutoResposta comando = this.SelecionarComando(idPlataforma, nome);
            if (comando == null)
            {
                comando = new ComandoAutoResposta() { IdPlataforma = idPlataforma, Nome = nome };
            }

            comando.Conteudo = conteudo;

            // Salvar
            switch (comando.Id == Guid.Empty)
            {
                case true:
                    // Novo Id
                    comando.Id = Guid.NewGuid();

                    // Salvar
                    this.Insert(comando);
                    break;

                default:
                    // Salvar
                    this.Update(comando);
                    break;
            }

            // Id
            return comando.Id;
        }

        /// <summary>
        /// Remover comando
        /// </summary>
        /// <param name="idPlataforma">Identificador da plataforma</param>
        /// <param name="nome">Nome do comando</param>
        public void Remover(Guid idPlataforma, string nome)
        {
            this.Delete(this.SelecionarComando(idPlataforma, nome));
        }
        #endregion

        #region Diversos
        /// <inheritdoc />
        public override string ObterTabelaNome()
        {
            return "comando_auto_resposta";
        }
        #endregion
        #endregion
    }
}