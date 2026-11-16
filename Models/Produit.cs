using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpicerieAlda.Models
{
    public class Produit
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nom { get; set; } = "";

        [StringLength(500)]
        public string? Description { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Prix { get; set; }


        [StringLength(200)]
        public string ImageUrl { get; set; } = "";

        [Required]
        public int CategorieId { get; set; }
        public Categorie? Categorie { get; set; }

        public List<ProduitCommande> ProduitCommandes { get; set; } = new();
    }
}
