
using UnityEngine;

public class StoneGenerator : ResourceGenerator {

    public void Generate(Island island, Zones zones) {
        Chunk chunk = island.leftBound;
        for(int i = 0; i < zones.zoneArea.Length; i++) {
            if(RollChance(0.05f) && zones.zoneArea[i] != ZoneType.VILLAGE) island.SpawnResource("Stone", chunk);
            chunk = new Chunk(chunk.x + 1);
        }
    }

    private bool RollChance(float chance) {
        return Random.Range(0f, 1f) < chance;
    }

}
