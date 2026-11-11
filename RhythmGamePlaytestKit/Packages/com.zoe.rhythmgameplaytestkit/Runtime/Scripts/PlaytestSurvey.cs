
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RhythmGamePlaytestKit
{
    public class PlaytestSurvey : MonoBehaviour
    {
        private static PlaytestSurvey Instance;

        [Header("Survey Interface")]
        [SerializeField] private GameObject surveyPanel;

        [Header("Player Consent")]
        [SerializeField] private Toggle consentToggle;

        [Header("Rating Questions (1-5)")]
        [SerializeField] private Slider enjoymentSlider;
        [SerializeField] private Slider rhythmClaritySlider;
        [SerializeField] private Slider difficultySlider;
        [SerializeField] private Slider audioFeedbackSlider;

        [Header("Additional Feedback")]
        [SerializeField] private Toggle latencyToggle;
        [SerializeField] private TMP_InputField commentsInput;
        [SerializeField] private TMP_InputField bugReportInput;

        [Header("Feedback Message")]
        [SerializeField] private TMP_Text statusText;

        private void Awake()
        {
            Instance = this;

        }

        public static void Open()
        {
            if (Instance != null && Instance.surveyPanel != null)
                Instance.surveyPanel.SetActive(true);
            else
                Debug.LogWarning("PlaytestSurvey instance not found.");
        }

        public static void Close()
        {
            if (Instance != null && Instance.surveyPanel != null)
                Instance.surveyPanel.SetActive(false);
        }

        public void SubmitSurvey()
        {
            if (consentToggle == null || !consentToggle.isOn)
            {
                ShowMessage("Please provide consent before submitting.");
                return;
            }

            SurveyData data = new SurveyData();
            data.consentGiven = true;

            data.gameplayEnjoyment = Mathf.RoundToInt(enjoymentSlider.value);
            data.rhythmClarity = Mathf.RoundToInt(rhythmClaritySlider.value);
            data.difficultyRating = Mathf.RoundToInt(difficultySlider.value);
            data.audioFeedback = Mathf.RoundToInt(audioFeedbackSlider.value);

            data.latencyIssues = latencyToggle != null && latencyToggle.isOn;

            data.additionalComments = commentsInput != null ? commentsInput.text : "";
            data.bugReport = bugReportInput != null ? bugReportInput.text : "";

            bool success = SurveyExporter.ExportSurvey(data);

            if (success)
            {
                ShowMessage("Thank you! Your feedback has been saved.");
                Debug.Log("Playtest survey submitted: " + data.sessionID);
                surveyPanel.SetActive(false);
            }
            else
            {
                ShowMessage("Something went wrong. Please try again.");
            }
        }

        private void ShowMessage(string message)
        {
            if (statusText != null)
                statusText.text = message;

            Debug.Log(message);
        }
    }
}
