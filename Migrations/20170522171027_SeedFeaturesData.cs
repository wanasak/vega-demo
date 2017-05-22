using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VegaDemo.Migrations
{
    public partial class SeedFeaturesData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Features VALUES ('Feature1')");
            migrationBuilder.Sql("INSERT INTO Features VALUES ('Feature2')");
            migrationBuilder.Sql("INSERT INTO Features VALUES ('Feature3')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Features");
        }
    }
}
