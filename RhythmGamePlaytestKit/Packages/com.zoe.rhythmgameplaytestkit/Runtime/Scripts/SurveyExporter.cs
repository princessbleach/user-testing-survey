using System.IO;
using UnityEngine;

namespace RhythmGamePlaytestKit
{
    public static class SurveyExporter
    {
        public static bool ExportSurvey(SurveyData data)
        {
            // Never save responses without consent
            if (data == null || !data.consentGiven)
            {
                Debug.LogWarning(
                    "Survey not exported: consent has not been given."
                );

                return false;
            }

            try
            {
                // Create a folder for survey responses
                string folderPath = Path.Combine(
                    Application.persistentDataPath,
                    "PlaytestKit",
                    "SurveyResponses"
                );

                Directory.CreateDirectory(folderPath);

                // Convert survey data into JSON
                string json = JsonUtility.ToJson(data, true);

                // Generate a unique filename
                string filePath = Path.Combine(
                    folderPath,
                    $"Survey_{data.sessionID}.json"
                );

                // Write JSON to disk
                File.WriteAllText(filePath, json);

                Debug.Log("Survey successfully exported: " + filePath);

                return true;
            }
            catch (System.Exception exception)
            {
                Debug.LogError(
                    "Survey export failed: " + exception.Message
                );

                return false;
            }
        }
    }
}
