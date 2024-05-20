using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Sound Effects")]
    public AudioClip[] sfxClips; // Array of sound effect clips
    public float sfxVolume = 1.0f; // Volume for sound effects

    [Header("Background Music")]
    public AudioClip[] musicClips; // Array of background music clips
    public float musicVolume = 0.5f; // Volume for background music

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Create a new GameObject with an AudioSource component for playing audio
        GameObject audioPlayer = new GameObject("AudioPlayer");
        audioSource = audioPlayer.AddComponent<AudioSource>();
        DontDestroyOnLoad(audioPlayer);
    }

    // Play a sound effect by index
    public void PlaySFX(int index)
    {
        if (index >= 0 && index < sfxClips.Length)
        {
            audioSource.PlayOneShot(sfxClips[index], sfxVolume);
        }
        else
        {
            Debug.LogWarning("Invalid sound effect index: " + index);
        }
    }

    // Play background music by index
    public void PlayMusic(int index)
    {
        if (index >= 0 && index < musicClips.Length)
        {
            audioSource.clip = musicClips[0];
            audioSource.volume = musicVolume;
            audioSource.Play();
            
        }
        else
        {
            Debug.LogWarning("Invalid music index: " + index);
        }
    }

    // Stop background music
    public void StopMusic()
    {
        audioSource.Stop();
    }

    // Set sound effects volume
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
    }

    // Set background music volume
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        if (audioSource.isPlaying)
        {
            audioSource.volume = musicVolume;
        }
    }
}
