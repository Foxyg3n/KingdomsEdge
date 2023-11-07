using System.Collections.Generic;
using UnityEngine;

public class NPCRequester : MonoBehaviour {

    private readonly List<NPC> queuedNPCs = new();
    private readonly List<NPC> lockedNPCs = new();
    private float offset => 0.3f;
    private float interval => 0.15f;

    public int lockedCount => lockedNPCs.Count;
    public bool canRequest => queuedNPCs.Count <= 3;
    public int queuePriority = 1;

    public void QueueNpc(NPC npc) {
        queuedNPCs.Add(npc);
    }

    public void UnqueueNpc(NPC npc) {
        if(lockedNPCs.Contains(npc)) UnlockNpc(npc);
        queuedNPCs.Remove(npc);
    }

    public bool IsLocked(NPC npc) {
        return lockedNPCs.Contains(npc);
    }

    public void LockNpc(NPC npc) {
        if(queuedNPCs.Contains(npc)) lockedNPCs.Add(npc);
    }

    public void UnlockNpc(NPC npc) {
        lockedNPCs.Remove(npc);
    }

    public Vector3 GetLockedPosition(NPC npc) {
        if(!lockedNPCs.Contains(npc)) return Vector3.zero;
        return GetQueueOrigin() + new Vector3(lockedNPCs.IndexOf(npc) * -interval - offset, 0, 0);
    }

    public Vector3 GetQueueOrigin() {
        return new Vector3(transform.position.x, 0, 0);
    }

    public Vector3 RequestQueuePosition() {
        return GetQueueOrigin() + new Vector3(lockedNPCs.Count * -interval - offset, 0, 0);
    }

    public NPC PopNpc() {
        if(lockedNPCs.Count == 0) return null;
        NPC npc = lockedNPCs[0];
        UnqueueNpc(npc);
        return npc;
    }

}
