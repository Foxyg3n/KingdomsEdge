using System.Collections.Generic;
using UnityEngine;

public class Village {

    private Island island;
    public Zone villageZone;

    private List<NPCRequester> npcRequesters = new();

    public Village(Zone villageZone) {
        this.villageZone = villageZone;
    }

    public void Initialize(Island island) {
        this.island = island;

        // DrawMarkers();
        CreateVillage();
    }

    public void DrawMarkers() {
        Chunk chunk = villageZone.bounds.leftBound;
        for(int i = 0; i < villageZone.bounds.size; i++) {
            island.SpawnMarker("VillageMarker", chunk);
            chunk = chunk.Translate(1);
        }
        foreach(Zone zone in island.zones.zones) {
            if(zone.type == ZoneType.FOREST) {
                Zone forestZone = zone;
                chunk = forestZone.bounds.leftBound;
                for(int i = 0; i < forestZone.bounds.size; i++) {
                    island.SpawnMarker("ForestMarker", chunk);
                    chunk = chunk.Translate(1);
                }
            }
        }
    }

    public void CreateVillage() {
        island.SpawnBuilding<Storage>(villageZone.bounds.center.Translate(-3));
        island.SpawnBuilding<TownHall>(villageZone.bounds.center);
        island.SpawnBuilding<RecruiterHouse>(villageZone.bounds.center.Translate(3));

        for(int i = 0; i < 3; i++) {
            island.SpawnEntity("Villager", new Vector2(Random.Range(villageZone.bounds.leftBound.position.x, villageZone.bounds.rightBound.position.x), 0));
        }
    }

    public void RecalculateBounds() {
        // island.RemoveMarkers();
        island.zones.RecalculateVillageBounds();
        // DrawMarkers();
    }

    public void AddNPCRequester(NPCRequester npcRequester) {
        npcRequesters.Add(npcRequester);
    }

    public void RemoveNPCRequester(NPCRequester npcRequester) {
        npcRequesters.Remove(npcRequester);
    }

    public bool HasFreeRequester() {
        foreach(NPCRequester requester in npcRequesters) {
            if(requester.canRequest) return true;
        }
        return false;
    }

    public NPCRequester GetFreeNPCRequester() {
        NPCRequester priorRequester = null;
        foreach(NPCRequester requester in npcRequesters) {
            if(requester.canRequest && (priorRequester == null || requester.queuePriority < priorRequester.queuePriority)) priorRequester = requester;
        }
        return priorRequester;
    }

}
