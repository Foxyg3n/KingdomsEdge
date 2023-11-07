public class ZoneBounds {

    public Chunk leftBound;
    public Chunk rightBound;

    public int size => rightBound.x - leftBound.x + 1;
    public Chunk center => new Chunk(rightBound.x - size / 2);

}