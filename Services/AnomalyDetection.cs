using System;
using System.Threading.Tasks;
using Azure.Core;
using LibrarySSMS;
using WEBAPPP.DTO;
using LibrarySSMS.Models;
using LibrarySSMS.Enums;



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

       public bool AnomalydetectionVariante2(Guid patientId, int age, bool Gender, UserState userState)
        {
            var P= 0.0; //probas d'anomalie null au depart
            var alpha=0.3; //coeficient de lissage 

           //chargement du model onnxDNN

            while(P<0.7)
            {
              for(int i=0;i<5;i++) //simulation ~5seconde de lecture 
              {
                //implementation de la lecture des valeur par onnxDNN 

                P=alpha*P+(1-alpha)*P;
              }

              if (P>=0.4 && P<=0.7)
              {
               //pas de cas grave a simple avertissement
               //appeler le service de notification pour notifier le patient au niveau du dashboard
                return false;
              }
            }
           //sortie de ka boucle anglobante veut dire probas elevé d'anomalie 70% ou plus
           //appel du service OBU ou Montre ou Telephone tout depents de l'etat du user
           return true;
        }
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       
       

    
       
       
       
       

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
}