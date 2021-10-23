namespace yTapioBOT.BancoDados.Database
{
    using System;
    using Base;
    using Dapper.Database.Extensions;
    using Entidade.Database;
    using Npgsql;

    /// <summary>
    /// Classe ComandoDb
    /// </summary>
    public class ComandoDb : BaseDb<Comando>
    {
        #region Construtor
        /// <summary>
        /// Inicia uma nova instância de <seealso cref="ComandoDb"/>
        /// </summary>
        /// <param name="sessaoControle">Sessão com o banco de dados</param>
        public ComandoDb(NpgsqlConnection sessaoControle)
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
        public Comando SelecionarComando(Guid idPlataforma, string nome)
        {
            // Retorno
            return this.SessaoControle.Get<Comando>("WHERE (id_plataforma = @IdPlataforma) AND (nome = @Nome)", new { IdPlataforma = idPlataforma, Nome = nome });
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
            Comando comando = this.SelecionarComando(idPlataforma, nome);
            if (comando == null)
            {
                comando = new Comando() { IdPlataforma = idPlataforma, Nome = nome };
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
            return "comando";
        }
        #endregion
        #endregion
    }
}