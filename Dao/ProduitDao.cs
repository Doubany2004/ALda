using Microsoft.EntityFrameworkCore;
using EpicerieAlda.Models;
using EpicerieAlda.Data;
namespace EpicerieAlda.Dao
{
    public class ProduitDao : IProduitDao
    {
        private readonly AldaDbContext _db;

        // permet de donner l'accès à la base de données via le contexte AldaDbContext
        public ProduitDao(AldaDbContext db)
        {
            _db = db;
        }

        // trie les produit par nom et retourne la liste des produits de chaque  catégorie
        public Task<List<Produit>> ObtenirTousAsyncAvecCategorie() => 
_db.Produits
            .Include(p => p.Categorie)
       .OrderBy(p => p.CategorieId)
       .ThenBy(p => p.Nom)
       .ToListAsync();

    }
}


    
