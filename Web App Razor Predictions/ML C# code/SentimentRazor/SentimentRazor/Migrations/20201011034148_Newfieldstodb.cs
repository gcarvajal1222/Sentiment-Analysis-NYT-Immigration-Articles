using Microsoft.EntityFrameworkCore.Migrations;

namespace SentimentRazor.Migrations
{
    public partial class Newfieldstodb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prediction",
                table: "SavePrediction");

            migrationBuilder.AddColumn<string>(
                name: "ActualSentiment",
                table: "SavePrediction",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConfidenceScore",
                table: "SavePrediction",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SentimentPrediction",
                table: "SavePrediction",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualSentiment",
                table: "SavePrediction");

            migrationBuilder.DropColumn(
                name: "ConfidenceScore",
                table: "SavePrediction");

            migrationBuilder.DropColumn(
                name: "SentimentPrediction",
                table: "SavePrediction");

            migrationBuilder.AddColumn<string>(
                name: "Prediction",
                table: "SavePrediction",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
