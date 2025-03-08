/*using System;
using System.IO;
using System.Text.Json;
using Microsoft.ML;
using Microsoft.ML.Data;
using WEBAPP.Models;


public class ServiceML
{
    private static readonly string dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dataset_sante_avec_anomalie.csv");
    private static readonly string modelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "model.zip");

    private readonly MLContext mlContext;
    private ITransformer model;
    private PredictionEngine<Smartwatch, SmartwatchPrediction> predictionEngine;
    private readonly Random random = new Random();

    public ServiceML()
    {
        mlContext = new MLContext();
        model = null!;
        predictionEngine = null!;

        if (File.Exists(modelPath))
        {
            LoadModel();
        }
        else
        {
            TrainModel();
        }
    }

    private void LoadModel()
    {
        DataViewSchema modelSchema;
        model = mlContext.Model.Load(modelPath, out modelSchema);
        predictionEngine = mlContext.Model.CreatePredictionEngine<Smartwatch, SmartwatchPrediction>(model);
    }

    private void TrainModel()
    {
        var data = mlContext.Data.LoadFromTextFile<Smartwatch>(
            path: dataPath,
            hasHeader: true,
            separatorChar: ',');

        var pipeline = mlContext.Transforms.Concatenate("Features", new[] { "FC", "TA_sys", "TA_dia", "O2", "VFC" })
            .Append(mlContext.Transforms.Conversion.MapValueToKey("Anomalie"))
            .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Anomalie", "Features"))
            .Append(mlContext.Transforms.Conversion.MapKeyToValue("Anomalie"));

        model = pipeline.Fit(data);

        // Sauvegarde du modèle
        mlContext.Model.Save(model, data.Schema, modelPath);
        LoadModel();
    }
    
    public Smartwatch GenerateRandomSmartwatchData()
    {
        return new Smartwatch
        {
            ADRMAC = "00:11:22:33:44:55",
            FC = random.Next(60, 120), // Fréquence cardiaque entre 60 et 120 bpm
            TA_sys = random.Next(90, 140), // Tension artérielle systolique entre 90 et 140 mmHg
            TA_dia = random.Next(60, 90), // Tension artérielle diastolique entre 60 et 90 mmHg
            O2 = random.Next(90, 100), // Saturation en oxygène entre 90 et 100%
            VFC = random.Next(20, 100) // Variabilité de la fréquence cardiaque entre 20 et 100 ms
        };
    }

    public Smartwatch GetSmartwatchFromJson(string json)
    {
        return JsonSerializer.Deserialize<Smartwatch>(json) ?? throw new ArgumentException("JSON invalide");
    }

    public string Predict(Smartwatch smartwatchData)
    {
        var prediction = predictionEngine.Predict(smartwatchData);
        return prediction.PredictedLabel;
    }
}*/
