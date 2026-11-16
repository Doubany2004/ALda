using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EpicerieAlda.Models
{
    public class ProduitCommande
    {
        public int Id { get; set; }
        public int ProduitId { get; set; }
        public Produit? Produit { get; set; }

        public int CommandeId { get; set; }
        public Commande? Commande { get; set; }

        [Required]
        public int Quantite { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "La quantité doit être entre 1 et 100.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal PrixUnitaire { get; set; }

        [NotMapped]
        public decimal SousTotal => Quantite * PrixUnitaire;


    }
}