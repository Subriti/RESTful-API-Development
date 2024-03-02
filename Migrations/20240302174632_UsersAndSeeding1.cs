using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RESTful_API__ASP.NET_Core.Migrations
{
    /// <inheritdoc />
    public partial class UsersAndSeeding1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PointOfInterests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointOfInterests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PointOfInterests_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "The one with that big park.", "New York City" },
                    { 2, "The one with the cathedral that was never ready.", "Antwerp" },
                    { 3, "The one with that big tower.", "Paris" }
                });

            migrationBuilder.InsertData(
                table: "PointOfInterests",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, 1, "The most visited urban park in the United States", "Central Park" },
                    { 2, 1, "A 102-storey skyscraper located in the Midtown Manhattan", "Empire State Building" },
                    { 3, 2, "A gothic style cathedral, conceived by architects", "Cathedral of our lady" },
                    { 4, 2, "The first finest example of railway architecture in Belgium.", "Antwerp Central Station" },
                    { 5, 3, "A wrought iron lattice tower on the Champ de Mars.", "Eiffel Tower" },
                    { 6, 3, "The world's largest museum.", "The Louvre" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CityId", "Email", "Name", "Password" },
                values: new object[,]
                {
                    { -4, 2, "apoo@gmail.com", "Apoorva Basnet", "Apoorva123" },
                    { -3, 3, "prakhyat123@gmail.com", "Prakhyat Shrestha", "Prakhyat123" },
                    { -2, 1, "ashishjaiswal@gmail.com", "Ashish Jaiswal", "Ashish123" },
                    { -1, 1, "aryalsubriti@gmail.com", "Subriti Aryal", "Subriti123" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PointOfInterests_CityId",
                table: "PointOfInterests",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CityId",
                table: "Users",
                column: "CityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PointOfInterests");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
