using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotteryApi.Migrations
{
    /// <inheritdoc />
    public partial class update6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tz",
                table: "Donors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tz",
                table: "Donors",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
