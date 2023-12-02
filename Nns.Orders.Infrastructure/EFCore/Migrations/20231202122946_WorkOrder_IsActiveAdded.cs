using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nns.Orders.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class WorkOrderIsActiveAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "WorkOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "WorkOrders");
        }
    }
}
