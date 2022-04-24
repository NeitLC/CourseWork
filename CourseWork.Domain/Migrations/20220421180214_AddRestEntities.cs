using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.Domain.Migrations
{
    public partial class AddRestEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_AspNetUsers_UserId",
                table: "Collections");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_AspNetUsers_UserId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_UserId",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Items",
                newName: "ThirdOptionalTextField");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Comments",
                newName: "UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                newName: "IX_Comments_UserId1");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Collections",
                newName: "UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Collections_UserId",
                table: "Collections",
                newName: "IX_Collections_UserId1");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Tags",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CollectionId",
                table: "Items",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "FirstOptionalBoolField",
                table: "Items",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FirstOptionalDateTimeField",
                table: "Items",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FirstOptionalNumberField",
                table: "Items",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstOptionalStringField",
                table: "Items",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstOptionalTextField",
                table: "Items",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Items",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Items",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "SecondOptionalBoolField",
                table: "Items",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SecondOptionalDateTimeField",
                table: "Items",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SecondOptionalNumberField",
                table: "Items",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondOptionalStringField",
                table: "Items",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondOptionalTextField",
                table: "Items",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ThirdOptionalBoolField",
                table: "Items",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ThirdOptionalDateTimeField",
                table: "Items",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ThirdOptionalNumberField",
                table: "Items",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThirdOptionalStringField",
                table: "Items",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfCreation",
                table: "Comments",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "Comments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Comments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Collections",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstFieldName",
                table: "Collections",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FirstFieldType",
                table: "Collections",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Collections",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecondFieldName",
                table: "Collections",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SecondFieldType",
                table: "Collections",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ThirdFieldName",
                table: "Collections",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ThirdFieldType",
                table: "Collections",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ItemTag",
                columns: table => new
                {
                    ItemsId = table.Column<int>(type: "integer", nullable: false),
                    TagsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTag", x => new { x.ItemsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_ItemTag_Items_ItemsId",
                        column: x => x.ItemsId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemUser",
                columns: table => new
                {
                    ItemsId = table.Column<int>(type: "integer", nullable: false),
                    UsersLikedId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemUser", x => new { x.ItemsId, x.UsersLikedId });
                    table.ForeignKey(
                        name: "FK_ItemUser_AspNetUsers_UsersLikedId",
                        column: x => x.UsersLikedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemUser_Items_ItemsId",
                        column: x => x.ItemsId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_CollectionId",
                table: "Items",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ItemId",
                table: "Comments",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTag_TagsId",
                table: "ItemTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemUser_UsersLikedId",
                table: "ItemUser",
                column: "UsersLikedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_AspNetUsers_UserId1",
                table: "Collections",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserId1",
                table: "Comments",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Items_ItemId",
                table: "Comments",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Collections_CollectionId",
                table: "Items",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_AspNetUsers_UserId1",
                table: "Collections");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserId1",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Items_ItemId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Collections_CollectionId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "ItemTag");

            migrationBuilder.DropTable(
                name: "ItemUser");

            migrationBuilder.DropIndex(
                name: "IX_Items_CollectionId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ItemId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "FirstOptionalBoolField",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "FirstOptionalDateTimeField",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "FirstOptionalNumberField",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "FirstOptionalStringField",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "FirstOptionalTextField",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "SecondOptionalBoolField",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "SecondOptionalDateTimeField",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "SecondOptionalNumberField",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "SecondOptionalStringField",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "SecondOptionalTextField",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ThirdOptionalBoolField",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ThirdOptionalDateTimeField",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ThirdOptionalNumberField",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ThirdOptionalStringField",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "DateOfCreation",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "FirstFieldName",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "FirstFieldType",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "SecondFieldName",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "SecondFieldType",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "ThirdFieldName",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "ThirdFieldType",
                table: "Collections");

            migrationBuilder.RenameColumn(
                name: "ThirdOptionalTextField",
                table: "Items",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Comments",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserId1",
                table: "Comments",
                newName: "IX_Comments_UserId");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Collections",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Collections_UserId1",
                table: "Collections",
                newName: "IX_Collections_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_UserId",
                table: "Items",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_AspNetUsers_UserId",
                table: "Collections",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_AspNetUsers_UserId",
                table: "Items",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
