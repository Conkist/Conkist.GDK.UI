using System;
using UnityEngine;
using UnityEngine.UI;

namespace Conkist.UI
{
    [AddComponentMenu("UI/Conkist/Progress Bar", 0)]
    [ExecuteAlways]
    /// <summary>
    /// A standard progressbar that can be moved between a minimum and maximum value.
    /// </summary>
    /// <remarks>
    /// The progressbar component is a UIBehaviour that controls a fill from left to right. The fill, when used, spans from the minimum value to the current value.
    /// The anchors of the fill is driven by the Progress. The fill can be direct children of the GameObject with the ProgressBar, or intermediary RectTransforms can be placed in between for additional control.
    /// When a change to the progressbar value occurs, a callback is sent to any registered listeners of realima.package.ui.ProgressBar.onValueChanged.
    /// </remarks>
    public class ProgressBar : Slider
    {
        
    }
}
