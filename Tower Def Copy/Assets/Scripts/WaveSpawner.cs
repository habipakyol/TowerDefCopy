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

    // Dalga arasýndaki süre, saniye cinsinden belirlenir ve varsayýlan olarak 4 saniye olarak ayarlanýr.
    public float timeBetWaves = 4f;

    // Geri sayým süresi, dalga aralarý arasýndaki süreyi hesaplamak için kullanýlýr.
    private float countdown = 0f;

    // Dalga dizinini takip etmek için bir deðiþken. Ýlk dalga için 0'dan baþlar.
    private int waveIndex = 0;

    // Update fonksiyonu, her çerçeve yeniden hesaplandýðýnda çaðrýlýr ve oyunun ana mantýðýný içerir.
    void Update()
    {
        // Geri sayým sýfýrsa, yeni bir dalga baþlatmak için Coroutine'i baþlatýrýz.
        if (countdown <= 0)
        {
            StartCoroutine(SpawnWave()); // Yeni bir dalga baþlatmak için IEnumerator SpawnWave fonksiyonunu çaðýrýr.
            countdown = timeBetWaves; // Geri sayýmý dalga arasýndaki süreyle yeniden baþlatýrýz.
        }

        countdown -= Time.deltaTime; // Her çerçeve, geri sayým süresini zaman geçiþine (deltaTime) göre azaltýr.

        // waveCountdownText metin nesnesine geri sayým süresini yuvarlayarak yazdýrýrýz.
        waveCountdownText.text = Mathf.Round(countdown).ToString();
    }

    // IEnumerator türünde bir fonksiyon olan SpawnWave, düþmanlarýn dalga dalga ortaya çýkmasýný saðlar.
    IEnumerator SpawnWave()
    {
        waveIndex++; // Dalga dizinini bir artýrýrýz.

        // Dalga dizini kadar düþmanýn spawn edilmesini saðlayan bir döngü.
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy(); // Düþman spawn etmek için bir fonksiyonu çaðýrýrýz.
            yield return new WaitForSeconds(0.2f); // Her düþmanýn spawn edilmesi arasýnda 0.2 saniye bekleriz.
        }
    }


    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
