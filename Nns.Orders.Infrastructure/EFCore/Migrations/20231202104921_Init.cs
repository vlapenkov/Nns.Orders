using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Nns.Orders.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MachineKinds",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineKinds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settlements",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settlements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkKindS",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkKindS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MachineApplications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SettlementId = table.Column<long>(type: "bigint", nullable: false),
                    MachineKindId = table.Column<long>(type: "bigint", nullable: false),
                    WorkKindId = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MachineApplications_MachineKinds_MachineKindId",
                        column: x => x.MachineKindId,
                        principalTable: "MachineKinds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MachineApplications_Settlements_SettlementId",
                        column: x => x.SettlementId,
                        principalTable: "Settlements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MachineApplications_WorkKindS_WorkKindId",
                        column: x => x.WorkKindId,
                        principalTable: "WorkKindS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderPlan",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SettlementId = table.Column<long>(type: "bigint", nullable: false),
                    WorkKindId = table.Column<long>(type: "bigint", nullable: false),
                    MachineKindId = table.Column<long>(type: "bigint", nullable: false),
                    Value = table.Column<long>(type: "bigint", nullable: false),
                    OrderNumber = table.Column<long>(type: "bigint", nullable: false),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderPlan_MachineKinds_MachineKindId",
                        column: x => x.MachineKindId,
                        principalTable: "MachineKinds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderPlan_Settlements_SettlementId",
                        column: x => x.SettlementId,
                        principalTable: "Settlements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderPlan_WorkKindS_WorkKindId",
                        column: x => x.WorkKindId,
                        principalTable: "WorkKindS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SettlementId = table.Column<long>(type: "bigint", nullable: false),
                    WorkKindId = table.Column<long>(type: "bigint", nullable: false),
                    OrderNumber = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrders_Settlements_SettlementId",
                        column: x => x.SettlementId,
                        principalTable: "Settlements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkOrders_WorkKindS_WorkKindId",
                        column: x => x.WorkKindId,
                        principalTable: "WorkKindS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MachineKinds",
                columns: new[] { "Id", "EndDate", "Name" },
                values: new object[,]
                {
                    { 1L, null, "Буровая машина" },
                    { 2L, null, "Машина крепления " },
                    { 3L, null, "Погрузочно-разгрузочная машина" },
                    { 4L, null, "Взрывная техника" },
                    { 5L, null, "Универсальная машина для любых работ" }
                });

            migrationBuilder.InsertData(
                table: "Settlements",
                columns: new[] { "Id", "EndDate", "Name" },
                values: new object[,]
                {
                    { 1L, null, "Первая выработка" },
                    { 2L, null, "Вторая выработка" },
                    { 3L, null, "Третья выработка" }
                });

            migrationBuilder.InsertData(
                table: "WorkKindS",
                columns: new[] { "Id", "EndDate", "Name" },
                values: new object[,]
                {
                    { 1L, null, "Бурение" },
                    { 2L, null, "Крепление" },
                    { 3L, null, "Отгрузка" },
                    { 4L, null, "Взрыв" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MachineApplications_MachineKindId",
                table: "MachineApplications",
                column: "MachineKindId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineApplications_SettlementId",
                table: "MachineApplications",
                column: "SettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineApplications_StartDate_SettlementId_WorkKindId_MachineKindId",
                table: "MachineApplications",
                columns: new[] { "StartDate", "SettlementId", "WorkKindId", "MachineKindId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MachineApplications_WorkKindId",
                table: "MachineApplications",
                column: "WorkKindId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPlan_MachineKindId",
                table: "OrderPlan",
                column: "MachineKindId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPlan_SettlementId",
                table: "OrderPlan",
                column: "SettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPlan_WorkKindId",
                table: "OrderPlan",
                column: "WorkKindId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_SettlementId",
                table: "WorkOrders",
                column: "SettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_StartDate_SettlementId_WorkKindId",
                table: "WorkOrders",
                columns: new[] { "StartDate", "SettlementId", "WorkKindId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_WorkKindId",
                table: "WorkOrders",
                column: "WorkKindId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MachineApplications");

            migrationBuilder.DropTable(
                name: "OrderPlan");

            migrationBuilder.DropTable(
                name: "WorkOrders");

            migrationBuilder.DropTable(
                name: "MachineKinds");

            migrationBuilder.DropTable(
                name: "Settlements");

            migrationBuilder.DropTable(
                name: "WorkKindS");
        }
    }
}
