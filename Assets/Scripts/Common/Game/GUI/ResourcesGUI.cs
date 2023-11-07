using UnityEngine;

public class ResourcesGUI : MonoBehaviour {
    
    public ResourceDisplay money;
    public ResourceDisplay stone;
    public ResourceDisplay wood;
    public ResourceDisplay leather;
    public ResourceDisplay iron;
    public ResourceDisplay food;

    public void Awake() {
        App.IslandManager.playerIsland.GetBuilding<Storage>().SetResourcesGUI(this);
    }

}
