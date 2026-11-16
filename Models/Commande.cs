using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpicerieAlda.Models;

public class Commande
{
    public int Id { get; set; }

    // debut le client n'a pas encore mis les information donc on peut avoir une commande sans client    
    public int? ClientId { get; set; }         
    public Client? Client { get; set; }          

    [Display(Name = "Date de la commande")]
    public DateTime DateCommande { get; set; } = DateTime.Now;

    [Required]
    [StringLength(20)]
    [Display(Name = "Statut")]
    public string Statut { get; set; } = "En attente";

    [NotMapped]
    [Display(Name = "Nombre d'articles")]
    public int NombreArticles => CommandeProduit.Sum(l => l.Quantite);

    [Column(TypeName = "decimal(10,2)")]
    [Display(Name = "Total")]
    public decimal Total { get; set; }

    


    public List<ProduitCommande> CommandeProduit { get; set; } = new();
}