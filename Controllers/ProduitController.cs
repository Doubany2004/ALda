using EpicerieAlda.Dao;
using EpicerieAlda.Models;
using Microsoft.AspNetCore.Mvc;
namespace EpicerieAlda.Controllers
{
    public class ProduitController : Controller
    {
        private readonly IProduitDao _dao; // dépend de l'interface, pas de la classe
        public ProduitController(IProduitDao dao)
        {
            _dao = dao;
        }

        public async Task<IActionResult> Index()
        {
            var produits = await _dao.ObtenirTousAsyncAvecCategorie(); // exemple avec idCategorie = 1
            return View(produits);

        }
       

    }
}
