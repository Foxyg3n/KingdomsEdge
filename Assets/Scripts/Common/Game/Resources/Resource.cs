using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public abstract class Resource : MonoBehaviour {

    public Island island;
    private NPC occupier;
    public List<Drop> drops = new();
    public bool isOccupied => occupier != null;
    public bool gathered { get; private set; }
    public Animator animator { get; private set; }

    public abstract Type GetDropType();
    
    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void Start() {
        island.RegisterResource(this);
    }

    public Vector2 GetPosition() {
        return new Vector2(transform.position.x, 0);
    }

    public void SetOccupier(NPC npc) {
        occupier = npc;
    }

    public void ClearOccupier() {
        occupier = null;
    }

    public void Gather() {
        GetComponent<Animator>().Play("Gather");
        gathered = true;
        island.UnregisterResource(this);
    }

    // public void DropResource() {
    //     
    // }

    public void DropResources() {
        int resourceCount = Random.Range(2, 3 + 1); // +1 for inclusive
        Vector3 position = transform.position;
        for(int i = 0; i < resourceCount; i++) {
            Drop drop = island.SpawnDrop(GetDropType(), position + new Vector3(Random.Range(-0.1f, 0.1f), 0.2f));
            drop.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0, 360));
            drops.Add(drop);
        }
    }

    public void Destroy() {
        Object.Destroy(gameObject);
    }

}
