using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Company
{
    [Column("CompanyId")]
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Адрес компании – обязательное поле.")]
    [MaxLength(60, ErrorMessage = "Адрес компании для адреса состоит из 60 символов.")]
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Country { get; set; }
    public ICollection<Employee>? Employees { get; set; }
}