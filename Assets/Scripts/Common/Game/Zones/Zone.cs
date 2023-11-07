public abstract class Zone {

    public ZoneBounds bounds = new ZoneBounds();
    public ZoneType type;

    public Zone(ZoneType type) {
        this.type = type;
    }

    public abstract void InitializeZone(Island island);

    public static Zone InstantiateFromType(ZoneType type) {
        switch (type) {
            case ZoneType.FOREST:
                return new ForestZone();
            case ZoneType.VILLAGE:
                return new VillageZone();
            default:
                return null;
        }
    }

}
