using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Island : MonoBehaviour {

    public Scene activeScene { get; private set; }

    public Transform groundLayer;
    public Transform frontBackgroundLayer;
    public Transform midBackgroundLayer;
    public Transform backBackgroundLayer;
    public Transform buildingsLayer;
    public Transform entitiesLayer;
    public Transform resourcesLayer;
    public Transform dropLayer;
    public Transform markersLayer;

    public ResourcesGUI gui;

    public int islandId { get; private set; }
    public int size { get; private set; }
    public int chunkCount => (int) (IslandManager.GROUND_SIZE / IslandManager.CHUNK_SIZE) * size;
    public Chunk leftBound => new(-chunkCount / 2);

    private readonly List<Resource> resources = new();
    private readonly List<Drop> drops = new();
    private readonly List<Building> buildings = new();

    public Zones zones;
    public Village village;

    public void InitializeIsland(Scene scene, IslandOptions options, int id) {
        activeScene = scene;
        size = options.islandSize;
        islandId = id;
        transform.position = new Vector3(0, (islandId - 1) * 100, 0);

        LandscapeGenerator.GenerateGround(groundLayer, size);
        LandscapeGenerator.GenerateLandscape(frontBackgroundLayer, midBackgroundLayer, backBackgroundLayer, size);
        zones = ZoneGenerator.GenerateZones(chunkCount);
        zones.AddGenerator(new StoneGenerator());
        //zones.AddGenerator(new CottonGenerator());
        zones.Initialize(this);
        // this.backgroundLayer = backgroundLayer;

        village = new Village(zones.villageZone);
        village.Initialize(this);
    }

    public void SpawnMarker(string markerName, Chunk chunk) {
        PrefabUtils.InstantiatePrefabRelative("Markers/" + markerName, chunk.position, markersLayer);
    }

    public void RemoveMarkers() {
        foreach(Transform child in markersLayer) {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void SpawnEntity(string entityName, Vector2 position) {
        GameObject gameObj = PrefabUtils.InstantiatePrefabRelative("Entities/NPCs/" + entityName, position, entitiesLayer);
        Entity entity = gameObj.GetComponent<Entity>();
        entity.InitIsland(this);
    }

    public T SpawnBuilding<T>(Chunk chunk) where T : Building {
        T building = PrefabUtils.InstantiatePrefabRelative("Buildings/" + typeof(T).Name, chunk.position - new Vector2(0, 0.01f), buildingsLayer).GetComponent<T>();
        building.transform.position += new Vector3((building.width - 1) * IslandManager.CHUNK_SIZE / 2, 0, 0);
        building.InitIsland(this);
        building.Initialize();
        buildings.Add(building);
        return building;
    }

    public T GetBuilding<T>() where T : Building {
        return (T) buildings.FirstOrDefault(building => building.GetType() == typeof(T));
    }
    
    public Resource SpawnResource(string resourceName, Chunk chunk) {
        float offset = IslandManager.CHUNK_SIZE / 2 * Random.Range(-0.8f, 0.8f);
        Resource resource = PrefabUtils.InstantiatePrefabRelative("Resources/" + resourceName, chunk.position + new Vector2(offset, -0.01f), resourcesLayer).GetComponent<Resource>();
        resource.island = this;
        return resource;
    }

    public Drop SpawnDrop<T>(Vector2 position) {
        return SpawnDrop(typeof(T), position);
    }

    public Drop SpawnDrop(Type dropType, Vector2 position) {
        Drop drop = PrefabUtils.InstantiatePrefabRelative("Resources/Drops/" + dropType.Name, position, dropLayer).GetComponent<Drop>();
        drop.island = this;
        return drop;
    }

    public void RegisterResource(Resource resource) {
        if(!resources.Contains(resource)) resources.Add(resource);
    }

    public void UnregisterResource(Resource resource) {
        resources.Remove(resource);
    }

    public void RegisterDrop(Drop drop) {
        if(!drops.Contains(drop)) drops.Add(drop);
    }

    public void UnregisterDrop(Drop drop) {
        drops.Remove(drop);
    }

    // FIXME: Is it needed?
    // public bool HasFreeResource() {
    //     return resources.Count != 0;
    // }

    public bool HasFreeResource<T>() where T : Resource {
        return resources.Find(resource => resource.GetType() == typeof(T)) != null;
    }

    public bool HasFreeResourceByJob(Worker npc) {
        return resources.Find(resource => resource.GetType() == npc.GetJobResourceType()) != null;
    }

    public Resource GetNearestResource(Entity entity) {
        List<Resource> resourceList = resources.FindAll(resource => !resource.isOccupied);
        resourceList.Sort((a, b) => Mathf.Abs(a.transform.position.x - entity.transform.position.x).CompareTo(Mathf.Abs(b.transform.position.x - entity.transform.position.x)));
        return resourceList[0];
    }

    public Resource GetNearestResource<T>(Entity entity) where T : Resource {
        List<Resource> resourceList = resources.FindAll(resource => resource.GetType() == typeof(T) && !resource.isOccupied);
        resourceList.Sort((a, b) => Mathf.Abs(a.transform.position.x - entity.transform.position.x).CompareTo(Mathf.Abs(b.transform.position.x - entity.transform.position.x)));
        return resourceList.Count == 0 ? null : resourceList[0];
    }

    public Resource GetNearestResourceByJob(Worker npc) {
        List<Resource> resourceList = resources.FindAll(resource => resource.GetType() == npc.GetJobResourceType() && !resource.isOccupied);
        resourceList.Sort((a, b) => Mathf.Abs(a.transform.position.x - npc.transform.position.x).CompareTo(Mathf.Abs(b.transform.position.x - npc.transform.position.x)));
        return resourceList.Count == 0 ? null : resourceList[0];
    }

    public Drop GetNearestDrop<T>(Entity entity) where T : Drop {
        List<Drop> dropList = drops.FindAll(drop => drop.GetType() == typeof(T));
        dropList.Sort((a, b) => Mathf.Abs(a.transform.position.x - entity.transform.position.x).CompareTo(Mathf.Abs(b.transform.position.x - entity.transform.position.x)));
        return dropList.Count == 0 ? null : dropList[0];
    }

    public Drop GetNearestDropByJob(Worker npc) {
        List<Drop> dropList = drops.FindAll(drop => drop.GetType() == npc.GetJobDropType());
        dropList.Sort((a, b) => Mathf.Abs(a.transform.position.x - npc.transform.position.x).CompareTo(Mathf.Abs(b.transform.position.x - npc.transform.position.x)));
        return dropList.Count == 0 ? null : dropList[0];
    }
}
