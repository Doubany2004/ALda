using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EpicerieAlda.Migrations
{
    /// <inheritdoc />
    public partial class Initiale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroCivique = table.Column<int>(type: "int", nullable: false),
                    NomRue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumeroAppartement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ville = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Province = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Pays = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CodePostal = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Prix = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CategorieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produits_Categories_CategorieId",
                        column: x => x.CategorieId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Commandes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    DateCommande = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Statut = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commandes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commandes_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProduitCommandes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProduitId = table.Column<int>(type: "int", nullable: false),
                    CommandeId = table.Column<int>(type: "int", nullable: false),
                    Quantite = table.Column<int>(type: "int", nullable: false),
                    PrixUnitaire = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduitCommandes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProduitCommandes_Commandes_CommandeId",
                        column: x => x.CommandeId,
                        principalTable: "Commandes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProduitCommandes_Produits_ProduitId",
                        column: x => x.ProduitId,
                        principalTable: "Produits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Nom" },
                values: new object[,]
                {
                    { 1, "Fruits et légumes" },
                    { 2, "Viandes et poissons" },
                    { 3, "Produits laitiers" },
                    { 4, "Épicerie" }
                });

            migrationBuilder.InsertData(
                table: "Produits",
                columns: new[] { "Id", "CategorieId", "Description", "ImageUrl", "Nom", "Prix" },
                values: new object[,]
                {
                    { 1, 1, "Avocat mûr, à l'unité.", "/Image/avocat.jpg", "Avocat", 1.99m },
                    { 2, 1, "Oignons jaunes, sac de 1 kg.", "/Image/oignion.jpg", "Oignons", 2.49m },
                    { 3, 1, "Laitue romaine fraîche.", "/Image/salade.jpg", "Laitue", 2.29m },
                    { 4, 1, "Tomates de serre, 1 kg.", "/Image/tomate.jpg", "Tomates", 3.99m },
                    { 5, 2, "Crevettes crues, 400 g.", "/Image/crevette.jpg", "Crevettes", 12.99m },
                    { 6, 2, "Filets de poisson frais, 500 g.", "/Image/poisson.png", "Poisson", 10.99m },
                    { 7, 2, "Poitrines de poulet, 1 kg.", "/Image/poulet.jpg", "Poulet", 14.99m },
                    { 8, 2, "Bœuf decoupé , 1000 g.", "/Image/viande.jpg", "Viande", 18.99m },
                    { 9, 3, "Lait partiellement écrémé, 2 L.", "/Image/lait.jpg", "Lait 2 %", 4.79m },
                    { 10, 3, "Cheddar doux, 400 g.", "/Image/fromage.jpg", "Fromage cheddar", 7.49m },
                    { 11, 3, "Gouda en tranches, 200 g.", "/Image/fromage2.jpg", "Fromage gouda", 5.99m },
                    { 12, 3, "Un fromage de 10 morceaux.", "/Image/vache_riz.jpg", "Vache Riz", 4.79m },
                    { 13, 4, "Huile d'olive extra vierge, 1 L.", "/Image/huile.jpg", "Huile d'olive", 11.99m },
                    { 14, 4, "Pâtes alimentaires, 680 g.", "/Image/pasta.jpg", "Pasta", 2.99m },
                    { 15, 4, "Spaghetti, 900 g.", "/Image/spageti.jpg", "Spaghetti", 2.79m },
                    { 16, 4, "Pâte de tomate, boîte de 156 ml.", "/Image/pate_tomate.jpg", "Pâte de tomate", 1.49m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commandes_ClientId",
                table: "Commandes",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ProduitCommandes_CommandeId",
                table: "ProduitCommandes",
                column: "CommandeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProduitCommandes_ProduitId",
                table: "ProduitCommandes",
                column: "ProduitId");

            migrationBuilder.CreateIndex(
                name: "IX_Produits_CategorieId",
                table: "Produits",
                column: "CategorieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProduitCommandes");

            migrationBuilder.DropTable(
                name: "Commandes");

            migrationBuilder.DropTable(
                name: "Produits");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
