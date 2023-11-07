using System;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public abstract class Building : MonoBehaviour {

    public Island island { private set; get; }
    public int width { private set; get; }
    public MenuIcon[] menuIcons;
    protected BuildingMenu menu;
    [SerializeField] private List<Sprite> upgradeTextures;
    private int level = 0;
    private int maxLevel => upgradeTextures.Capacity - 1;

    public virtual void Initialize() {
        this.menu = transform.GetChild(0).GetComponent<BuildingMenu>();
        menu.InitializeMenu(menuIcons);
        if(menu.initialized) menu.buildingOptionChooseEvent += ChooseBuildingOption;
        gameObject.SetActive(false);
        menu.DisableMenu();
        AppearMockup mockup = PrefabUtils.InstantiatePrefabAbsolute("Buildings/AnimationMockups/AppearMockup", transform.position - new Vector3(0, 0.27f), transform.parent).GetComponent<AppearMockup>();
        mockup.enableBuildingEvent += EnableBuilding;
    }

    public void InitIsland(Island island) {
        if(this.island == null) this.island = island;
    }

    protected void OnTriggerEnter2D(Collider2D collider) {
        menu.EnableMenu();
    }

    protected void OnTriggerExit2D(Collider2D collider) {
        menu.DisableMenu();
    }

    public abstract void ChooseBuildingOption(object sender, EventArgs args);

    public void EnableBuilding(object sender, EventArgs args) {
        gameObject.SetActive(true);
    }

    public int GetLevel() {
        return level;
    }

    public void SetLevel(int level) {
        if(level < 0 || level > maxLevel) return;
        this.level = level;
        GetComponent<SpriteRenderer>().sprite = upgradeTextures[level];
    }

    public void Upgrade() {
        if(level < 0 || level >= maxLevel) return;
        gameObject.SetActive(false);
        menu.DisableMenu();
        AppearMockup mockup = PrefabUtils.InstantiatePrefabAbsolute("Buildings/AnimationMockups/AppearMockup", transform.position - new Vector3(0, 0.27f), transform.parent).GetComponent<AppearMockup>();
        mockup.enableBuildingEvent += EnableBuilding;
        SetLevel(GetLevel() + 1);
    }

}
