using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.Json.Serialization;
using WEBAPPP.Models;

namespace WEBAPP.Models{
    public class CGM : ConnectedD{

       
       
        public required string Marque { get; set; } 

        public required string Modele { get; set; } 

        public required string Type { get; set; }

    

     



    }

}