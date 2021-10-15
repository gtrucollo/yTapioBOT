namespace yTapioBOT.BancoDados.Migrations
{
    using System;
    using FluentMigrator;

    /// <summary>
    /// Classe CreatePlataforma
    /// </summary>
    [Migration(20211014203625)]
    public class CreatePlataforma : Migration
    {
        #region Métodos
        /// <inheritdoc />
        public override void Up()
        {
            Create.Table("plataforma")
                .WithColumn("id_plataforma").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("lancamento").AsDateTimeOffset().NotNullable().WithDefaultValue(DateTimeOffset.UtcNow)
                .WithColumn("alteracao").AsDateTimeOffset().NotNullable().WithDefaultValue(DateTimeOffset.UtcNow)
                .WithColumn("tipo").AsByte().NotNullable()
                .WithColumn("status").AsByte().NotNullable()
                .WithColumn("url").AsString(50).NotNullable();
        }

        /// <inheritdoc />
        public override void Down()
        {
            Delete.Table("plataforma");
        }
        #endregion
    }
}