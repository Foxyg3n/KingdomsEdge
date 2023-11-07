using System;
using UnityEngine;

public class Icon : MonoBehaviour {

    public IconID iconID;
    public float tilt;

    public EventHandler clickEvent = delegate { };
    public EventHandler hoverEvent = delegate { };

    private void OnMouseDown() {
        clickEvent.Invoke(this, EventArgs.Empty);
    }

    private void OnMouseEnter() {
        hoverEvent.Invoke(this, EventArgs.Empty);
    }

}
