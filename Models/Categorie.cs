using System.ComponentModel.DataAnnotations;

namespace EpicerieAlda.Models
{
    public class Categorie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nom { get; set; } = "";

        
        public List<Produit> Produits { get; set; } = new();
    }
}