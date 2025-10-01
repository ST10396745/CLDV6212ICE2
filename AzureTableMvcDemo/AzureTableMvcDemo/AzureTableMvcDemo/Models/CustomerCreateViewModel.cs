using System.ComponentModel.DataAnnotations;

namespace AzureTableMvcDemo.Models;

public class CustomerCreateViewModel
{
    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(200)]
    public string Email { get; set; } = string.Empty;
}
