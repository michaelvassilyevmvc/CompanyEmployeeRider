using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Employee
{
    [Column("EmployeeId")]
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Имя сотрудника – обязательное поле.")]
    [MaxLength(30,ErrorMessage = "Максимальная длина имени — 30 символов..")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Возраст – обязательное поле.")]
    public int Age { get; set; }
    [Required(ErrorMessage = "Должность – обязательное поле.")]
    [MaxLength(20, ErrorMessage = "Максимальная длина позиции — 20 символов.")]
    public string? Position { get; set; }
    [ForeignKey(nameof(Company))]
    public Guid CompanyId { get; set; }
    public Company? Company { get; set; }
}