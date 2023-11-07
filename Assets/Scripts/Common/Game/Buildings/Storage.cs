using System;
using System.Linq;

public class Storage : Building {

    private Inventory moneyInventory = new(9);
    private Inventory stoneInventory = new(9);
    private Inventory woodInventory = new(9);
    private Inventory leatherInventory = new(9);
    private Inventory ironInventory = new(9);
    private Inventory foodInventory = new(9);

    private ResourcesGUI resourcesGui;

    public override void ChooseBuildingOption(object sender, EventArgs args) {
        IconID iconID = ((IconEventArgs) args).iconID;

        switch(iconID) {
            case IconID.LEVEL_UP:
                Upgrade();
                break;
        }
    }

    public void SetResourcesGUI(ResourcesGUI resourcesGui) {
        this.resourcesGui = resourcesGui;
        UpdateGUI();
    }

    private Inventory GetInventoryByDropType(Type dropType) {
        switch(dropType) {
            case Type t when t == typeof(Wood): return woodInventory;
            case Type t when t == typeof(Stone): return stoneInventory;
            // case Type t when t == typeof(Leather): return leatherInventory;
            // case Type t when t == typeof(Iron): return ironInventory;
            // case Type t when t == typeof(Food): return foodInventory;
            default: return null;
        }
    }

    public bool IsFull(Type dropType) {
        Inventory inventory = GetInventoryByDropType(dropType);
        if(inventory == null) return false;
        return inventory.isFull;
    }

    public void AddItem(Type dropType, params Drop[] drops) {
        Inventory inventory = GetInventoryByDropType(dropType);
        if(inventory == null) return;
        inventory.AddItems(drops.ToList());
        UpdateGUI();
    }

    public void RemoveItem(Type dropType, Drop drop) {
        Inventory inventory = GetInventoryByDropType(dropType);
        if(inventory == null) return;
        inventory.RemoveItem(drop);
        UpdateGUI();
    }

    public void PassFrom(Type dropType, Inventory externalInventory) {
        Inventory inventory = GetInventoryByDropType(dropType);
        if(inventory == null) return;
        externalInventory.PassAll(inventory);
        UpdateGUI();
    }

    public void UpdateGUI() {
        if(resourcesGui == null) return;

        resourcesGui.money.count = moneyInventory.count + "";
        resourcesGui.wood.count = woodInventory.count + "";
        resourcesGui.stone.count = stoneInventory.count + "";
        resourcesGui.leather.count = leatherInventory.count + "";
        resourcesGui.iron.count = ironInventory.count + "";
        resourcesGui.food.count = foodInventory.count + "";

        resourcesGui.money.size = moneyInventory.size + "";
        resourcesGui.wood.size = woodInventory.size + "";
        resourcesGui.stone.size = stoneInventory.size + "";
        resourcesGui.leather.size = leatherInventory.size + "";
        resourcesGui.iron.size = ironInventory.size + "";
        resourcesGui.food.size = foodInventory.size + "";
    }
}