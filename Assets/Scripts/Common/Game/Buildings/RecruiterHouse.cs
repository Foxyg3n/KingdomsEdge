using System;

public class RecruiterHouse : Building {

    public NPCRequester npcRequester;

    public override void Initialize() {
        base.Initialize();
        npcRequester = GetComponent<NPCRequester>();
        island.village.AddNPCRequester(npcRequester);
    }

    public override void ChooseBuildingOption(object sender, EventArgs args) {
        IconID iconID = ((IconEventArgs) args).iconID;

        switch(iconID) {
            case IconID.WOODCUTTER:
                NPC woodcutter = npcRequester.PopNpc();
                if(woodcutter == null) break;
                island.SpawnEntity("Woodcutter", woodcutter.transform.position);
                Destroy(woodcutter.gameObject);
                break;
            case IconID.MINER:
                NPC miner = npcRequester.PopNpc();
                if(miner == null) break;
                island.SpawnEntity("Miner", miner.transform.position);
                Destroy(miner.gameObject);
                break;
            case IconID.FARMER:
                NPC farmer = npcRequester.PopNpc();
                if(farmer == null) break;
                island.SpawnEntity("Farmer", farmer.transform.position);
                Destroy(farmer.gameObject);
                break;
        }
    }

}
