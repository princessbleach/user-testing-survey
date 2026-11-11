using System;
using UnityEngine;

namespace RhythmGamePlaytestKit
{
    [Serializable]
    public class SurveyData
    {
        // Session information
        public string sessionID;
        public string dateTime;

        // Consent
        public bool consentGiven;

        // Automatically collected system information
        public string gameVersion;
        public string platform;
        public string deviceModel;

        // Quantitative feedback (1-5)
        public int gameplayEnjoyment;
        public int rhythmClarity;
        public int difficultyRating;
        public int audioFeedback;
        public bool latencyIssues;

        // Qualitative feedback
        public string additionalComments;
        public string bugReport;

        // Create a new survey session
        public SurveyData()
        {
            sessionID = Guid.NewGuid().ToString();
            dateTime = DateTime.UtcNow.ToString("o");

            consentGiven = false;

            gameVersion = Application.version;
            platform = Application.platform.ToString();
            deviceModel = SystemInfo.deviceModel;
        }
    }
}
