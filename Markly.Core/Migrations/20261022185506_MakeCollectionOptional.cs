using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Markly.Core.Migrations
{
    /// <inheritdoc />
    public partial class MakeCollectionOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmarks_Collections_CollectionId",
                table: "Bookmarks");

            migrationBuilder.AlterColumn<int>(
                name: "CollectionId",
                table: "Bookmarks",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmarks_Collections_CollectionId",
                table: "Bookmarks",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmarks_Collections_CollectionId",
                table: "Bookmarks");

            migrationBuilder.AlterColumn<int>(
                name: "CollectionId",
                table: "Bookmarks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmarks_Collections_CollectionId",
                table: "Bookmarks",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
