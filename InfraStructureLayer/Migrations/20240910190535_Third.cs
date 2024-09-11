using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructureLayer.Migrations
{
    public partial class Third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the existing column
            migrationBuilder.DropPrimaryKey(
                name: "PK_Registrations",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Registrations");

            // Add new column with IDENTITY property
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Registrations",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            // Re-apply primary key on the new Id column
            migrationBuilder.AddPrimaryKey(
                name: "PK_Registrations",
                table: "Registrations",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove the primary key constraint on the Id column
            migrationBuilder.DropPrimaryKey(
                name: "PK_Registrations",
                table: "Registrations");

            // Drop the Id column (with the IDENTITY property)
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Registrations");

            // Re-add the old Id column as string
            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Registrations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            // Re-apply the primary key constraint on the old Id column
            migrationBuilder.AddPrimaryKey(
                name: "PK_Registrations",
                table: "Registrations",
                column: "Id");
        }
    }
}
