# Rhythm Game Playtest Kit

**A reusable Unity user-testing and feedback collection package designed for rhythm games.**

The Rhythm Game Playtest Kit allows developers to collect structured player feedback during playtesting sessions. It includes a customisable survey interface, consent management, automatic session identification, local JSON backups and remote data submission through Google Sheets.

## Features

- **Player consent:** Responses are only collected after the player provides consent.
- **Automatic session identification:** Each submission receives a unique GUID.
- **Timestamp recording:** Submission dates and times are recorded in UTC.
- **Game information:** Automatically records the game version, platform and device model.
- **Quantitative feedback:** Four rating questions covering gameplay enjoyment, rhythm clarity, difficulty and audio feedback.
- **Qualitative feedback:** Players can submit additional comments and bug reports.
- **Latency reporting:** Players can indicate whether they experienced latency issues.
- **Customisable themes:** Developers can configure four survey colours: background, surface, primary and accent.
- **Local data storage:** Responses are saved as individual JSON files on the player's device.
- **Remote data collection:** Responses are submitted to a connected Google Sheets spreadsheet.

## Requirements

- Unity 6
- Unity UI
- TextMeshPro
- Internet connection for remote submissions
- A Google account and configured Google Sheets backend

## Installation

The package can be installed through Unity Package Manager using its Git repository URL.

1. Open the Unity project.
2. Navigate to **Window → Package Management → Package Manager**.
3. Click the `+` button.
4. Select **Add package from git URL**.
5. Enter:

`https://github.com/princessbleach/user-testing-survey.git?path=/RhythmGamePlaytestKit/Packages/com.zoe.rhythmgameplaykit`

6. Click Add and wait for Unity to import the package.

## Setup

### 1. Add the survey prefab

1. Locate the package in the Project window.
2. Navigate to `Runtime/Prefabs`.
3. Drag the `RhythmGameSurvey` prefab into your scene.
4. Ensure the scene contains an EventSystem to allow mouse and UI interaction.

### 2. Configure the survey theme

The `PlaytestTheme` component exposes four customisable colours:

- Background Colour
- Surface Colour
- Primary Colour
- Accent Colour

The `PlaytestThemeApplier` component applies these colours to the UI elements assigned to its colour arrays.

Developers can customise the survey appearance through the Unity Inspector.

### 3. Configure remote data collection

The package uses Google Apps Script as a web endpoint to receive survey responses and append them to a Google Sheets spreadsheet.

To configure a backend:

1. Create a Google spreadsheet.
2. Create a sheet tab named `Survey Responses`.
3. Set up a Google Apps Script web app with a `doPost` function to receive JSON submissions.
4. Deploy the script as a Web App.
5. Configure the deployed Web App URL in `SurveyExporter.cs`.

The deployed endpoint must be accessible to the game without requiring the player to sign into Google.

**Note:** The current package implementation uses a configured Google Apps Script endpoint. Developers should replace this with their own endpoint before using the package for their own playtesting projects.

### 4. Connect the survey to gameplay

The `PlaytestSurvey` component provides methods for opening and closing the survey:

- `PlaytestSurvey.Open()`
- `PlaytestSurvey.Close()`

These static methods can be called from other game scripts.

The Submit button must be connected to `PlaytestSurvey.SubmitSurvey()` through its Button On Click event.

## Data Collection

Survey responses contain the following information:

| Field | Description |
|---|---|
| Session ID | Unique identifier for each submission |
| Date Time | UTC submission timestamp |
| Game Version | Current Unity application version |
| Platform | Platform running the game |
| Device Model | Device model information |
| Gameplay Enjoyment | Rating from 1–5 |
| Rhythm Clarity | Rating from 1–5 |
| Difficulty Rating | Rating from 1–5 |
| Audio Feedback | Rating from 1–5 |
| Latency Issues | Boolean response |
| Additional Comments | Optional written feedback |
| Bug Report | Optional bug description |

### Local Storage

A local JSON backup is created for each successful local export.

Files are stored at:

`Application.persistentDataPath/PlaytestKit/SurveyResponses/`

Each file is named using its generated session ID.

### Remote Storage

Survey responses are sent to the configured Google Apps Script endpoint using an HTTP POST request.

The Apps Script receives the JSON data and appends it as a new row in Google Sheets.

A successful local save and a successful remote upload are handled separately. If remote submission fails, the local JSON backup remains available.

## Privacy and Consent

The survey requires explicit player consent before creating or submitting survey data.

No names, email addresses or contact details are collected.

The current implementation records technical information, including platform and device model, alongside survey responses. Players should be informed that their responses are transmitted to and stored in the configured Google Sheets backend.

Developers are responsible for providing appropriate privacy information, restricting spreadsheet access and securing their submission endpoint before public deployment.

## Package Structure

```text
com.zoe.rhythmgameplaykit/
│
├── Runtime/
│   ├── Scripts/
│   │   ├── PlaytestSurvey.cs
│   │   ├── SurveyData.cs
│   │   ├── SurveyExporter.cs
│   │   ├── PlaytestTheme.cs
│   │   └── PlaytestThemeApplier.cs
│   │
│   └── Prefabs/
│       └── RhythmGameSurvey.prefab
│
├── Samples~/
│   └── Demo/
│
├── Documentation~/
│
├── package.json
└── README.md
```

## Technologies

- Unity 6
- C#
- Unity UI
- TextMeshPro
- UnityWebRequest
- JSON serialisation
- Google Apps Script
- Google Sheets

## Author

Zoe Efstathiou

**Repository:** https://github.com/princessbleach/user-testing-survey