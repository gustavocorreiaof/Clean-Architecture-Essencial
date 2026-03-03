using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Name", "Description", "Price", "Stock", "Image", "CategoryId" },
                values: new object[,]
                {
                    { new Guid("1a2b3c4d-5e6f-4a8b-9c0d-1e2f3a4b5c6d"), "Caderno", "Caderno universitário com 200 folhas", 19.99m, 100, "caderno.jpg", new Guid("a66e285e-0cb5-45d2-998c-d2cb092fafc3") },
                    { new Guid("2b3c4d5e-6f7a-4b8c-9d0e-1f2a3b4c5d6e"), "Mochila", "Mochila resistente para transporte de livros e laptop", 149.99m, 50, "mochila.jpg", new Guid("f331d521-319d-4511-ba3e-73febdc635ec") },
                    { new Guid("3c4d5e6f-7a8b-4c9d-0e1f-2a3b4c5d6e7f"), "Fone de Ouvido", "Fone de ouvido com cancelamento de ruído e microfone embutido", 89.99m, 75, "fone.jpg", new Guid("47e1eb5b-1a6b-4b0f-a759-6a159680ffb2") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM \"Product\" WHERE \"Id\" IN ('1a2b3c4d-5e6f-4a8b-9c0d-1e2f3a4b5c6d', '2b3c4d5e-6f7a-4b8c-9d0e-1f2a3b4c5d6e', '3c4d5e6f-7a8b-4c9d-0e1f-2a3b4c5d6e7f')");
        }
    }
}