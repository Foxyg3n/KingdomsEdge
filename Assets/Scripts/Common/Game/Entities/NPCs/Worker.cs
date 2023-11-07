using System;

public abstract class Worker : NPC {

    public Inventory inventory = new(3);
    public override float speed => 0.15f;
    
    public abstract Type GetJobResourceType();
    public abstract Type GetJobDropType();
    
    private void Start() { 
        goalSelector.AddGoal(0, new WorkGoal(this));
        goalSelector.AddGoal(1, new WanderingGoal(this));
    }

    public void Collect(Drop drop) {
        if(inventory.isFull) return;
        InterruptGoTo();
        drop.Collect();
        inventory.AddItem(drop);
    }
    
}