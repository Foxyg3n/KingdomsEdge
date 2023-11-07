using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
        
    public static AudioManager instance;
    
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] public AudioSource backgroundMusic;
    [SerializeField] public AudioClip[] backgroundMusicClips;
    
    public void PlayBackgroundMusic(int index) {
        backgroundMusic.clip = backgroundMusicClips[index];
        backgroundMusic.loop = true;
        backgroundMusic.Play();
    }

    public void ChangeMasterVolume(float volume) {
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void ChangeMusicVolume(float volume) {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void ChangeSFXVolume(float volume) {
        audioMixer.SetFloat("SFXVolume", volume);
    }

}
