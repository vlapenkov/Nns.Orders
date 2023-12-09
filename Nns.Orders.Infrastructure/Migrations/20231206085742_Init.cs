using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Nns.Orders.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EquipmentTypes",
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
                    table.PrimaryKey("PK_EquipmentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Excavations",
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
                    table.PrimaryKey("PK_Excavations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkTypes",
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
                    table.PrimaryKey("PK_WorkTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentApplications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EquipmentTypeId = table.Column<long>(type: "bigint", nullable: false),
                    WorkTypeId = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentApplications_EquipmentTypes_EquipmentTypeId",
                        column: x => x.EquipmentTypeId,
                        principalTable: "EquipmentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentApplications_WorkTypes_WorkTypeId",
                        column: x => x.WorkTypeId,
                        principalTable: "WorkTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkCycles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkTypeId = table.Column<long>(type: "bigint", nullable: false),
                    OrderNumber = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkCycles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkCycles_WorkTypes_WorkTypeId",
                        column: x => x.WorkTypeId,
                        principalTable: "WorkTypes",
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
                    ExcavationId = table.Column<long>(type: "bigint", nullable: false),
                    WorkTypeId = table.Column<long>(type: "bigint", nullable: false),
                    EquipmentTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Value = table.Column<long>(type: "bigint", nullable: false),
                    OrderNumber = table.Column<long>(type: "bigint", nullable: false),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrders_EquipmentTypes_EquipmentTypeId",
                        column: x => x.EquipmentTypeId,
                        principalTable: "EquipmentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkOrders_Excavations_ExcavationId",
                        column: x => x.ExcavationId,
                        principalTable: "Excavations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkOrders_WorkTypes_WorkTypeId",
                        column: x => x.WorkTypeId,
                        principalTable: "WorkTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EquipmentTypes",
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
                table: "Excavations",
                columns: new[] { "Id", "EndDate", "Name" },
                values: new object[,]
                {
                    { 1L, null, "Первая выработка" },
                    { 2L, null, "Вторая выработка" },
                    { 3L, null, "Третья выработка" }
                });

            migrationBuilder.InsertData(
                table: "WorkTypes",
                columns: new[] { "Id", "EndDate", "Name" },
                values: new object[,]
                {
                    { 1L, null, "Бурение" },
                    { 2L, null, "Крепление" },
                    { 3L, null, "Отгрузка" },
                    { 4L, null, "Взрыв" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentApplications_EquipmentTypeId",
                table: "EquipmentApplications",
                column: "EquipmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentApplications_StartDate_WorkTypeId_EquipmentTypeId",
                table: "EquipmentApplications",
                columns: new[] { "StartDate", "WorkTypeId", "EquipmentTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentApplications_WorkTypeId",
                table: "EquipmentApplications",
                column: "WorkTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCycles_StartDate_WorkTypeId",
                table: "WorkCycles",
                columns: new[] { "StartDate", "WorkTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkCycles_WorkTypeId",
                table: "WorkCycles",
                column: "WorkTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_EquipmentTypeId",
                table: "WorkOrders",
                column: "EquipmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_ExcavationId",
                table: "WorkOrders",
                column: "ExcavationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_StartDate_ExcavationId_WorkTypeId_EquipmentTypeId",
                table: "WorkOrders",
                columns: new[] { "StartDate", "ExcavationId", "WorkTypeId", "EquipmentTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_WorkTypeId",
                table: "WorkOrders",
                column: "WorkTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentApplications");

            migrationBuilder.DropTable(
                name: "WorkCycles");

            migrationBuilder.DropTable(
                name: "WorkOrders");

            migrationBuilder.DropTable(
                name: "EquipmentTypes");

            migrationBuilder.DropTable(
                name: "Excavations");

            migrationBuilder.DropTable(
                name: "WorkTypes");
        }
    }
}
