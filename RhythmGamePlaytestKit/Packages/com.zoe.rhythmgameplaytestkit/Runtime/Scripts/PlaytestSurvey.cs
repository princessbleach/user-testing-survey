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

        private bool isSubmitting;

        private void Awake()
        {
            Instance = this;
        }

        public static void Open()
        {
            if (Instance != null && Instance.surveyPanel != null)
                Instance.surveyPanel.SetActive(true);
        }

        public static void Close()
        {
            if (Instance != null && Instance.surveyPanel != null)
                Instance.surveyPanel.SetActive(false);
        }

        public void SubmitSurvey()
        {
            if (isSubmitting)
                return;

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

            data.additionalComments =
                commentsInput != null ? commentsInput.text : "";

            data.bugReport =
                bugReportInput != null ? bugReportInput.text : "";

            // Save local backup first
            bool savedLocally = SurveyExporter.ExportSurvey(data);

            if (!savedLocally)
            {
                ShowMessage("Could not save feedback. Please try again.");
                return;
            }

            // Upload to Google Sheets
            isSubmitting = true;
            ShowMessage("Uploading your feedback...");

            StartCoroutine(
                SurveyExporter.SendToGoogleSheets(data, (uploaded) =>
                {
                    isSubmitting = false;

                    if (uploaded)
                    {
                        ShowMessage("Thank you! Your feedback has been submitted.");
                        surveyPanel.SetActive(false);
                    }
                    else
                    {
                        ShowMessage("Saved locally, but online upload failed.");
                    }
                })
            );
        }

        private void ShowMessage(string message)
        {
            if (statusText != null)
                statusText.text = message;

            Debug.Log(message);
        }
    }
}
