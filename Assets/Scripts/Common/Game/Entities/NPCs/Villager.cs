public class Villager : NPC {

    public override float speed => 0.15f;
    
    private void Start() {
        goalSelector.AddGoal(0, new AcquireJobGoal(this));
        // this.goalSelector.AddGoal(1, new WanderingGoal(this));
    }

}
