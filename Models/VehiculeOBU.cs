using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.Json.Serialization;
using WEBAPPP.Models;

namespace WEBAPP.Models{
    public class VehiculeOBU : ConnectedD{

        [Key]
        public Guid IdV { get; set; }  // Convention EF pour la clé primaire

        [JsonIgnore]
        public string Immatriculation { get; set; } = string.Empty;

        public string Marque { get; set; } = string.Empty;

        public string Modele { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string Couleur { get; set; } = string.Empty;

     



    }

}