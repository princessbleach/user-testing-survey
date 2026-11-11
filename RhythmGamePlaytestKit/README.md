# Rhythm Game Playtest Kit

A reusable Unity user-testing survey package designed for rhythm games. The package collects player feedback to support quality assurance, usability testing and iterative game development.

## Features

- Player consent before submitting responses.
- Automatic session ID and UTC timestamp generation.
- Automatic game version, platform and device information.
- 1–5 player rating questions.
- Rhythm-specific feedback, including enjoyment, rhythm clarity, difficulty and audio.
- Latency issue reporting.
- Additional comments and bug reports.
- JSON response export.
- Customisable survey colour themes.

## Installation

1. Add the package to your Unity project.
2. Locate the `RhythmGameSurvey` prefab.
3. Drag the prefab into your scene.
4. Configure the survey theme using the Inspector.
5. Connect the survey opening and submission events to your game.

## Customisation

The `PlaytestTheme` component allows developers to customise four colours:

- Background
- Surface
- Primary
- Accent

The `PlaytestThemeApplier` component applies these colours to assigned UI elements.

## Data Collection

Survey responses are exported as individual JSON files.

Files are stored in:

`Application.persistentDataPath/PlaytestKit/SurveyResponses/`

Responses include a generated session ID, timestamp, game version, platform, device model, ratings, latency feedback, comments and bug reports.

Responses are only exported when the player provides consent. No names, email addresses or contact details are collected.

## Technologies

- Unity 6
- C#
- Unity UI
- TextMeshPro
- JSON serialisation

## Project Structure

- `Runtime/Scripts` – Survey and theme functionality.
- `Runtime/Prefabs` – Reusable survey prefab.
- `Samples~/Demo` – Demonstration scene.
- `Documentation~` – Additional documentation.

## Author

Zoe Efstathiou

University for the Creative Arts – BSc Games Development