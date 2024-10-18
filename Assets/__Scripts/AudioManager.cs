using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioClip blasterSound;
    public AudioClip shipExplosionSound;
    public AudioClip powerUpSound;
    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>(); // Make sure this is not null
            if (audioSource == null) {
                Debug.LogError("Failed to find AudioSource component on AudioManager.");
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip == null) {
            Debug.LogError("Attempted to play a null AudioClip.");
            return;
        }
        if (audioSource == null) {
            Debug.LogError("AudioSource component is not initialized.");
            return;
        }
        audioSource.PlayOneShot(clip);
    }
}
