using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestNg.Migrations
{
    public partial class Addingcontactstble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "contacts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<string>(nullable: true),
                    firstName = table.Column<string>(nullable: true),
                    lastName = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    DateofBirth = table.Column<DateTime>(type: "date", nullable: false),
                    Phonenumber = table.Column<string>(nullable: true),
                    numbertype = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Adress = table.Column<string>(nullable: true),
                    Other = table.Column<string>(type: "text", nullable: true),
                    isfavorite = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contacts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_contacts_AspNetUsers_userID",
                        column: x => x.userID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_contacts_userID",
                table: "contacts",
                column: "userID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contacts");
        }
    }
}
