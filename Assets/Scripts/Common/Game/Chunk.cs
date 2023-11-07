using UnityEngine;

public class Chunk {

    public int x;

    public Vector2 position => new Vector2(x * IslandManager.CHUNK_SIZE + IslandManager.CHUNK_SIZE / 2, 0);

    public Chunk(int x) {
        this.x = x;
    }

    public Chunk Translate(int value) {
        return new Chunk(x + value);
    }

    public override bool Equals(object obj) {
        if(obj == null || GetType() != obj.GetType()) return false;
        Chunk chunk = obj as Chunk;
        return x == chunk.x;
    }
    
    public override int GetHashCode() {
        return x;
    }

    public static bool operator ==(Chunk chunk1, Chunk chunk2) {
        return chunk1.x == chunk2.x;
    }

    public static bool operator !=(Chunk chunk1, Chunk chunk2) {
        return chunk1.x != chunk2.x;
    }
}
