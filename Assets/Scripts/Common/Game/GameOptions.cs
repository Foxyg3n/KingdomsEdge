using System.Collections.Generic;
using System.Collections;

public class GameOptions : IEnumerable<IslandOptions> {

    private List<IslandOptions> islandOptions;

    public GameOptions(IslandOptions[] islandOptions) {
        this.islandOptions = new List<IslandOptions>(islandOptions);
    }

    public IEnumerator<IslandOptions> GetEnumerator() {
        return islandOptions.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return islandOptions.GetEnumerator();
    }

}
