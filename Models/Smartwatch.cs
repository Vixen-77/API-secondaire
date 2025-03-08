using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.Json.Serialization;
using WEBAPPP.Models;

namespace WEBAPP.Models{
    public class Smartwatch : ConnectedD{

        public string Marque { get; set; } = string.Empty;

        public Guid IDuser { get; private set; }

        public string Modele { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string Couleur { get; set; } = string.Empty;
        public float FC { get; set; }
        public float TA_sys { get; set; }
        public float  TA_dia { get; set; }
        public float O2 { get; set; }
        public float VFC { get; set; }
        public bool IsConnected { get; set; }
        public bool Anomalie { get; set; }

    }
 
public class SmartwatchPrediction
{
    [Anomalie("PredictedLabel")]
        public bool Anomalie;
        public string PredictedLabel { get; set; } = string.Empty; // Ajoutez cette ligne
    }
}