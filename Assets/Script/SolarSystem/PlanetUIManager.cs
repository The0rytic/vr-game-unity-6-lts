using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PlanetUIManager : MonoBehaviour
{
    public static PlanetUIManager Instance;

    [SerializeField] GameObject infoPanel;
    [SerializeField] TextMeshProUGUI planetNameText;
    [SerializeField] TextMeshProUGUI planetFactText;
    [SerializeField] GameObject bumiButton;
    [SerializeField] GameObject closeButton;

    [Header("Sembunyikan saat Materi Bumi")]
    [SerializeField] GameObject container;

    [Header("Teleport Points")]
    [SerializeField] Transform merkuriusTeleport;
    [SerializeField] Transform venusTeleport;
    [SerializeField] Transform bumiTeleport;
    [SerializeField] Transform marsTeleport;
    [SerializeField] Transform jupiterTeleport;
    [SerializeField] Transform saturnusTeleport;
    [SerializeField] Transform uranusTeleport;
    [SerializeField] Transform neptunusTeleport;

    Dictionary<string, string> planetFacts = new Dictionary<string, string>()
    {
        { "Merkurius", "Planet terkecil dan terdekat dengan Matahari. Satu hari di Merkurius lebih panjang dari satu tahunnya!" },
        { "Venus", "Planet terpanas di tata surya meski bukan yang terdekat. Suhunya bisa mencapai 465°C!" },
        { "Bumi", "Satu-satunya planet yang diketahui memiliki kehidupan. Ditutupi 71% air." },
        { "Mars", "Disebut Planet Merah karena tanahnya mengandung besi berkarat. Memiliki gunung tertinggi di tata surya." },
        { "Jupiter", "Planet terbesar di tata surya. Badai Besar Merahnya sudah berlangsung lebih dari 350 tahun!" },
        { "Saturnus", "Satu-satunya planet dengan cincin es yang terlihat jelas. Lebih ringan dari air!" },
        { "Uranus", "Berputar miring 98 derajat, hampir tidur miring. Musimnya berlangsung selama 21 tahun!" },
        { "Neptunus", "Planet terjauh dari Matahari. Anginnya tercepat di tata surya, mencapai 2.100 km/jam!" }
    };

    Dictionary<string, Transform> teleportPoints;

    SolarSystem solarSystem;

    void Awake()
    {
        Instance = this;
        infoPanel.SetActive(false);
        solarSystem = FindFirstObjectByType<SolarSystem>(FindObjectsInactive.Include);
        
        Debug.Log("SolarSystem found: " + (solarSystem != null ? "YES" : "NULL")); // tambah ini

        teleportPoints = new Dictionary<string, Transform>()
        {
            { "Merkurius", merkuriusTeleport },
            { "Venus", venusTeleport },
            { "Bumi", bumiTeleport },
            { "Mars", marsTeleport },
            { "Jupiter", jupiterTeleport },
            { "Saturnus", saturnusTeleport },
            { "Uranus", uranusTeleport },
            { "Neptunus", neptunusTeleport }
        };
    }

    public void SelectPlanet(string name)
    {
        bool isBumi = name == "Bumi";
        string fact = planetFacts.ContainsKey(name) ? planetFacts[name] : "Fakta tidak tersedia.";

        infoPanel.SetActive(true);
        planetNameText.text = name;
        planetFactText.text = fact;
        bumiButton.SetActive(isBumi);
        solarSystem.StopOrbits();

        if (teleportPoints.ContainsKey(name) && teleportPoints[name] != null)
        {
            Debug.Log("Teleporting to: " + name);
            CameraFlyTo.Instance.FlyToPlanet(teleportPoints[name]);
        }
        else
        {
            Debug.LogError("Teleport point tidak ditemukan: " + name);
        }
    }

    public void HidePanel()
    {
        infoPanel.SetActive(false);
        solarSystem.ResumeOrbits();
    }

    public void OnBumiButtonClick()
    {
        // sembunyiin InfoPanel dan Container
        infoPanel.SetActive(false);
        if (container != null) container.SetActive(false);

        if (BumiMateriManager.Instance != null)
            BumiMateriManager.Instance.TampilkanMateri();
        else
            Debug.LogError("BumiMateriManager tidak ditemukan di scene!");
    }

    // dipanggil dari BumiMateriManager saat panel ditutup
    public void OnBumiMateriTutup()
    {
        if (container != null) container.SetActive(true);
        solarSystem.ResumeOrbits();
    }
}