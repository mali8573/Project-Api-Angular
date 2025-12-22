using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotteryApi.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Orders_OrderModelId",
                table: "Packages");

            migrationBuilder.DropIndex(
                name: "IX_Packages_OrderModelId",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "OrderModelId",
                table: "Packages");

            migrationBuilder.CreateTable(
                name: "OrderModelPackageModel",
                columns: table => new
                {
                    OrderModelId = table.Column<int>(type: "int", nullable: false),
                    PackagesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderModelPackageModel", x => new { x.OrderModelId, x.PackagesId });
                    table.ForeignKey(
                        name: "FK_OrderModelPackageModel_Orders_OrderModelId",
                        column: x => x.OrderModelId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderModelPackageModel_Packages_PackagesId",
                        column: x => x.PackagesId,
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderModelPackageModel_PackagesId",
                table: "OrderModelPackageModel",
                column: "PackagesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderModelPackageModel");

            migrationBuilder.AddColumn<int>(
                name: "OrderModelId",
                table: "Packages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Packages_OrderModelId",
                table: "Packages",
                column: "OrderModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Orders_OrderModelId",
                table: "Packages",
                column: "OrderModelId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
