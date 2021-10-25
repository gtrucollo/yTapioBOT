namespace yTapioBOT.BancoDados.Migrations
{
    using System;
    using FluentMigrator;

    /// <summary>
    /// Classe CreateComandoAutoResposta
    /// </summary>
    [Migration(20211025201438)]
    public class CreateComandoAutoResposta : Migration
    {
        #region Métodos
        #region Públicos
        /// <inheritdoc />
        public override void Up()
        {
            Create.Table("comando_auto_resposta")
              .WithColumn("id").AsGuid().NotNullable().PrimaryKey().Unique()
              .WithColumn("id_plataforma").AsGuid().ForeignKey("plataforma", "id").NotNullable()
              .WithColumn("lancamento").AsDateTimeOffset().NotNullable().WithDefaultValue(DateTimeOffset.UtcNow)
              .WithColumn("alteracao").AsDateTimeOffset().NotNullable().WithDefaultValue(DateTimeOffset.UtcNow)
              .WithColumn("nome").AsString(100).NotNullable()
              .WithColumn("conteudo").AsString(500).NotNullable();
        }

        /// <inheritdoc />
        public override void Down()
        {
            Delete.Table("comando_auto_resposta");
        }
        #endregion
        #endregion
    }
}
