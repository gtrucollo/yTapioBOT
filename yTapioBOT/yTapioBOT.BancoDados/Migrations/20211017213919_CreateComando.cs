﻿namespace yTapioBOT.BancoDados.Migrations
{
    using System;
    using FluentMigrator;

    [Migration(20211017213919)]
    public class CreateComando : Migration
    {
        #region Métodos
        #region Públicos
        /// <inheritdoc />
        public override void Up()
        {
            Create.Table("comando")
              .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
              .WithColumn("lancamento").AsDateTimeOffset().NotNullable().WithDefaultValue(DateTimeOffset.UtcNow)
              .WithColumn("alteracao").AsDateTimeOffset().NotNullable().WithDefaultValue(DateTimeOffset.UtcNow)
              .WithColumn("conteudo").AsString(500).NotNullable()
              .WithColumn("contagem").AsInt64().Nullable()
              .WithColumn("administrador").AsBoolean().NotNullable().WithDefaultValue(false);
        }

        /// <inheritdoc />
        public override void Down()
        {
            Delete.Table("comando");
        }
        #endregion
        #endregion
    }
}