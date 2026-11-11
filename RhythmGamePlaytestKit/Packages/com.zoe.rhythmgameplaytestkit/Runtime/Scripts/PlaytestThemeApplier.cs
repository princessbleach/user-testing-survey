using UnityEngine;
using UnityEngine.UI;

namespace RhythmGamePlaytestKit
{
    [ExecuteAlways]
    public class PlaytestThemeApplier : MonoBehaviour
    {
        [Header("Theme Configuration")]
        [SerializeField] private PlaytestTheme theme;

        [Header("Background Elements")]
        [SerializeField] private Graphic[] backgroundElements;

        [Header("Surface Elements")]
        [SerializeField] private Graphic[] surfaceElements;

        [Header("Primary Elements")]
        [SerializeField] private Graphic[] primaryElements;

        [Header("Accent Elements")]
        [SerializeField] private Graphic[] accentElements;

        private void OnValidate()
        {
            ApplyTheme();
        }

        public void ApplyTheme()
        {
            if (theme == null)
                return;

            ApplyColour(backgroundElements, theme.BackgroundColour);
            ApplyColour(surfaceElements, theme.SurfaceColour);
            ApplyColour(primaryElements, theme.PrimaryColour);
            ApplyColour(accentElements, theme.AccentColour);
        }

        private void ApplyColour(Graphic[] elements, Color colour)
        {
            if (elements == null)
                return;

            foreach (Graphic element in elements)
            {
                if (element != null)
                    element.color = colour;
            }
        }
    }
}
