using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace EpicerieAlda.Models;

public class Client
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Le Nom  est obligatoire.")]
    [StringLength(50, ErrorMessage = "Le Nom ne peut pas dépasser 50 caractères.")]
    [Display(Name = "Nom")]
    public string Nom { get; set; } = "";

    [Required(ErrorMessage = "Le Prénom  est obligatoire.")]
    [StringLength(50, ErrorMessage = "Le prénom ne peut pas dépasser 50 caractères.")]
    [Display(Name = "prénom")]
    public string Prenom { get; set; } = "";

    [Required(ErrorMessage = "Le téléphone est obligatoire.")]
    [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "Format attendu : 613-555-0101.")]
    [Display(Name = "Téléphone")]
    public string Telephone { get; set; } = "";

    [Required(ErrorMessage = "L'email est obligatoire.")]
    [RegularExpression(@"^[a-zA-Z0-9._%+\-]+@[a-zA-Z0-9.\-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Format attendu : alda@gmail.com")]
    [Display(Name = "Email")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Le numéro civique  est obligatoire.")]
    [Range(1, 99999, ErrorMessage = "Le numéro civique doit être positif.")]
    [Display(Name = "Numéro Civique")]
    public int NumeroCivique { get; set; }

    [Required(ErrorMessage = "Le nom de la rue  est obligatoire.")]
    [StringLength(50, ErrorMessage = "Le nom de la rue ne peut pas dépasser 50 caractères.")]
    [Display(Name = "Nom de la Rue")]
    public string NomRue { get; set; } = "";


    [Display(Name = "Numéro Appartement")]
    public string? NumeroAppartement { get; set; }


    [Required(ErrorMessage = "La ville  est obligatoire.")]
    [StringLength(50, ErrorMessage = "La ville ne peut pas dépasser 50 caractères.")]
    [Display(Name = "Ville")]
    public string Ville { get; set; } = "";

    [Required(ErrorMessage = "La province est obligatoire.")]
    [StringLength(50, ErrorMessage = "La province ne peut pas dépasser 50 caractères.")]
    [Display(Name = "province")]
    public string Province { get; set; } = "";

    [Required(ErrorMessage = "Le pays  est obligatoire.")]
    [StringLength(50, ErrorMessage = "Le pays ne peut pas dépasser 50 caractères.")]
    [Display(Name = "Pays")]
    public string Pays { get; set; } = "";

    [Required(ErrorMessage = "Le code postal est obligatoire.")]
    [RegularExpression(@"^[A-Za-z]\d[A-Za-z] ?\d[A-Za-z]\d$", ErrorMessage = "Format attendu : K1A 0B1.")]
    [Display(Name = "Code Postal")]
    public string CodePostal { get; set; } = "";

    public List<Commande> CommandeClient { get; set; } = new();



}


