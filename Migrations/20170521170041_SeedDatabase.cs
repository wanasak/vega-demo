using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VegaDemo.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Make (Name) VALUES ('Make1')");
            migrationBuilder.Sql("INSERT INTO Make (Name) VALUES ('Make2')");
            migrationBuilder.Sql("INSERT INTO Make (Name) VALUES ('Make3')");

            migrationBuilder.Sql("INSERT INTO Model (Name, MakeId) VALUES ('Make1-ModelA', (SELECT Id FROM Make WHERE Name = 'Make1'))");
            migrationBuilder.Sql("INSERT INTO Model (Name, MakeId) VALUES ('Make1-ModelB', (SELECT Id FROM Make WHERE Name = 'Make1'))");
            migrationBuilder.Sql("INSERT INTO Model (Name, MakeId) VALUES ('Make1-ModelC', (SELECT Id FROM Make WHERE Name = 'Make1'))");

            migrationBuilder.Sql("INSERT INTO Model (Name, MakeId) VALUES ('Make2-ModelA', (SELECT Id FROM Make WHERE Name = 'Make2'))");
            migrationBuilder.Sql("INSERT INTO Model (Name, MakeId) VALUES ('Make2-ModelB', (SELECT Id FROM Make WHERE Name = 'Make2'))");
            migrationBuilder.Sql("INSERT INTO Model (Name, MakeId) VALUES ('Make2-ModelC', (SELECT Id FROM Make WHERE Name = 'Make2'))");

            migrationBuilder.Sql("INSERT INTO Model (Name, MakeId) VALUES ('Make3-ModelA', (SELECT Id FROM Make WHERE Name = 'Make3'))");
            migrationBuilder.Sql("INSERT INTO Model (Name, MakeId) VALUES ('Make3-ModelB', (SELECT Id FROM Make WHERE Name = 'Make3'))");
            migrationBuilder.Sql("INSERT INTO Model (Name, MakeId) VALUES ('Make3-ModelC', (SELECT Id FROM Make WHERE Name = 'Make3'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Make");
        }
    }
}
