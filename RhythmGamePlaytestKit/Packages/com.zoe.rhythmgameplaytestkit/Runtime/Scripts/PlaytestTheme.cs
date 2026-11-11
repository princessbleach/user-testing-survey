
using UnityEngine;

namespace RhythmGamePlaytestKit
{
    public class PlaytestTheme : MonoBehaviour
    {
        [Header("Custom Survey Colour Scheme")]

        [SerializeField]
        private Color backgroundColour = new Color32(16, 17, 31, 255);

        [SerializeField]
        private Color surfaceColour = new Color32(32, 35, 61, 255);

        [SerializeField]
        private Color primaryColour = new Color32(121, 87, 255, 255);

        [SerializeField]
        private Color accentColour = new Color32(66, 200, 255, 255);

        public Color BackgroundColour => backgroundColour;
        public Color SurfaceColour => surfaceColour;
        public Color PrimaryColour => primaryColour;
        public Color AccentColour => accentColour;
    }
}
