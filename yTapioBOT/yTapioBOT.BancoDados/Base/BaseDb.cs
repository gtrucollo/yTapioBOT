namespace yTapioBOT.BancoDados.Base
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Reflection;
    using Dapper.Database.Extensions;
    using Npgsql;

    /// <summary>
    /// Classe BaseDatabase
    /// </summary>
    public abstract class BaseDb<TType> where TType : class
    {
        #region Campos
        /// <summary>
        /// Controle para a conexão com o banco de dados
        /// </summary>
        private NpgsqlConnection sessaoControle;

        /// <summary>
        /// Transação de controle
        /// </summary>
        private IDbTransaction transacaoControle;
        #endregion

        #region Construtor
        /// <summary>
        /// Inicia uma nova instância de <seealso cref="BaseDatabase"/>
        /// </summary>
        /// <param name="sessaoControle">Sessão com o banco de dados</param>
        public BaseDb(NpgsqlConnection sessaoControle)
        {
            this.sessaoControle = sessaoControle;
        }
        #endregion

        #region Propriedades
        /// <summary>
        /// Obtém SessaoControle
        /// </summary>
        protected NpgsqlConnection SessaoControle
        {
            get
            {
                if (sessaoControle == null)
                {
                    sessaoControle = Sessao.SessaoControle;
                }

                // Verificar status da conexão
                if (sessaoControle.State != ConnectionState.Open)
                {
                    sessaoControle.Open();
                }

                return sessaoControle;
            }
        }
        #endregion

        #region Métodos
        #region Diversos
        /// <summary>
        /// Obter o valor total de registros
        /// </summary>
        /// <returns>O Valor total de registros</returns>
        public long ObterRelacaoTotal()
        {
            return this.SessaoControle.Count<TType>();
        }

        /// <summary>
        /// Selecionar todos os registros
        /// </summary>
        /// <returns>A lista de registros encontrada</returns>
        public IList<TType> SelecionarTodos()
        {
            return this.SessaoControle.GetList<TType>().ToList();
        }
        #endregion

        #region Transações
        /// <summary>
        /// Inicia uma transação com o banco de dados.
        /// </summary>
        protected void IniciarTransacao()
        {
            // Abrir nova transação
            this.transacaoControle = this.SessaoControle.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// Grava as informações da transação no banco.
        /// </summary>
        /// <returns>True se executado com sucesso</returns>
        protected bool GravarTransacao()
        {
            // Gravar a transação
            try
            {
                this.transacaoControle.Commit();
                this.transacaoControle.Dispose();
                this.transacaoControle = null;

                // OK
                return true;
            }
            catch
            {
                try
                {
                    // Cancelar transação
                    this.CancelarTransacao();
                }
                catch (Exception exp)
                {
                    Console.WriteLine(string.Format("[BaseDb] Erro ao cancelar a transação (GravarTransacao): {0}", exp));
                }

                throw;
            }
        }

        /// <summary>
        /// Cancela uma transação.
        /// </summary>
        protected void CancelarTransacao()
        {
            try
            {
                this.transacaoControle.Rollback();
            }
            finally
            {
                this.transacaoControle = null;
            }
        }
        #endregion

        #region Insert e Update
        /// <summary>
        /// Insere ou altera um objeto no banco de dados
        /// </summary>      
        /// <param name="objeto">Parâmetro Objeto</param>
        /// <returns>O objeto após a persistência</returns>
        public virtual TType Insert(TType objeto)
        {
            try
            {
                // Begin Transação
                this.IniciarTransacao();

                // Atualizar a data de alteração
                PropertyInfo propriedade = objeto.GetType().GetProperties().Where(x => x.Name.ToLower() == "lancamento").FirstOrDefault();
                if (propriedade != null)
                {
                    propriedade.SetValue(objeto, DateTimeOffset.UtcNow);
                }

                // Atualizar a data de alteração
                propriedade = objeto.GetType().GetProperties().Where(x => x.Name.ToLower() == "alteracao").FirstOrDefault();
                if (propriedade != null)
                {
                    propriedade.SetValue(objeto, DateTimeOffset.UtcNow);
                }

                // Atualizar
                this.SessaoControle.Insert(objeto);

                // Commit Transação
                this.GravarTransacao();

                // OK
                return objeto;
            }
            catch
            {
                // Rollback Transação
                this.CancelarTransacao();
                throw;
            }
        }

        /// <summary>
        /// Insere ou altera vários objetos no banco de dados
        /// </summary>
        /// <param name="user">O usuário do controle de acesso</param>
        /// <param name="listaObjetos">Lista com os Objetos</param>
        /// <param name="logInformacao">Informação adicional para o Log</param>
        public void Insert(List<TType> listaObjetos)
        {
            // Validar
            if (listaObjetos.Count == 0)
            {
                return;
            }

            try
            {
                // Begin Transação
                this.IniciarTransacao();

                // Salvar todos os objetos
                listaObjetos.ForEach(x => this.Insert(x));

                // Commit Transação
                this.GravarTransacao();
            }
            catch
            {
                // Rollback Transação
                this.CancelarTransacao();
                throw;
            }
        }

        /// <summary>
        /// Insere ou altera um objeto no banco de dados
        /// </summary>      
        /// <param name="objeto">Parâmetro Objeto</param>
        /// <returns>O objeto após a persistência</returns>
        public virtual TType Update(TType objeto)
        {
            try
            {
                // Begin Transação
                this.IniciarTransacao();

                // Atualizar a data de alteração
                PropertyInfo propriedade = objeto.GetType().GetProperties().Where(x => x.Name.ToLower() == "alteracao").FirstOrDefault();
                if (propriedade != null)
                {
                    propriedade.SetValue(objeto, DateTimeOffset.UtcNow);
                }

                // Atualizar
                this.SessaoControle.Update(objeto);

                // Commit Transação
                this.GravarTransacao();

                // OK
                return objeto;
            }
            catch
            {
                // Rollback Transação
                this.CancelarTransacao();
                throw;
            }
        }

        /// <summary>
        /// Insere ou altera vários objetos no banco de dados
        /// </summary>
        /// <param name="user">O usuário do controle de acesso</param>
        /// <param name="listaObjetos">Lista com os Objetos</param>
        /// <param name="logInformacao">Informação adicional para o Log</param>
        public void Update(List<TType> listaObjetos)
        {
            // Validar
            if (listaObjetos.Count == 0)
            {
                return;
            }

            try
            {
                // Begin Transação
                this.IniciarTransacao();

                // Salvar todos os objetos
                listaObjetos.ForEach(x => this.Update(x));

                // Commit Transação
                this.GravarTransacao();
            }
            catch
            {
                // Rollback Transação
                this.CancelarTransacao();
                throw;
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// Exclui um objeto do banco de dados.
        /// </summary>
        /// <param name="objeto">Parâmetro objeto</param>
        public virtual void Delete(TType objeto)
        {
            if (objeto is null)
            {
                return;
            }

            try
            {
                // Begin Transação
                this.IniciarTransacao();

                // Excluir
                this.SessaoControle.Delete(objeto);

                // Commit Transação
                this.GravarTransacao();
            }
            catch
            {
                // Rollback Transação
                this.CancelarTransacao();
                throw;
            }
        }

        /// <summary>
        /// Exclui um objeto do banco de dados.
        /// </summary>
        /// <param name="listaObjetos">Parâmetro objeto</param>
        public void Delete(List<TType> listaObjetos)
        {
            // Validar
            if (listaObjetos.Count == 0)
            {
                return;
            }

            try
            {
                // Begin Transação
                this.IniciarTransacao();

                // Salvar todos os objetos
                listaObjetos.ForEach(x => this.Delete(x));

                // Commit Transação
                this.GravarTransacao();
            }
            catch
            {
                // Rollback Transação
                this.CancelarTransacao();
                throw;
            }
        }
        #endregion
        #endregion
    }
}