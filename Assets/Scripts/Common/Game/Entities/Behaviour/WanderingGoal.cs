using System;
using UnityEngine;

public class WanderingGoal : Goal<NPC> {

    private readonly float walkingInterval = 2f;
    private readonly float walkingRadius = 2f;
    private bool shouldWalk;

    private ZoneBounds villageBounds;
    private Vector3 destination;
    private float cooldown;

    public WanderingGoal(NPC entity) : base(entity) {}

    public override bool MeetsRequirements() {
        return true;
    }

    public override void Execute() {
        if(shouldWalk) {
            entity.transform.position = Vector3.MoveTowards(entity.transform.position, destination, Time.deltaTime * entity.speed);
            entity.transform.localScale = new Vector3(Mathf.Sign(destination.x - entity.transform.position.x), 1, 1);
            if(Math.Abs(transform.position.x - destination.x) < 0.01f) {
                shouldWalk = false;
                entity.animator.Play("Idle");
            }
        } else {
            if(cooldown <= 0) {
                shouldWalk = true;
                villageBounds = entity.village.villageZone.bounds;
                float offset = UnityEngine.Random.Range(-1 * Math.Min(walkingRadius, Math.Abs(villageBounds.leftBound.position.x - entity.transform.position.x)), Math.Min(walkingRadius, Math.Abs(villageBounds.rightBound.position.x - entity.transform.position.x)));
                float nextPoint = entity.transform.position.x + offset;
                destination = new Vector3(nextPoint, entity.transform.position.y, entity.transform.position.z);
                cooldown = walkingInterval;
                entity.animator.Play("Walk");
            } else {
                cooldown -= Time.deltaTime;
            }
        }
    }

    public override void Reset() {
        shouldWalk = false;
        cooldown = 0;
    }

}