using System;
using UnityEngine;

public abstract class Drop : MonoBehaviour {
    
    public Island island;
    public bool collected { get; private set; }

    public void Start() {
        island.RegisterDrop(this);
    }

    public void Collect() {
        gameObject.SetActive(false);
        collected = true;
        island.UnregisterDrop(this);
    }

    public abstract Type GetCorrespondingJobType();

    public void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.TryGetComponent(out Worker worker)) {
            if(worker.GetType() == GetCorrespondingJobType()) worker.Collect(this);
        }
    }
    
}