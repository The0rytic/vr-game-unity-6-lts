using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource voiceSource;

    public SFXClips sfx;

    [Header("Audio Mixer (opsional)")]
    public AudioMixer audioMixer;

    // key untuk PlayerPrefs
    private const string MUSIC_VOL_KEY = "MusicVolume";
    private const string SFX_VOL_KEY   = "SFXVolume";

    void Awake()
    {
        // Singleton + DontDestroyOnLoad
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // load volume tersimpan
        LoadVolume();
    }

    // ── Play musik ────────────────────────────────────────────
    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (musicSource.clip == clip) return; // sudah playing
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    // ── Play SFX ──────────────────────────────────────────────
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    // ── Set volume ────────────────────────────────────────────
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat(MUSIC_VOL_KEY, volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat(SFX_VOL_KEY, volume);
        PlayerPrefs.Save();
    }

    // ── Get volume (untuk isi slider di UI) ───────────────────
    public float GetMusicVolume() => musicSource.volume;
    public float GetSFXVolume()   => sfxSource.volume;

    // ── Load volume dari PlayerPrefs ──────────────────────────
    void LoadVolume()
    {
        musicSource.volume = PlayerPrefs.GetFloat(MUSIC_VOL_KEY, 1f);
        sfxSource.volume   = PlayerPrefs.GetFloat(SFX_VOL_KEY,   1f);
    }

    public void PlayDialogueVoice(AudioClip clip, float pitch)
    {
        if (clip == null) return;

        // Samakan volume dengan settingan SFX agar slider tetap berfungsi
        voiceSource.volume = sfxSource.volume; 
        
        voiceSource.pitch = pitch;
        voiceSource.clip = clip;
        
        // Gunakan Play() biasa alih-alih PlayOneShot(). 
        // Ini akan memotong suara huruf sebelumnya agar audionya tidak 
        // bertumpuk dan meledak menjadi sangat keras.
        voiceSource.Play(); 
    }

    public void StopDialogueVoice()
    {
        if (voiceSource != null && voiceSource.isPlaying)
        {
            voiceSource.Stop();
        }
    }
    public void PlayButtonHover() => PlaySFX(sfx.buttonHover);
public void PlayButtonClick() => PlaySFX(sfx.buttonClick);

    
}