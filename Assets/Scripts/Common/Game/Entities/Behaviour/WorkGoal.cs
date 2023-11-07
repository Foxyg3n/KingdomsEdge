using System;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class WorkGoal : Goal<Worker> {

    private bool goingToResource;
    private bool working;
    private Vector3 destination;
    private Resource targetResource;

    private readonly FixedStopwatch workingStopwatch = new(12);

    public WorkGoal(Worker entity) : base(entity) {}

    public override bool MeetsRequirements() {
        return entity.island.HasFreeResourceByJob(entity) && !entity.island.GetBuilding<Storage>().IsFull(entity.GetJobDropType());
    }

    // TODO: Pass durability to resource when suddenly doing something else
    public override void Execute() {
        if(entity.inventory.isFull) {
            Storage storage = entity.island.GetBuilding<Storage>();
            if(entity.isWalking) return;
            entity.GoTo(storage, () => storage.PassFrom(entity.GetJobDropType(), entity.inventory));
        } else {
            if(goingToResource) {
                if(entity.isWalking) return;
                entity.GoTo(transform.position.x < destination.x ? destination.x - 0.1f : destination.x + 0.1f, () => {
                    goingToResource = false;
                    working = true;
                    workingStopwatch.Split();
                    entity.animator.Play("Work");
                });
            } else {
                if(working) {
                    if(workingStopwatch.IsFinished) {
                        if(entity.isWalking) return;
                        if(!targetResource.gathered) {
                            targetResource.Gather();
                            entity.animator.Play("Idle");
                        }
                        if(targetResource.drops.Count == 0) return;
                        Drop farthestDrop = null;
                        float maxDistance = 0;
                        foreach(Drop drop in targetResource.drops) {
                            if(drop.collected) continue;
                            float distance = Mathf.Abs(drop.transform.position.x - entity.transform.position.x);
                            if(distance > maxDistance) {
                                maxDistance = distance;
                                farthestDrop = drop;
                            }
                        }

                        if(farthestDrop == null) {
                            working = false;
                            targetResource = null;
                            return;
                        }
                        
                        entity.GoTo(farthestDrop.transform.position.x, () => {
                            working = false;
                            targetResource = null;
                        });
                    }
                } else {
                    goingToResource = true;
                    if(targetResource == null) targetResource = entity.island.GetNearestResourceByJob(entity);
                    targetResource.SetOccupier(entity);
                    destination = targetResource.GetPosition();
                    entity.animator.Play("Walk");
                }
            }
        }
    }

    public override void Reset() {
        entity.InterruptGoTo();
        working = false;
        goingToResource = false;
        workingStopwatch.Split();
    }

}
