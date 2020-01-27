using Microsoft.EntityFrameworkCore.Migrations;

namespace EmailGroupsAppv1.Migrations
{
    public partial class AddingIdToMailGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MailAddresses_MailGroups_GroupName",
                table: "MailAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MailGroups",
                table: "MailGroups");

            migrationBuilder.DropIndex(
                name: "IX_MailAddresses_GroupName",
                table: "MailAddresses");

            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "MailAddresses");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MailGroups",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "MailAddresses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MailGroups",
                table: "MailGroups",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MailGroups_Name",
                table: "MailGroups",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MailAddresses_GroupId",
                table: "MailAddresses",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_MailAddresses_MailGroups_GroupId",
                table: "MailAddresses",
                column: "GroupId",
                principalTable: "MailGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MailAddresses_MailGroups_GroupId",
                table: "MailAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MailGroups",
                table: "MailGroups");

            migrationBuilder.DropIndex(
                name: "IX_MailGroups_Name",
                table: "MailGroups");

            migrationBuilder.DropIndex(
                name: "IX_MailAddresses_GroupId",
                table: "MailAddresses");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MailGroups");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "MailAddresses");

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "MailAddresses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MailGroups",
                table: "MailGroups",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_MailAddresses_GroupName",
                table: "MailAddresses",
                column: "GroupName");

            migrationBuilder.AddForeignKey(
                name: "FK_MailAddresses_MailGroups_GroupName",
                table: "MailAddresses",
                column: "GroupName",
                principalTable: "MailGroups",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
