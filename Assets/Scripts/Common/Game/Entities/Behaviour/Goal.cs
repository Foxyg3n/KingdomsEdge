using UnityEngine;

public abstract class Goal<T> : IGoal where T : Entity {

    protected readonly T entity;
    protected readonly Transform transform;

    protected Goal(T entity) {
        this.entity = entity;
        transform = entity.transform;
    }

    public abstract bool MeetsRequirements();
    public abstract void Execute();
    public abstract void Reset();

}
