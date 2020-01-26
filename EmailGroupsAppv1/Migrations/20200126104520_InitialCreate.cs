using Microsoft.EntityFrameworkCore.Migrations;

namespace EmailGroupsAppv1.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MailGroups",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailGroups", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "MailAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    MailGroupName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MailAddresses_MailGroups_MailGroupName",
                        column: x => x.MailGroupName,
                        principalTable: "MailGroups",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MailAddresses_MailGroupName",
                table: "MailAddresses",
                column: "MailGroupName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MailAddresses");

            migrationBuilder.DropTable(
                name: "MailGroups");
        }
    }
}
