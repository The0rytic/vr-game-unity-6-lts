using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Pengaturan Scene")]
    [SerializeField] private string namaSceneGame = "Scene_1";
    [SerializeField] private GameObject SettingsCanvas;
    [SerializeField] private GameObject CreditsCanvas;


        void Start()
    {
       
    }



    // --- FUNGSI UTAMA (sementara jalan dulu) ---
    public void PlayGame()
    {
        Debug.Log("Berhasil cuy");
        // Langsung pindah ke scene game seketika
        SceneManager.LoadScene(namaSceneGame);
    }

    public void OpenSettings()
    {   
        SettingsCanvas.SetActive(true);
    }

    public void CloseSettings()
    {
        SettingsCanvas.SetActive(false); 
    }
    public void OpenCredits()
    {
        CreditsCanvas.SetActive(true);
    }

    public void CloseCredits()
    {
        CreditsCanvas.SetActive(false);
    }

    public void Quit() => Application.Quit();

}