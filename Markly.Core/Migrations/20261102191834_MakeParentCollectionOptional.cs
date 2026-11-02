using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Markly.Core.Migrations
{
    /// <inheritdoc />
    public partial class MakeParentCollectionOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_Collections_ParentId",
                table: "Collections");

            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                table: "Collections",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_Collections_ParentId",
                table: "Collections",
                column: "ParentId",
                principalTable: "Collections",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_Collections_ParentId",
                table: "Collections");

            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                table: "Collections",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_Collections_ParentId",
                table: "Collections",
                column: "ParentId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
