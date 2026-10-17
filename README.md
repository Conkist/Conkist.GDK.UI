# Conkist.GDK.UI

<p>
  <img src="https://img.shields.io/badge/Unity-2022.3+-black?logo=unity" />
  <img src="https://img.shields.io/badge/C%23-dev-239120?logo=csharp" />
  <img src="https://img.shields.io/badge/version-0.1.0-blue" />
  <img src="https://img.shields.io/badge/license-MIT-green" />
</p>

UI toolkit for the **Conkist Game Development Kit** — ready-to-use components and layout utilities built on uGUI and TextMesh Pro to accelerate interface development in Unity.

```json
"me.conkist.gdk.ui": "https://github.com/Conkist/Conkist.GDK.UI.git"
```

---

## Components

### Dialog

A configurable dialog system for in-game conversations, confirmations and branching choices.

| Script | Responsibility |
|---|---|
| `DialogBoxBehaviour` | Core dialog box controller — show, hide, populate content |
| `DialogOption` | Single selectable option (text + callback) |
| `DialogOptionHandler` | Manages a list of options and routes selections |

**Sample:** a working Dialog Call example is included in `Samples~/DialogBox` and available via the Package Manager samples UI.

---

### Loading Canvas

`LoadingCanvas` — a full-screen overlay for async loading transitions. Attach to a persistent canvas and call `Show()` / `Hide()` around any loading operation.

---

### Progress Bar

`ProgressBar` — a simple normalized progress bar component. Set `Value` (0–1) to drive fill amount. Compatible with any uGUI `Image` fill type.

---

### Safe Area Layout

`SafeArea` — automatically adjusts a `RectTransform` to respect device safe area insets (notch, home indicator). Attach to any panel that needs to stay clear of system UI on mobile.

---

## Install

Add to `Packages/manifest.json`:

```json
{
  "dependencies": {
    "me.conkist.gdk.ui": "https://github.com/Conkist/Conkist.GDK.UI.git",
    "com.unity.2d.sprite": "1.0.0",
    "com.unity.ugui": "1.0.0",
    "com.unity.textmeshpro": "3.0.6"
  }
}
```

---

## Part of

**[Conkist GDK](https://github.com/Conkist)** — Game Development Kit for Unity · [conkist.me](https://conkist.me)
