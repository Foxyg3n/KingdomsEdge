using UnityEngine;
using UnityEngine.SceneManagement;

public static class PrefabUtils {

    public static GameObject InstantiatePrefab(string prefabName, Transform parent) {
        GameObject prefab = Resources.Load<GameObject>(prefabName);
        return Object.Instantiate(prefab, parent);
    }

    public static GameObject InstantiatePrefabAbsolute(string prefabName, Vector2 position, Transform parent) {
        return InstantiatePrefabAbsolute(prefabName, position, Quaternion.identity, parent);
    }

    public static GameObject InstantiatePrefabAbsolute(string prefabName, Vector2 position, Quaternion rotation, Transform parent) {
        GameObject prefab = Resources.Load<GameObject>(prefabName);
        return Object.Instantiate(prefab, (Vector3) position, rotation, parent);
    }

    public static GameObject InstantiatePrefabRelative(string prefabName, Vector2 position, Transform parent) {
        return InstantiatePrefabRelative(prefabName, position, Quaternion.identity, parent);
    }

    public static GameObject InstantiatePrefabRelative(string prefabName, Vector2 position, Quaternion rotation, Transform parent) {
        GameObject prefab = Resources.Load<GameObject>(prefabName);
        return Object.Instantiate(prefab, (Vector3) position + parent.position, rotation, parent);
    }

    public static GameObject InstantiatePrefab(string prefabName, Scene scene) {
        GameObject prefab = Resources.Load<GameObject>(prefabName);
        GameObject gameObject = Object.Instantiate(prefab);
        SceneManager.MoveGameObjectToScene(gameObject, scene);
        return gameObject;
    }

    public static GameObject InstantiatePrefab(string prefabName, Vector2 position, Scene scene) {
        GameObject prefab = Resources.Load<GameObject>(prefabName);
        GameObject gameObject = Object.Instantiate(prefab, (Vector3) position, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(gameObject, scene);
        return gameObject;
    }

}
