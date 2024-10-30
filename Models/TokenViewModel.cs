// ViewModels/TokenViewModel.cs
using System.ComponentModel.DataAnnotations;

public class TokenViewModel
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Amount must be greater than 0")]
    public int Amount { get; set; }

    [Required]
    [RegularExpression(@"^[a-zA-Z0-9]{32,44}$", ErrorMessage = "Invalid recipient address")]
    public string RecipientAddress { get; set; }
}