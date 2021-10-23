namespace yTapioBOT.BancoDados.Database
{
    using System;
    using System.Data;
    using Base;
    using Dapper;
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
            // Parâmetros
            DynamicParameters parametros = new();
            parametros.Add(nameof(Comando.IdPlataforma), idPlataforma, DbType.Guid);
            parametros.Add(nameof(Comando.Nome), nome, DbType.String);

            // Retorno
            return this.SessaoControle.QueryFirstOrDefault<Comando>("SELECT * FROM comando WHERE (id_plataforma = @IdPlataforma) AND (nome = @Nome)", parametros);
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