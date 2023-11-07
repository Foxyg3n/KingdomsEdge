using System;
using UnityEngine;
using Random = System.Random;

public class Woodcutter : Worker {

    private void PlayLeonSound() {
        AudioSource audioSource = GetComponent<AudioSource>();
        Random random = new();
        float randomPitch = random.Next(8, 13) / 10f;
        audioSource.pitch = randomPitch;
        audioSource.Play();
    }

    public override Type GetJobResourceType() {
        return typeof(Tree);
    }

    public override Type GetJobDropType() {
        return typeof(Wood);
    }
    
}
