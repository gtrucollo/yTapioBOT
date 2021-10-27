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
        public Comando SelecionarComando(Guid idPlataforma, string nome, bool atualizarQuantidade)
        {
            // Selecionar
            Comando retorno = this.SessaoControle.Get<Comando>("WHERE (id_plataforma = @IdPlataforma) AND (nome = @Nome)", new { IdPlataforma = idPlataforma, Nome = nome });
            if (retorno == null)
            {
                return retorno;
            }

            // Atualizar contagem
            if (retorno.Conteudo.Contains("%COMMAND_COUNT%") && atualizarQuantidade)
            {
                retorno.Contagem = (retorno.Contagem ?? 0) + 1;

                // Salvar
                this.Update(retorno);

                // Selecionar novamente
                retorno = this.SelecionarComando(idPlataforma, nome, false);
            }

            // Retorno
            return retorno;
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
            Comando comando = this.SelecionarComando(idPlataforma, nome, false);
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
            this.Delete(this.SelecionarComando(idPlataforma, nome, false));
        }

        /// <summary>
        /// Atualizar a contagem
        /// </summary>
        /// <param name="idPlataforma">Identificador da plataforma</param>
        /// <param name="nome">Nome do comando</param>
        /// <param name="contagem">Contagem a ser atualizada</param>
        public void AtualizarContagem(Guid idPlataforma, string nome, int contagem)
        {
            // Selecionar
            Comando retorno = this.SelecionarComando(idPlataforma, nome, false);
            if (retorno == null)
            {
                return;
            }

            // Atualizar
            retorno.Contagem = contagem;

            // Salvar
            this.Update(retorno);
        }
        #endregion
        #endregion
    }
}