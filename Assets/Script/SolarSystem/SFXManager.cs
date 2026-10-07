using UnityEngine;

public class SFXManager : MonoBehaviour
{
    // Pintu masuk satu-satunya
    public static SFXManager Instance;

    private AudioSource audioSource;

    void Awake()
    {
        // Kalau sudah ada SFXManager lain, hancurkan yang baru
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Tetap hidup walau ganti scene

        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }
}