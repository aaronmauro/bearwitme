using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomAudioPlayer : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 1f;
    }

    public Sound[] sounds;

    [Header("Pitch Settings")]
    public float minPitch = 0.95f;
    public float maxPitch = 1.05f;

    private AudioSource audioSource;
    private int lastPlayedIndex = -1;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomClip()
    {
        if (sounds == null || sounds.Length == 0)
        {
            Debug.LogWarning("No sounds assigned!");
            return;
        }

        int index;

        // Prevent repeating the same sound twice in a row
        if (sounds.Length == 1)
        {
            index = 0;
        }
        else
        {
            do
            {
                index = Random.Range(0, sounds.Length);
            } while (index == lastPlayedIndex);
        }

        lastPlayedIndex = index;

        Sound selected = sounds[index];

        // Apply random pitch variation
        audioSource.pitch = Random.Range(minPitch, maxPitch);

        // Play the sound
        audioSource.PlayOneShot(selected.clip, selected.volume);
    }

    // Optional: play on start
    void Start()
    {
        PlayRandomClip();
    }
}