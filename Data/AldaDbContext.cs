using Microsoft.EntityFrameworkCore;
using EpicerieAlda.Models;

namespace EpicerieAlda.Data
{
    public class AldaDbContext:DbContext
    {

        public AldaDbContext(DbContextOptions<AldaDbContext> options) : base(options) { }

        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Commande> Commandes => Set<Commande>();
        public DbSet<Produit> Produits => Set<Produit>();
        public DbSet<Categorie> Categories => Set<Categorie>();
        public DbSet<ProduitCommande> ProduitCommandes => Set<ProduitCommande>();
        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Categorie>().HasData(
       new Categorie { Id = 1, Nom = "Fruits et légumes" },
       new Categorie { Id = 2, Nom = "Viandes et poissons" },
       new Categorie { Id = 3, Nom = "Produits laitiers" },
       new Categorie { Id = 4, Nom = "Épicerie" }
   );

            mb.Entity<Produit>().HasData(
    // Fruits et légumes (CategorieId = 1)
    new Produit { Id = 1, Nom = "Avocat", Description = "Avocat mûr, à l'unité.", Prix = 1.99m, ImageUrl = "/Image/avocat.jpg", CategorieId = 1 },
    new Produit { Id = 2, Nom = "Oignons", Description = "Oignons jaunes, sac de 1 kg.", Prix = 2.49m, ImageUrl = "/Image/oignion.jpg", CategorieId = 1 },
    new Produit { Id = 3, Nom = "Laitue", Description = "Laitue romaine fraîche.", Prix = 2.29m, ImageUrl = "/Image/salade.jpg", CategorieId = 1 },
    new Produit { Id = 4, Nom = "Tomates", Description = "Tomates de serre, 1 kg.", Prix = 3.99m, ImageUrl = "/Image/tomate.jpg", CategorieId = 1 },

    // Viandes et poissons (CategorieId = 2)
    new Produit { Id = 5, Nom = "Crevettes", Description = "Crevettes crues, 400 g.", Prix = 12.99m, ImageUrl = "/Image/crevette.jpg", CategorieId = 2 },
    new Produit { Id = 6, Nom = "Poisson", Description = "Filets de poisson frais, 500 g.", Prix = 10.99m, ImageUrl = "/Image/poisson.png", CategorieId = 2 },
    new Produit { Id = 7, Nom = "Poulet", Description = "Poitrines de poulet, 1 kg.", Prix = 14.99m, ImageUrl = "/Image/poulet.jpg", CategorieId = 2 },
    new Produit { Id = 8, Nom = "Viande", Description = "Bœuf decoupé , 1000 g.", Prix = 18.99m, ImageUrl = "/Image/viande.jpg", CategorieId = 2 },

    // Produits laitiers (CategorieId = 3)
    new Produit { Id = 9, Nom = "Lait 2 %", Description = "Lait partiellement écrémé, 2 L.", Prix = 4.79m, ImageUrl = "/Image/lait.jpg", CategorieId = 3 },
    new Produit { Id = 10, Nom = "Fromage cheddar", Description = "Cheddar doux, 400 g.", Prix = 7.49m, ImageUrl = "/Image/fromage.jpg", CategorieId = 3 },
    new Produit { Id = 11, Nom = "Fromage gouda", Description = "Gouda en tranches, 200 g.", Prix = 5.99m, ImageUrl = "/Image/fromage2.jpg", CategorieId = 3 },
    new Produit { Id = 12, Nom = "Vache Riz", Description = "Un fromage de 10 morceaux.", Prix = 4.79m, ImageUrl = "/Image/vache_riz.jpg", CategorieId = 3 },

    // Épicerie (CategorieId = 4)
    new Produit { Id = 13, Nom = "Huile d'olive", Description = "Huile d'olive extra vierge, 1 L.", Prix = 11.99m, ImageUrl = "/Image/huile.jpg", CategorieId = 4 },
    new Produit { Id = 14, Nom = "Pasta", Description = "Pâtes alimentaires, 680 g.", Prix = 2.99m, ImageUrl = "/Image/pasta.jpg", CategorieId = 4 },
    new Produit { Id = 15, Nom = "Spaghetti", Description = "Spaghetti, 900 g.", Prix = 2.79m, ImageUrl = "/Image/spageti.jpg", CategorieId = 4 },
    new Produit { Id = 16, Nom = "Pâte de tomate", Description = "Pâte de tomate, boîte de 156 ml.", Prix = 1.49m, ImageUrl = "/Image/pate_tomate.jpg", CategorieId = 4 }
  
);
        }

    }
}
