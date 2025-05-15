/*using System;
using System.Threading.Tasks;
using Azure.Core;
using LibrarySSMS;
using WEBAPPP.DTO;
using LibrarySSMS.Models;
using LibrarySSMS.Enums;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;



//FIXME: ce code sera retravailler pour quil puisse intéger linsetion de la montre qui a detcter lanomalie 
namespace WEBAPPP.Services{
public class AnomalyDetection
{

    
    
        private static readonly Random _random = new Random();
       
        public AnomalyDetection(AppDbContext context, ILogger<AnomalyDetection> logger)
        { }


        //FIXME:Méthode de détection d'anomalie avec l'algorithme de l'année passée
        //TODO: cette methode sera appeler par le HUB si le patient a coonecter un smartwatch dencienne generation (année passée)

        public async Task<bool> AnomalydetectionVariante1(Guid patientId, int age, UserState userState)
        {
             int maxIterations = 200; // Nombre max de vérifications (ex: 30 secondes)
    
               for (int i = 0; i < maxIterations; i++)
                {
                  var (FC, PAS, PAD, TGS) = GenerateRandomValues(age);

                  if (FC > 220 - age || PAS is < 90 or > 130 || PAD is < 60 or > 90 || TGS is < 0.5 or > 3)
                  {
                     // Anomalie détectée
                     //apple du service OBU ou Mont ou telephone 
                     //en cour de developpement
                    
                     return true;//sortie de la boucle 
                  }

                 await Task.Delay(1000); // Attente 1 seconde
                }

                 return false; // Aucune anomalie après 200 itérations
        } 






       
        //FIXME: Méthode de détection d'anomalie avec l'algorithme de cette année avec IA intégére
        //TODO: cette methode sera appeler par le HUB si le patient a coonecter un smartwatchnewGen

        /* 6   Age                       20002 non-null  float64
 7   Gender                    20002 non-null  int64  
 8   Weight                    20002 non-null  float64
 9   Height                    20002 non-null  float64*/

        
/*
   public bool AnomalydetectionVariante2(Guid patientId, int Age, bool Gender, double Weight, double Height, UserState userState)
{
    double P = 0.0;
    double alpha = 0.3;

    // Normalisation manuelle (exemples — à adapter avec tes vraies valeurs)

    // Charger le modèle ONNX
    using var session = new InferenceSession("Models/ton_model.onnx");

    while (P < 0.7)
    {
        for (int i = 0; i < 5; i++)
        {
            // Préparer les entrées du modèle
            var inputData = new float[] { (float)normAge, (float)normGender, (float)normWeight, (float)normHeight };
            var inputTensor = new DenseTensor<float>(inputData, new[] { 1, 4 });
            var inputNamed = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("input", inputTensor)
            };

            // Lancer la prédiction
            using IDisposableReadOnlyCollection<DisposableNamedOnnxValue> results = session.Run(inputNamed);
            var predictionTensor = results.First().AsEnumerable<float>().ToArray();
            var prediction = predictionTensor[0]; // on suppose sortie sigmoid binaire entre 0 et 1

            // Lissage exponentiel
            P = alpha * prediction + (1 - alpha) * P;
        }

        if (P >= 0.4 && P < 0.7)
        {
            // TODO: Appel au service de notification
            Console.WriteLine("🟡 Avertissement : risque modéré détecté (P = " + P.ToString("0.00") + ")");
            return false;
        }
    }

    // Cas critique
    Console.WriteLine("🔴 Alerte : anomalie détectée avec P = " + P.ToString("0.00"));

    // TODO: Appel au service d'urgence OBU / téléphone / montre
    return true;
}*/
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       

    
       
       
       
       

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////TODO:  ALGORITHME DE SIMULATION DE CAPTEUR DE CHAQUNE DES MONTRE AVEC RONDOMTODO:///////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 

       
       /*
       
       
        private static (int FC_capte, int PAS_capte, int PAD_capte, double TGS_capte) GenerateRandomValues(int age)
        {
    

              // Fréquence cardiaque basée sur l'âge
              int fcCapte = age switch
            {
              <= 20 => _random.Next(100, 171),
              <= 30 => _random.Next(95, 163),
              <= 40 => _random.Next(90, 153),
              <= 50 => _random.Next(85, 146),
              <= 60 => _random.Next(80, 137),
              _ => _random.Next(75, 129)
            };

            // Tension artérielle systolique et diastolique
            int pasCapte, padCapte;
            if (_random.Next(0, 2) == 0) // Cas hypertendu
            {
                pasCapte = _random.Next(131, 180);
                padCapte = _random.Next(91, 120);
            }
             else // Cas hypotendu
            {
                pasCapte = _random.Next(50, 89);
                padCapte = _random.Next(40, 59);
            }

            // Taux de glycémie
            double tgsCapte = _random.Next(0, 2) == 0
             ? Math.Round(_random.NextDouble() * (5 - 3) + 3.1, 2)  // Hyperglycémie
             : Math.Round(_random.NextDouble() * 0.5, 2); // Hypoglycémie

         return (fcCapte, pasCapte, padCapte, tgsCapte);
        } 
    }
}*/