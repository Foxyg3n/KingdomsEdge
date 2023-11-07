using UnityEngine;

public static class ZoneGenerator {

    private const float SCALE = 10f;
    private const float DENSITY = 0.6f;

    public static Zones GenerateZones(int chunkSize) {
        ZoneType[] forestZone = GenerateForest(chunkSize);
        ZoneType[] villageZone = GenerateVillage(chunkSize);

        ZoneType[] zoneArea = MergeZones(forestZone, villageZone);
        return new Zones(chunkSize, zoneArea);
    }

    private static ZoneType[] MergeZones(params ZoneType[][] zones) {
        if(zones.Length == 0) return new ZoneType[0];

        ZoneType[] zonesCombined = new ZoneType[zones[0].Length];
        for(int i = 0; i < zonesCombined.Length; i++) {
            zonesCombined[i] = ZoneType.WILDERNESS;

            for(int j = zones.Length - 1; j >= 0; j--) {
                if(zones[j][i] != ZoneType.WILDERNESS) {
                    zonesCombined[i] = zones[j][i];
                    break;
                }
            }
        }

        return zonesCombined;
    }

    private static ZoneType[] GenerateVillage(int size) {
        ZoneType[] zoneArea = new ZoneType[size];
        int villageSize = 20;

        for(int i = 0; i < size; i++) {
            zoneArea[i] = ZoneType.WILDERNESS;
        }

        int villageStartPosition = size / 2 - villageSize / 2;
        for(int i = villageStartPosition; i < villageStartPosition + villageSize; i++) {
            zoneArea[i] = ZoneType.VILLAGE;
        }

        return zoneArea;
    }

    private static ZoneType[] GenerateForest(int size) {
        ZoneType[] zoneArea = new ZoneType[size];
        float[] zoneHeatMap = GenerateHeatMap(size, SCALE);

        for(int i = 0; i < zoneHeatMap.Length; i++) {
            zoneArea[i] = zoneHeatMap[i] > 1f - DENSITY ? ZoneType.FOREST : ZoneType.WILDERNESS;
        }

        return zoneArea;
    }

    public static float[] GenerateHeatMap(int width, float scale) {
        float offsetX = Random.Range(0f, 99999f);
        float offsetY = Random.Range(0f, 99999f);
        float[] heatMap = new float[width];
        for(int x = 0; x < width; x++) {
            float xCoord = (float) x / width * scale;
            float noise = Mathf.PerlinNoise(xCoord + offsetX, offsetY);
            heatMap[x] = noise;
        }

        return heatMap;
    }

}
