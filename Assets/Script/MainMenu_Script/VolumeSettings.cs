using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        // isi slider dari volume tersimpan
        musicSlider.value = AudioManager.Instance.GetMusicVolume();
        sfxSlider.value   = AudioManager.Instance.GetSFXVolume();

        // listen perubahan slider
        musicSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
    }
}