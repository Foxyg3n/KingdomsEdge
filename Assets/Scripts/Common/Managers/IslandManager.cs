using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IslandManager : MonoBehaviour {

    public const float GROUND_SIZE = (float) 128 / 100;
    public const float CHUNK_SIZE = (float) 32 / 100;

    private readonly Dictionary<int, Island> islands = new();
    public Island playerIsland;

    public void GenerateIslands(GameOptions gameOptions) {
        foreach(IslandOptions islandOptions in gameOptions) {
            int islandIndex = islands.Count + 1;
            bool isPlayerIsland = islandOptions.isPlayerIsland;

            Scene islandScene = SceneManager.CreateScene("Island" + islandIndex);
            Island island = GenerateIsland(islandScene, islandOptions, islandIndex);
            islands.Add(islandIndex, island);

            if(isPlayerIsland) playerIsland = island;
        }
    }

    private Island GenerateIsland(Scene scene, IslandOptions options, int islandIndex) {
        GameObject islandGameObject = PrefabUtils.InstantiatePrefab("Island", scene);
        Island island = islandGameObject.GetComponent<Island>();
        island.InitializeIsland(scene, options, islandIndex);
        return island;
    }

    public Island GetIsland(int islandId) {
        return islands[islandId];
    }

}
