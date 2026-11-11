using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace RhythmGamePlaytestKit
{
    public static class SurveyExporter
    {
        private const string GoogleSheetsURL =
            "https://script.google.com/macros/s/AKfycbz6NOvkvlQVO24Y97P_N73Fv5V6uETUbDEBCo_DTRTBIrXrPdqi-KrjojrTQzCl-UTmMA/exec";

        // Save a local JSON backup
        public static bool ExportSurvey(SurveyData data)
        {
            if (data == null || !data.consentGiven)
            {
                Debug.LogWarning("Survey not exported: consent required.");
                return false;
            }

            try
            {
                string folderPath = Path.Combine(
                    Application.persistentDataPath,
                    "PlaytestKit",
                    "SurveyResponses"
                );

                Directory.CreateDirectory(folderPath);

                string json = JsonUtility.ToJson(data, true);

                string filePath = Path.Combine(
                    folderPath,
                    $"Survey_{data.sessionID}.json"
                );

                File.WriteAllText(filePath, json);

                Debug.Log("Local survey backup saved: " + filePath);
                return true;
            }
            catch (Exception exception)
            {
                Debug.LogError("Local export failed: " + exception.Message);
                return false;
            }
        }

        // Upload survey responses to Google Sheets
        public static IEnumerator SendToGoogleSheets(
            SurveyData data,
            Action<bool> onComplete)
        {
            if (data == null || !data.consentGiven)
            {
                onComplete?.Invoke(false);
                yield break;
            }

            string json = JsonUtility.ToJson(data);

            using (UnityWebRequest request =
                UnityWebRequest.Post(
                    GoogleSheetsURL,
                    json,
                    "application/json"
                ))
            {
                request.timeout = 20;

                yield return request.SendWebRequest();

                bool success =
                    request.result == UnityWebRequest.Result.Success &&
                    request.downloadHandler != null &&
                    request.downloadHandler.text.Contains("Success");

                if (success)
                {
                    Debug.Log("Survey successfully uploaded to Google Sheets!");
                }
                else
                {
                    Debug.LogError(
                        "Google Sheets upload failed: " +
                        request.error + " | " +
                        (request.downloadHandler != null
                            ? request.downloadHandler.text
                            : "No response received.")
                    );
                }

                onComplete?.Invoke(success);
            }
        }
    }
}
