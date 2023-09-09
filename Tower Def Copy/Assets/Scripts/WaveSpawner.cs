using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;

    public TextMeshProUGUI waveCountdownText;

    // Dalga aras�ndaki s�re, saniye cinsinden belirlenir ve varsay�lan olarak 4 saniye olarak ayarlan�r.
    public float timeBetWaves = 4f;

    // Geri say�m s�resi, dalga aralar� aras�ndaki s�reyi hesaplamak i�in kullan�l�r.
    private float countdown = 0f;

    // Dalga dizinini takip etmek i�in bir de�i�ken. �lk dalga i�in 0'dan ba�lar.
    private int waveIndex = 0;

    // Update fonksiyonu, her �er�eve yeniden hesapland���nda �a�r�l�r ve oyunun ana mant���n� i�erir.
    void Update()
    {
        // Geri say�m s�f�rsa, yeni bir dalga ba�latmak i�in Coroutine'i ba�lat�r�z.
        if (countdown <= 0)
        {
            StartCoroutine(SpawnWave()); // Yeni bir dalga ba�latmak i�in IEnumerator SpawnWave fonksiyonunu �a��r�r.
            countdown = timeBetWaves; // Geri say�m� dalga aras�ndaki s�reyle yeniden ba�lat�r�z.
        }

        countdown -= Time.deltaTime; // Her �er�eve, geri say�m s�resini zaman ge�i�ine (deltaTime) g�re azalt�r.

        // waveCountdownText metin nesnesine geri say�m s�resini yuvarlayarak yazd�r�r�z.
        waveCountdownText.text = Mathf.Round(countdown).ToString();
    }

    // IEnumerator t�r�nde bir fonksiyon olan SpawnWave, d��manlar�n dalga dalga ortaya ��kmas�n� sa�lar.
    IEnumerator SpawnWave()
    {
        waveIndex++; // Dalga dizinini bir art�r�r�z.

        // Dalga dizini kadar d��man�n spawn edilmesini sa�layan bir d�ng�.
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy(); // D��man spawn etmek i�in bir fonksiyonu �a��r�r�z.
            yield return new WaitForSeconds(0.2f); // Her d��man�n spawn edilmesi aras�nda 0.2 saniye bekleriz.
        }
    }


    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
