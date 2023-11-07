using UnityEngine;

public static class LandscapeGenerator {

    private static Sprite[] groundSprites = new Sprite[] {
        ResourceUtils.GetSprite("Grounds/ground_0"),
        ResourceUtils.GetSprite("Grounds/ground_1"),
        ResourceUtils.GetSprite("Grounds/ground_2"),
        ResourceUtils.GetSprite("Grounds/ground_3")
    };

    public static void GenerateGround(Transform groundLayer, int size) {
        float groundWidth = IslandManager.GROUND_SIZE;
        float xPos = groundWidth * -size / 2 + groundWidth / 2;
        for(int i = 0; i < size; i++) {
            int spriteIndex = Random.Range(0, groundSprites.Length);
            GameObject ground = PrefabUtils.InstantiatePrefabRelative("Ground", new Vector2(xPos, 0.01f), groundLayer);
            ground.GetComponent<SpriteRenderer>().sprite = groundSprites[spriteIndex];
            xPos += groundWidth;
        }
    }

    public static void GenerateLandscape(Transform frontMountainLayer, Transform midMountainLayer, Transform backMountainLayer, int size) {
        float mountainWidth = 6.4f;
        float xPos = mountainWidth * -size / 2 + mountainWidth / 2;
        for(int i = 0; i < size; i++) {
            PrefabUtils.InstantiatePrefabRelative("FrontMountain", new Vector2(xPos, 1.5f), frontMountainLayer);
            xPos += mountainWidth;
        }

        size /= 2;
        
        xPos = mountainWidth * -size / 2 + mountainWidth / 2;
        for(int i = 0; i < size; i++) {
            PrefabUtils.InstantiatePrefabRelative("MidMountain", new Vector2(xPos, 1.5f), midMountainLayer);
            xPos += mountainWidth;
        }

        xPos = mountainWidth * -size / 2 + mountainWidth / 2;
        for(int i = 0; i < size; i++) {
            PrefabUtils.InstantiatePrefabRelative("BackMountain", new Vector2(xPos, 1.3f), backMountainLayer);
            xPos += mountainWidth;
        }
    }

}
