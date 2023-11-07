using UnityEngine;

public class AcquireJobGoal : Goal<Villager> {

    private bool shouldWalk;
    private Vector3 destination;
    private int queuePlace;
    private NPCRequester npcRequester;

    public AcquireJobGoal(Villager entity) : base(entity) {}

    public override bool MeetsRequirements() {
        return entity.village.HasFreeRequester();
    }

    public override void Execute() {
        if(npcRequester != null && npcRequester.IsLocked(entity)) {
            if(shouldWalk) {
                if(!entity.isWalking) entity.GoTo(destination.x, () => shouldWalk = false);
            } else {
                destination = npcRequester.GetLockedPosition(entity);
                if(!(Mathf.Abs(entity.transform.position.x - destination.x) < 0.01f)) shouldWalk = true;
            }
        } else {
            if(shouldWalk) {
                if(!entity.isWalking || queuePlace != npcRequester.lockedCount) {
                    destination = npcRequester.RequestQueuePosition();
                    queuePlace = npcRequester.lockedCount;
                    entity.GoTo(destination.x, () => {
                        shouldWalk = false;
                        transform.localScale = new Vector3(1, 1, 1);
                        npcRequester.LockNpc(entity);
                    });
                }

                destination = npcRequester.RequestQueuePosition(); // can theoretically be optimized
            } else {
                shouldWalk = true;
                npcRequester = entity.village.GetFreeNPCRequester();
                npcRequester.QueueNpc(entity);
                queuePlace = npcRequester.lockedCount;
            }
        }
    }

    public override void Reset() {
        shouldWalk = false;
        npcRequester = null;
    }

}
