using Microsoft.EntityFrameworkCore;
using EpicerieAlda.Models;
namespace EpicerieAlda.Dao
{
    public interface ICommandeDao
    {
        Task<Commande?> ObtenirParIdAsync(int id);
        Task<Commande?> ObtenirAvecProduitsAsync(int id);
        Task AjouterAsync(Commande commande);
        Task AjouterProduitAsync(int commandeId, int produitId, int quantite);
        Task SupprimerLigneAsync(int ligneId);
        Task ModifierQuantiteAsync(int ligneId, int quantite);
        Task<int> ObtenirOuCreerPanierAsync(int? commandeId);
        Task ConfirmerAsync(int commandeId, int clientId);


    }
}
