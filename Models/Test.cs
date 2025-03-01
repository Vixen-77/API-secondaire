using System.ComponentModel.DataAnnotations;

namespace WEBAPPP.Models
{
    public class Test
    {
        [Key]
        public int Id { get; set; }  // Convention EF pour la clé primaire

        [Required]
        public string Nickname { get; set; } = string.Empty;
    }
}
