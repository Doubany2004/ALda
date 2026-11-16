
using Microsoft.EntityFrameworkCore;
using EpicerieAlda.Models;
namespace EpicerieAlda.Dao
{
    public interface IProduitDao
    {
        Task<List<Produit>> ObtenirTousAsyncAvecCategorie();

    }
}
