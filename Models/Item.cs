using System.ComponentModel.DataAnnotations;

namespace RazorPagesPizza.Models;

public class Item
{
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }
    public string Category { get; set; }
    public string IconURL { get; set; }
    public ItemType Size { get; set; }

    [Range(0.01, 9999.99)]
    public decimal Price { get; set; }
}

public enum ItemType { Small, Medium, Large }
