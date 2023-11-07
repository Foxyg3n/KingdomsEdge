public class ForestZone : Zone {

    private Island island;

    public ForestZone() : base(ZoneType.FOREST) {}

    public override void InitializeZone(Island island) {
        this.island = island;

        SpawnTrees();
        // DrawMarkers();
    }

    public void SpawnTrees() {
        bool[] treeSpawnMap = new bool[bounds.size];
        float[] treeMap = ZoneGenerator.GenerateHeatMap(bounds.size, 15);
        float density = 0.5f;

        for(int i = 0; i < treeMap.Length; i++) {
            treeSpawnMap[i] = treeMap[i] > 1f - density;
        }

        Chunk chunk = bounds.leftBound;
        int forestOffset = 3;
        for(int i = 0; i < treeSpawnMap.Length; i++) {
            if(treeSpawnMap[i] || i < forestOffset || i > treeSpawnMap.Length - forestOffset) island.SpawnResource("Tree", chunk);
            chunk = chunk.Translate(1);
        }

        // List<Chunk> zoneCores = ZoneGenerator.GenerateZoneCores(this.size, 10f);
        // foreach(Chunk zoneCore in zoneCores) {
        //     int treeCount = Random.Range(3, 6);
        //     Chunk startingChunk = new Chunk(zoneCore.x - (treeCount / 2));
        //     for(int i = 0; i < treeCount; i++) {
        //         Chunk chunk = new Chunk(startingChunk.x + i);
        //         Vector2 offset = new Vector2(Random.Range(-0.12f, 0.12f), 0);
        //         if(islandSize / 4 * IslandManager.CHUNK_SIZE - Mathf.Abs(transform.position.x + chunk.position.x) < 0) return;
        //         GameObject treeObject = PrefabUtils.InstantiatePrefabRelative("Zones/Tree", chunk.position + offset, transform);
        //         float scaleOffset = Random.Range(0.75f, 1.25f);
        //         treeObject.transform.localScale = new Vector3(scaleOffset, scaleOffset, treeObject.transform.localScale.z);
        //     }
        // }
    }

    public void DrawMarkers() {
        for(int i = 0; i < bounds.size; i++) {
            Chunk chunk = new Chunk(bounds.leftBound.x + i);
            island.SpawnMarker("ForestMarker", chunk);
        }
    }

}
