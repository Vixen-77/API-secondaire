
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WEBAPPP.Models{

  public abstract class ConnectedD{

      [Key]
      public Guid IdO { get; set; }  // ajouter un service pour les prefix 

      [JsonIgnore]
      public required int ADRMAC {get; set;}
      



  }

}