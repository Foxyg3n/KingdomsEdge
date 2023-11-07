using System;
using UnityEngine;

public class AppearMockup : MonoBehaviour {

    public EventHandler enableBuildingEvent = delegate { };
    
    public void EnableBuilding() {
        enableBuildingEvent.Invoke(this, null);
    }

    public void EndAnimation() {
        GameObject.Destroy(gameObject);
    }

}
