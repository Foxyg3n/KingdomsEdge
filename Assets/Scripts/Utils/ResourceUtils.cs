using UnityEngine;

public static class ResourceUtils {

    public static Sprite GetSprite(string spriteName) {
        return Resources.Load<Sprite>(spriteName);
    }

    public static GameObject GetPrefab(string prefabName) {
        return Resources.Load<GameObject>(prefabName);
    }

    public static T GetResource<T>(string resourceName) where T : Object {
        return Resources.Load<T>(resourceName);
    }

}
