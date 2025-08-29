using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "INSERT INTO Products (Name, Description, Price, Stock, ImageUrl, CategoryId) " +
                "VALUES ('Caderno', 'Caderno Vermelho', 10.00, 10, 'caderno1.jpg', 1)");
            
            migrationBuilder.Sql(
                "INSERT INTO Products (Name, Description, Price, Stock, ImageUrl, CategoryId) " +
                "VALUES ('Caneta', 'Caneta Preta', 2.00, 20, 'caneta1.jpg', 1)");
            
            migrationBuilder.Sql(
                "INSERT INTO Products (Name, Description, Price, Stock, ImageUrl, CategoryId) " +
                "VALUES ('Chaveiro', 'Chaveiro Clássico', 12.00, 15, 'chaveiro1.jpg', 2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "DELETE FROM Products WHERE Name IN ('Caderno', 'Caneta', 'Chaveiro')");
        }
    }
}
