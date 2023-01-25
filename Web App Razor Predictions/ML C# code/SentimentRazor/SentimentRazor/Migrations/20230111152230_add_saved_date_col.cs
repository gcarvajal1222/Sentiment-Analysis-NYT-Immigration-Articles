using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SentimentRazor.Migrations
{
    public partial class add_saved_date_col : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SavedDate",
                table: "SavePrediction",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SavedDate",
                table: "SavePrediction");
        }
    }
}
