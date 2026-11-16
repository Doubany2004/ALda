using EpicerieAlda.Dao;
using EpicerieAlda.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace EpicerieAlda.Controllers
{
    public class CommandeController : Controller
    {
        private readonly ICommandeDao _dao; // dépend de l'interface, pas de la classe
        private readonly IClientDao _clientDao;
        public CommandeController(ICommandeDao dao,IClientDao clientDao)
        {
            _dao = dao;
            _clientDao = clientDao;
        }

        public async Task<IActionResult> Index()
        {
            // va chercher id de la commande dans la session
            int? sessionId = HttpContext.Session.GetInt32("CommandeId");

            // si le numero de session n'est pas null
            if (sessionId != null)
            {
                // cherche la commande avec les produits associés
                var commandeId = await _dao.ObtenirAvecProduitsAsync(sessionId.Value);
                if (commandeId != null)
                {
                    // commande trouvée, on l'affiche dans la vue
                    return View(commandeId);
                }


            }
            // si le numero de session est null, on crée une nouvelle commande qui est 
            return View(new Commande());
        }

            // ajouter un produit a la commande
            [HttpPost]
            public async Task<IActionResult> AjouterProduitAuPanier(int produitId)
            {
                // lire le numéro du panier dans la session (peut être null)
                int? ancienId = HttpContext.Session.GetInt32("CommandeId");

                // le DAO retrouve le panier ou en crée un nouveau
                int commandeId = await _dao.ObtenirOuCreerPanierAsync(ancienId);

                //  garder le numéro du panier dans la session
                HttpContext.Session.SetInt32("CommandeId", commandeId);

                // ajouter 1 exemplaire du produit dans le panier
                await _dao.AjouterProduitAsync(commandeId, produitId, 1);

            // met a jour le nombre d'articles dans la session pour l'afficher sur l'acceuil 

            await MettreAJourNombreArticlesAsync(commandeId);

            return RedirectToAction("Index", "Produit");   // on reste sur la page Produits
            }
        // modifier la quantité d'un produit dans le panier
        [HttpPost]
        public async Task<IActionResult> ModifierQuantite(int ligneId, int quantite)
        {
            if (quantite <= 0)
            {
                // si la quantité est 0 ou négative, on supprime la ligne
                await _dao.SupprimerLigneAsync(ligneId);
            }
            else
            {
                // sinon on modifie la quantité
                await _dao.ModifierQuantiteAsync(ligneId, quantite);
            }
            // mettre à jour le nombre d'articles affiché dans le menu
            int? commandeId = HttpContext.Session.GetInt32("CommandeId");
            if (commandeId != null)
            {
                await MettreAJourNombreArticlesAsync(commandeId.Value);
            }
            return RedirectToAction("Index");
        }

        
        // supprimer une ligne du panier
        [HttpPost]
        public async Task<IActionResult> SupprimerLigne(int ligneId)
        {
            // 1. supprimer d'abord
            await _dao.SupprimerLigneAsync(ligneId);

            // 2. puis mettre à jour le nombre d'articles affiché dans le menu
            int? commandeId = HttpContext.Session.GetInt32("CommandeId");
            if (commandeId != null)
            {
                await MettreAJourNombreArticlesAsync(commandeId.Value);
            }

            return RedirectToAction("Index");
        }

        // afficher le formulaire client (GET) pour aller à la page de confirmation de la commande
        public async Task<IActionResult> Confirmer()
        {
            int? commandeId = HttpContext.Session.GetInt32("CommandeId");
            if (commandeId == null)
                return RedirectToAction("Index");

            // on ne confirme pas un panier vide
            var commande = await _dao.ObtenirAvecProduitsAsync(commandeId.Value);
            if (commande == null || commande.CommandeProduit.Count == 0)
                return RedirectToAction("Index");

            ViewData["Total"] = commande.Total;
            return View(new Client());
        }

        // confirmation de la commande post pour enregistrer le client et confirmer la commande
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirmer(Client client)
        {
            int? commandeId = HttpContext.Session.GetInt32("CommandeId");
            if (commandeId == null)
                return RedirectToAction("Index");

            // le formulaire contient des erreurs : on le réaffiche avec les messages
            if (!ModelState.IsValid)
            {
                var commande = await _dao.ObtenirAvecProduitsAsync(commandeId.Value);
                ViewData["Total"] = commande?.Total ?? 0m;
                return View(client);
            }



            //  enregistrer le client (après ça, client.Id est rempli par la base)
            await _clientDao.AjouterClientAsync(client);

            //  relier la commande à ce client et la confirmer
            await _dao.ConfirmerAsync(commandeId.Value, client.Id);

            //  le panier est terminé : on vide la session
            HttpContext.Session.Remove("CommandeId");
            HttpContext.Session.Remove("NombreArticles");

            //  on retient la commande confirmée pour la page Merci
            HttpContext.Session.SetInt32("DerniereCommandeId", commandeId.Value);

            return RedirectToAction("Confirmation");
        }

        // page de remerciement : affiche la commande que le client vient de confirmer
        public async Task<IActionResult> Confirmation()
        {
            int? commandeId = HttpContext.Session.GetInt32("DerniereCommandeId");
            if (commandeId == null)
                return RedirectToAction("Index", "Acceuil");

            var commande = await _dao.ObtenirAvecProduitsAsync(commandeId.Value);
            if (commande == null)
                return RedirectToAction("Index", "Acceuil");

            return View(commande);
        }

        // met à jour le nombre d'articles gardé dans la session
        // mettre sur acceuil a cote du logo le nombre d'articles dans le panier
        private async Task MettreAJourNombreArticlesAsync(int commandeId)
        {
            var commande = await _dao.ObtenirAvecProduitsAsync(commandeId);
            int nombre = commande?.NombreArticles ?? 0;
            HttpContext.Session.SetInt32("NombreArticles", nombre);
        }

    }
}

