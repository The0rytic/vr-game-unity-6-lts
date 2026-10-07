using UnityEngine;

public class SceneMusicPlayer : MonoBehaviour
{
    [SerializeField] public AudioClip sceneMusic;

    void Start()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayMusic(sceneMusic);
    }
}