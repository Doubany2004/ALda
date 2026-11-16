using Microsoft.EntityFrameworkCore;
using EpicerieAlda.Models;
using EpicerieAlda.Data;
namespace EpicerieAlda.Dao
{
    public class CommandeDao : ICommandeDao
    {
        private readonly AldaDbContext _db;

        // permet de donner l'accès à la base de données via le contexte AldaDbContext
        public CommandeDao(AldaDbContext db)
        {
            _db = db;
        }

        // affiche la liste d'une commande avec les produits associés avec les colonnes de la table Commande
        public Task<Commande?> ObtenirParIdAsync(int id) =>
            _db.Commandes.FirstOrDefaultAsync(c => c.Id == id);

        // cherche une commande avec ses lignes et les produits de chaque ligne avec les colonnes
        // de la table CommandeProduit et de la table Produit
        // utilise pour afficher les produits disponibles dans la commande sur le panier
        public Task<Commande?> ObtenirAvecProduitsAsync(int id) =>
            _db.Commandes
             .Include(c => c.Client)
               .Include(c => c.CommandeProduit)
                   .ThenInclude(l => l.Produit)
               .FirstOrDefaultAsync(c => c.Id == id);

        // ajoute une commande à la base de données
        public async Task AjouterAsync(Commande commande)
        {
            await _db.Commandes.AddAsync(commande);
            await _db.SaveChangesAsync();
        }

        // creer un panier ou va etre les commandes du client
        // si le panier existe deja on retourne l'id du panier sinon on cree un nouveau panier
        public async Task<int> ObtenirOuCreerPanierAsync(int? commandeId)
        {
            // le panier existe déjà et c'est encore un panier ?
            if (commandeId != null)
            {
                var existante = await _db.Commandes.FindAsync(commandeId.Value);
                if (existante != null && existante.Statut == "Panier")
                    return existante.Id;
            }

            // sinon : nouvelle commande vide (sans client)
            var commande = new Commande
            {
                Statut = "Panier",
                DateCommande = DateTime.Now
            };
            _db.Commandes.Add(commande);
            await _db.SaveChangesAsync();          // la base remplit commande.Id

            return commande.Id;
        }


        // ajoute un produit dans une commande (ou augmente la quantité s'il y est déjà)
        public async Task AjouterProduitAsync(int commandeId, int produitId, int quantite)
        {
            // chercher le produit pour avoir son vrai prix
            var produit = await _db.Produits.FindAsync(produitId);
            if (produit == null) return;

            //  regarder si ce produit est déjà dans la commande
            var ligne = await _db.ProduitCommandes
                .FirstOrDefaultAsync(l => l.CommandeId == commandeId && l.ProduitId == produitId);

            if (ligne != null)
            {
                ligne.Quantite += quantite;            
            }
            else
            {
                _db.ProduitCommandes.Add(new ProduitCommande
                {
                    CommandeId = commandeId,
                    ProduitId = produitId,
                    Quantite = quantite,
                    PrixUnitaire = produit.Prix        
                });
            }
            await _db.SaveChangesAsync();

            //  recalculer le total de la commande
            var commande = await _db.Commandes.FindAsync(commandeId);
            if (commande != null)
            {
                commande.Total = await _db.ProduitCommandes
                    .Where(l => l.CommandeId == commandeId)
                    .SumAsync(l => l.Quantite * l.PrixUnitaire);
                await _db.SaveChangesAsync();
            }
        }


        // change la quantité d'une ligne du panier (0 = retirer le produit)
        public async Task ModifierQuantiteAsync(int ligneId, int quantite)
        {
            var ligne = await _db.ProduitCommandes.FindAsync(ligneId);
            if (ligne == null) return;

            var commandeId = ligne.CommandeId;

            if (quantite <= 0)
                _db.ProduitCommandes.Remove(ligne);
            else
                ligne.Quantite = quantite;

            await _db.SaveChangesAsync();

            var commande = await _db.Commandes.FindAsync(commandeId);
            if (commande != null)
            {
                commande.Total = await _db.ProduitCommandes
                    .Where(l => l.CommandeId == commandeId)
                    .SumAsync(l => l.Quantite * l.PrixUnitaire);
                await _db.SaveChangesAsync();
            }
        }

        // retire un seul produit du panier et recalcule le total de la commande
        public async Task SupprimerLigneAsync(int ligneId)
        {
            var ligne = await _db.ProduitCommandes.FindAsync(ligneId);
            if (ligne == null) return;

            var commandeId = ligne.CommandeId;
            _db.ProduitCommandes.Remove(ligne);
            await _db.SaveChangesAsync();

            var commande = await _db.Commandes.FindAsync(commandeId);
            if (commande != null)
            {
                commande.Total = await _db.ProduitCommandes
                    .Where(l => l.CommandeId == commandeId)
                    .SumAsync(l => l.Quantite * l.PrixUnitaire);
                await _db.SaveChangesAsync();
            }
        }

        // confirme la commade du client 
        public async Task ConfirmerAsync(int commandeId, int clientId)
        {
            var commande = await _db.Commandes.FindAsync(commandeId);
            if (commande == null) return;
            commande.ClientId = clientId;
            commande.Statut = "En Attente";
            commande.DateCommande = DateTime.Now;
            await _db.SaveChangesAsync();
        }

    }
}
