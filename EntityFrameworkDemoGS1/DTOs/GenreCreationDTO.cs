using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkDemoGS1.DTOs;

public class GenreCreationDTO
{
    [StringLength(maximumLength: 150)]
    public string Name { get; set; } = null!;
}
