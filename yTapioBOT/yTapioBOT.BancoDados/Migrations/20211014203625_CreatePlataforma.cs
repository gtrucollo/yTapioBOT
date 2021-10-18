namespace yTapioBOT.BancoDados.Migrations
{
    using System;
    using FluentMigrator;
    using yTapioBOT.Entidade.Database;
    using yTapioBOT.Enumeradores;

    /// <summary>
    /// Classe CreatePlataforma
    /// </summary>
    [Migration(20211014203625)]
    public class CreatePlataforma : Migration
    {
        #region Métodos
        #region Públicos
        /// <inheritdoc />
        public override void Up()
        {
            Create.Table("plataforma")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("lancamento").AsDateTimeOffset().NotNullable().WithDefaultValue(DateTimeOffset.UtcNow)
                .WithColumn("alteracao").AsDateTimeOffset().NotNullable().WithDefaultValue(DateTimeOffset.UtcNow)
                .WithColumn("tipo").AsByte().NotNullable()
                .WithColumn("status").AsByte().NotNullable()
                .WithColumn("url").AsString(50).NotNullable();

            // Cadastrar plataforma padrão
            this.CadastrarNovaPlataforma("yTapioca", Plataforma.TipoEnum.Twitch);
            this.CadastrarNovaPlataforma("Jaabriel", Plataforma.TipoEnum.Twitch);
        }

        /// <inheritdoc />
        public override void Down()
        {
            Delete.Table("plataforma");
        }
        #endregion

        #region Privados
        /// <summary>
        /// Insere um novo registro na tabela de plataformass
        /// </summary>
        /// <param name="url">Url da plataforma</param>
        /// <param name="tipo">Tipo da plataforma</param>
        private void CadastrarNovaPlataforma(string url, Plataforma.TipoEnum tipo)
        {
            Insert.IntoTable("plataforma").Row(new
            {
                id = Guid.NewGuid(),
                tipo = (byte)tipo,
                url = url,
                status = (byte)AtivoInativo.Ativo
            });
        }
        #endregion
        #endregion
    }
}