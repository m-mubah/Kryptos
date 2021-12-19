using System.ComponentModel.DataAnnotations;

namespace Kryptos.Web.Client.Models.Luhn;

public class GenerateCard
{
    [Required(ErrorMessage = "A card manufacturer must be selected.")]
    public CardManufacturer CardManufacturer { get; set; }
}