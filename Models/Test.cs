using System.ComponentModel.DataAnnotations;
namespace WEBAPPP.Models
{
    public class Test
    {
        public int Id { get; set; }

        [Required]  // Rendre Name obligatoire
        public string Name { get; set; } = string.Empty;
    }
}
