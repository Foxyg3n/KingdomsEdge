public class IslandOptions {

    public int islandSize { get; private set; }
    public bool isPlayerIsland { get; private set; }

    public IslandOptions(int islandSize, bool isPlayerIsland) {
        this.islandSize = islandSize;
        this.isPlayerIsland = isPlayerIsland;
    }

}
