namespace PlayerSponsor.Server.Models;

public class Product
{
    public int Id { get; set; } // Unique identifier for the product
    public string Name { get; set; } = string.Empty; // Name of the product
    public int PriceUnit { get; set; } // Price as an integer in cents of localized property.
    public string Description { get; set; } = string.Empty; // Brief description of the product
    public string IconWord { get; set; } = string.Empty; // Keyword representing the icon for the product
    public string[]? Tags { get; set; } // Optional array of tags for the product
}
