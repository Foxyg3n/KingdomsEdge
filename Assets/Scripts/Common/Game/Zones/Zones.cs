using System.Collections.Generic;

public class Zones {

    private List<ResourceGenerator> generators = new List<ResourceGenerator>();

    public ZoneType[] zoneArea;
    public List<Zone> zones = new List<Zone>();
    public Zone villageZone;

    public Zones(int chunkSize, ZoneType[] zonesArea) {
        this.zoneArea = zonesArea;
        Zone currentZone = null;
        for(int i = 0; i < zonesArea.Length; i++) {
            if(currentZone == null && zonesArea[i] != ZoneType.WILDERNESS) {
                currentZone = Zone.InstantiateFromType(zonesArea[i]);
                currentZone.bounds.leftBound = new Chunk(i - chunkSize / 2);
            } else if(currentZone != null) {
                if(zonesArea[i] == ZoneType.WILDERNESS) {
                    currentZone.bounds.rightBound = new Chunk(i - 1 - chunkSize / 2);
                    zones.Add(currentZone);
                    if(currentZone.type == ZoneType.VILLAGE) villageZone = currentZone;
                    currentZone = null;
                } else if(zonesArea[i] != currentZone.type) {
                    currentZone.bounds.rightBound = new Chunk(i - 1 - chunkSize / 2);
                    if(currentZone.type == ZoneType.VILLAGE) villageZone = currentZone;
                    zones.Add(currentZone);
                    currentZone = Zone.InstantiateFromType(zonesArea[i]);
                    currentZone.bounds.leftBound = new Chunk(i - chunkSize / 2);
                }
            }
        }

        if(currentZone != null) {
            currentZone.bounds.rightBound = new Chunk(zonesArea.Length - chunkSize / 2);
            zones.Add(currentZone);
        }
    }

    public void Initialize(Island island) {
        foreach(Zone zone in zones) {
            zone.InitializeZone(island);
        }
        foreach(ResourceGenerator generator in generators) {
            generator.Generate(island, this);
        }
    }

    public void RecalculateVillageBounds() {
        for(int i = 0; i < zones.Count; i++) {
            if(zones[i].type == ZoneType.VILLAGE) {
                Zone villageZone = zones[i];
                if(i != 0) {
                    Chunk leftVillageBound = zones[i - 1].bounds.rightBound.Translate(1);
                    villageZone.bounds.leftBound = leftVillageBound;
                }
                if(i != zones.Count - 1) {
                    Chunk rightVillageBound = zones[i + 1].bounds.leftBound.Translate(-1);
                    villageZone.bounds.rightBound = rightVillageBound;
                }
                this.villageZone = villageZone;
            }
        }
    }

    public void AddGenerator(ResourceGenerator generator) {
        generators.Add(generator);
    }

}
