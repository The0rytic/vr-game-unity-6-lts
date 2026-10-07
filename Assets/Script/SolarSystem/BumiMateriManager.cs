using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Mengelola panel materi dan kuis Bumi secara hardcoded.
/// Cara pakai:
///   1. Buat GameObject baru di scene, attach script ini.
///   2. Di Inspector, assign semua field yang ada di bawah.
///   3. Di PlanetUIManager.OnBumiButtonClick(), panggil:
///      BumiMateriManager.Instance.TampilkanMateri();
/// </summary>
public class BumiMateriManager : MonoBehaviour
{
    public static BumiMateriManager Instance;

    // ─── ROOT PANEL ───────────────────────────────────────────────
    [Header("Panel Utama")]
    [SerializeField] GameObject panelUtama;      // parent Canvas / Panel

    // ─── HALAMAN MATERI ───────────────────────────────────────────
    [Header("Halaman Materi")]
    [SerializeField] GameObject halamanMateri;
    [SerializeField] TextMeshProUGUI judulMateriText;
    [SerializeField] TextMeshProUGUI isiMateriText;
    [SerializeField] Button tombolMateriBerikutnya;
    [SerializeField] Button tombolMulaiKuis;

    // ─── HALAMAN KUIS ─────────────────────────────────────────────
    [Header("Halaman Kuis")]
    [SerializeField] GameObject halamanKuis;
    [SerializeField] TextMeshProUGUI nomorSoalText;
    [SerializeField] TextMeshProUGUI pertanyaanText;
    [SerializeField] Button[] tombolJawaban;          // array 2 tombol: A dan B
    [SerializeField] TextMeshProUGUI feedbackText;
    [SerializeField] Button tombolLanjutSoal;

    // ─── HALAMAN SKOR ─────────────────────────────────────────────
    [Header("Halaman Skor")]
    [SerializeField] GameObject halamanSkor;
    [SerializeField] TextMeshProUGUI skorText;
    [SerializeField] Button tombolUlangiKuis;
    [SerializeField] Button tombolTutup;

    // ══════════════════════════════════════════════════════════════
    //  DATA MATERI (hardcoded)
    // ══════════════════════════════════════════════════════════════

    struct HalamanMateriData
    {
        public string judul;
        public string isi;
    }

    readonly HalamanMateriData[] materiList = new HalamanMateriData[]
    {
        new HalamanMateriData
        {
            judul = "Rotasi Bumi",
            isi  = "Rotasi Bumi adalah perputaran Bumi pada porosnya sendiri.\n\n" +
                   "• Arah rotasi: dari Barat ke Timur\n" +
                   "• Waktu 1 rotasi: ±24 jam (1 hari)\n\n" +
                   "Dampak rotasi:\n" +
                   "Terjadinya siang dan malam\n" +
                   "Perbedaan waktu (WIB, WITA, WIT)\n" +
                   "Gerak semu Matahari dari timur ke barat"
        },
        new HalamanMateriData
        {
            judul = "Revolusi Bumi",
            isi  = "Revolusi Bumi adalah pergerakan Bumi mengelilingi Matahari.\n\n" +
                   "• Lintasan: orbit elips\n" +
                   "• Waktu 1 revolusi: ±365,25 hari (1 tahun)\n\n" +
                   "Dampak revolusi:\n" +
                   "Pergantian musim (4 musim di belahan bumi lain)\n" +
                   "Dasar perhitungan Kalender Masehi\n" +
                   "Indonesia: 2 musim (hujan & kemarau)"
        },
        new HalamanMateriData
        {
            judul = "Kalender & Zona Waktu",
            isi  = "Kalender Masehi didasarkan pada revolusi Bumi (365 hari).\n" +
                   "Kalender Hijriah didasarkan pada revolusi Bulan (354 hari).\n\n" +
                   "Zona waktu Indonesia:\n" +
                   "• WIB  (GMT+7) — Jawa, Sumatera, Kalimantan Barat & Tengah\n" +
                   "• WITA (GMT+8) — Bali, NTB, NTT, Kalimantan Timur & Selatan\n" +
                   "• WIT  (GMT+9) — Maluku, Papua\n\n" +
                   "Perbedaan waktu ini terjadi karena Bumi berotasi 15° per jam."
        }
    };

    // ══════════════════════════════════════════════════════════════
    //  DATA SOAL (hardcoded)
    // ══════════════════════════════════════════════════════════════

    struct Soal
    {
        public string pertanyaan;
        public string[] pilihan;   // selalu 2 pilihan
        public int indexBenar;     // 0 = A, 1 = B
        public string penjelasan;
    }

    readonly Soal[] soalList = new Soal[]
    {
        new Soal
        {
            pertanyaan  = "Apa nama gerakan Bumi yang berputar pada porosnya?",
            pilihan     = new[] { "A. Rotasi", "B. Revolusi" },
            indexBenar  = 0,
            penjelasan  = "Benar! Rotasi adalah perputaran Bumi pada porosnya sendiri."
        },
        new Soal
        {
            pertanyaan  = "Siang hari terjadi karena…",
            pilihan     = new[] { "A. Matahari mendekat ke Bumi", "B. Bagian Bumi menghadap Matahari" },
            indexBenar  = 1,
            penjelasan  = "Tepat! Siang terjadi di bagian Bumi yang sedang menghadap Matahari."
        },
        new Soal
        {
            pertanyaan  = "Berapa lama Bumi menyelesaikan satu rotasi penuh?",
            pilihan     = new[] { "A. 24 jam", "B. 365 hari" },
            indexBenar  = 0,
            penjelasan  = "Benar! Satu rotasi Bumi membutuhkan sekitar 24 jam (1 hari)."
        },
        new Soal
        {
            pertanyaan  = "Gerakan Bumi mengelilingi Matahari disebut…",
            pilihan     = new[] { "A. Rotasi", "B. Revolusi" },
            indexBenar  = 1,
            penjelasan  = "Betul! Revolusi adalah pergerakan Bumi mengelilingi Matahari."
        },
        new Soal
        {
            pertanyaan  = "Berapa lama Bumi menyelesaikan satu revolusi penuh?",
            pilihan     = new[] { "A. 24 jam", "B. 365 hari" },
            indexBenar  = 1,
            penjelasan  = "Benar! Satu revolusi Bumi membutuhkan sekitar 365 hari (1 tahun)."
        },
        new Soal
        {
            pertanyaan  = "Apa dampak utama dari revolusi Bumi?",
            pilihan     = new[] { "A. Terjadinya siang dan malam", "B. Pergantian musim" },
            indexBenar  = 1,
            penjelasan  = "Tepat! Revolusi Bumi menyebabkan pergantian musim di berbagai belahan dunia."
        },
        new Soal
        {
            pertanyaan  = "Kalender Masehi didasarkan pada pergerakan apa?",
            pilihan     = new[] { "A. Bumi mengelilingi Matahari", "B. Bulan mengelilingi Bumi" },
            indexBenar  = 0,
            penjelasan  = "Benar! Kalender Masehi didasarkan pada revolusi Bumi (365 hari)."
        },
        new Soal
        {
            pertanyaan  = "Zona waktu WIB menggunakan GMT+…",
            pilihan     = new[] { "A. GMT+7", "B. GMT+8" },
            indexBenar  = 0,
            penjelasan  = "Benar! WIB (Waktu Indonesia Barat) menggunakan GMT+7."
        },
        new Soal
        {
            pertanyaan  = "Perubahan musim merupakan dampak dari…",
            pilihan     = new[] { "A. Rotasi Bumi", "B. Revolusi Bumi" },
            indexBenar  = 1,
            penjelasan  = "Tepat! Perubahan musim terjadi karena revolusi Bumi dan kemiringan porosnya."
        },
        new Soal
        {
            pertanyaan  = "Rotasi Bumi menyebabkan…",
            pilihan     = new[] { "A. Terjadinya siang dan malam", "B. Pergantian musim" },
            indexBenar  = 0,
            penjelasan  = "Benar! Rotasi Bumi menyebabkan terjadinya siang dan malam secara bergantian."
        }
    };

    // ══════════════════════════════════════════════════════════════
    //  STATE INTERNAL
    // ══════════════════════════════════════════════════════════════

    int indexMateri  = 0;
    int indexSoal    = 0;
    int skorBenar    = 0;
    bool sudahJawab  = false;

    // ══════════════════════════════════════════════════════════════
    //  UNITY LIFECYCLE
    // ══════════════════════════════════════════════════════════════

    void Awake()
    {
        Instance = this;
        panelUtama.SetActive(false);
    }

    // ══════════════════════════════════════════════════════════════
    //  PUBLIC — dipanggil dari PlanetUIManager
    // ══════════════════════════════════════════════════════════════

    /// <summary>Panggil ini dari OnBumiButtonClick() di PlanetUIManager.</summary>
    public void TampilkanMateri()
    {
        indexMateri = 0;
        panelUtama.SetActive(true);
        TampilHalamanMateri(indexMateri);
    }

    // ══════════════════════════════════════════════════════════════
    //  MATERI
    // ══════════════════════════════════════════════════════════════

    void TampilHalamanMateri(int index)
    {
        halamanMateri.SetActive(true);
        halamanKuis.SetActive(false);
        halamanSkor.SetActive(false);

        var data = materiList[index];
        judulMateriText.text = data.judul;
        isiMateriText.text   = data.isi;

        bool adaHalamanBerikutnya = index < materiList.Length - 1;
        tombolMateriBerikutnya.gameObject.SetActive(adaHalamanBerikutnya);
        tombolMulaiKuis.gameObject.SetActive(!adaHalamanBerikutnya);

        // pasang listener tombol (bersihkan dulu supaya tidak dobel)
        tombolMateriBerikutnya.onClick.RemoveAllListeners();
        tombolMateriBerikutnya.onClick.AddListener(MateriBerikutnya);

        tombolMulaiKuis.onClick.RemoveAllListeners();
        tombolMulaiKuis.onClick.AddListener(MulaiKuis);
    }

    void MateriBerikutnya()
    {
        indexMateri++;
        if (indexMateri < materiList.Length)
            TampilHalamanMateri(indexMateri);
    }

    // ══════════════════════════════════════════════════════════════
    //  KUIS
    // ══════════════════════════════════════════════════════════════

    void MulaiKuis()
    {
        indexSoal = 0;
        skorBenar = 0;
        TampilSoal(indexSoal);
    }

    void TampilSoal(int index)
    {
        halamanMateri.SetActive(false);
        halamanKuis.SetActive(true);
        halamanSkor.SetActive(false);

        sudahJawab = false;
        feedbackText.text = "";
        tombolLanjutSoal.gameObject.SetActive(false);

        var soal = soalList[index];
        nomorSoalText.text  = $"Soal {index + 1} / {soalList.Length}";
        pertanyaanText.text = soal.pertanyaan;

        // pasang teks & listener tiap tombol jawaban
        for (int i = 0; i < tombolJawaban.Length; i++)
        {
            int captured = i; // capture buat lambda
            tombolJawaban[i].GetComponentInChildren<TextMeshProUGUI>().text = soal.pilihan[i];
            tombolJawaban[i].onClick.RemoveAllListeners();
            tombolJawaban[i].onClick.AddListener(() => PilihJawaban(captured));
            tombolJawaban[i].interactable = true;
        }

        tombolLanjutSoal.onClick.RemoveAllListeners();
        tombolLanjutSoal.onClick.AddListener(SoalBerikutnya);
    }

    void PilihJawaban(int indexPilihan)
    {
        if (sudahJawab) return;
        sudahJawab = true;

        var soal = soalList[indexSoal];
        bool benar = indexPilihan == soal.indexBenar;

        if (benar)
        {
            skorBenar++;
            feedbackText.color = Color.green;
            feedbackText.text  = soal.penjelasan;
        }
        else
        {
            feedbackText.color = Color.red;
            feedbackText.text  = soal.penjelasan;
        }

        // nonaktifkan semua tombol setelah menjawab
        foreach (var btn in tombolJawaban)
            btn.interactable = false;

        tombolLanjutSoal.gameObject.SetActive(true);
    }

    void SoalBerikutnya()
    {
        indexSoal++;
        if (indexSoal < soalList.Length)
            TampilSoal(indexSoal);
        else
            TampilSkor();
    }

    // ══════════════════════════════════════════════════════════════
    //  SKOR AKHIR
    // ══════════════════════════════════════════════════════════════

    void TampilSkor()
    {
        halamanMateri.SetActive(false);
        halamanKuis.SetActive(false);
        halamanSkor.SetActive(true);

        string predikat;
        if      (skorBenar == soalList.Length) predikat = "Sempurna!";
        else if (skorBenar >= soalList.Length * 0.8f) predikat = "Hebat!";
        else if (skorBenar >= soalList.Length * 0.6f) predikat = "Cukup Baik!";
        else predikat = "Ayo Belajar Lagi!";

        skorText.text = $"{predikat}\n\nKamu menjawab benar\n{skorBenar} dari {soalList.Length} soal";

        tombolUlangiKuis.onClick.RemoveAllListeners();
        tombolUlangiKuis.onClick.AddListener(MulaiKuis);

        tombolTutup.onClick.RemoveAllListeners();
        tombolTutup.onClick.AddListener(TutupPanel);
    }

    void TutupPanel()
    {
        panelUtama.SetActive(false);

        // munculin lagi Container saat materi ditutup
        if (PlanetUIManager.Instance != null)
            PlanetUIManager.Instance.OnBumiMateriTutup();
    }
}