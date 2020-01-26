using Microsoft.EntityFrameworkCore.Migrations;

namespace EmailGroupsAppv1.Migrations
{
    public partial class ChangeRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MailAddresses_MailGroups_MailGroupName",
                table: "MailAddresses");

            migrationBuilder.DropIndex(
                name: "IX_MailAddresses_MailGroupName",
                table: "MailAddresses");

            migrationBuilder.DropColumn(
                name: "MailGroupName",
                table: "MailAddresses");

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "MailAddresses",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MailAddresses_MailGroups_GroupName",
                table: "MailAddresses");

            migrationBuilder.DropIndex(
                name: "IX_MailAddresses_GroupName",
                table: "MailAddresses");

            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "MailAddresses");

            migrationBuilder.AddColumn<string>(
                name: "MailGroupName",
                table: "MailAddresses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MailAddresses_MailGroupName",
                table: "MailAddresses",
                column: "MailGroupName");

            migrationBuilder.AddForeignKey(
                name: "FK_MailAddresses_MailGroups_MailGroupName",
                table: "MailAddresses",
                column: "MailGroupName",
                principalTable: "MailGroups",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
