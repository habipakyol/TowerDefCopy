using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target; // Turret'�n hedefi

    [Header("�znitelik")]

    public float fireRate = 1f;
    public float fireCountDown = 0f;
    public float range = 15f; // Turret'�n alg�lama menzili

    [Header("Unity Kurulum Alanlar�")]
    public string enemyTag = "Enemy"; // Turret'�n hedef olarak g�rece�i d��manlar�n etiketi

    public Transform partToRotate; // Turret'�n d�nece�i b�l�m
    public float turnSpeed = 5f; // Turret'�n d�nme h�z�

    public GameObject bulletPrefab;
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        // Turret'�n hedefini d�zenli olarak g�ncellemek i�in UpdateTarget fonksiyonunu belirli bir s�kl�kla �a��r�r.
        InvokeRepeating("UpdateTarget", 0, 0.5f);
    }

    // Hedefi g�ncellemek i�in kullan�lan fonksiyon
    void UpdateTarget()
    {
        // D��man etiketini ta��yan t�m GameObject'leri al�r
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity; // En k�sa mesafeyi s�f�ra ayarlar
        GameObject nearestEnemy = null; // En yak�n d��man� saklamak i�in bir de�i�ken

        foreach (GameObject enemy in enemies)
        {
            // Turret ile d��man aras�ndaki mesafeyi hesaplar
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            // E�er bu d��man, �nceki en yak�n d��mandan daha yak�nsa, bu d��man� en yak�n d��man olarak i�aretler
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        // E�er en yak�n d��man bulunmu�sa ve mesafe belirlenen menzil i�indeyse, bu d��man� hedef olarak belirler
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null; // Hedef yoksa target'i null yapar
        }
    }

    // Update is called once per frame
    void Update()
    {
        // E�er bir hedef yoksa, d�nmeye gerek yoktur, bu y�zden fonksiyonu terk eder
        if (target == null)
        {
            return;
        }

        // Turret'�n hedefe do�ru d�nmesini hesaplar
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountDown <= 0f)
        {
            Shoot(); // E�er ate�leme s�resi s�f�rsa, at�� yapar
            fireCountDown = 1f / fireRate; // Bir sonraki at���n bekleme s�resini ayarlar
        }

        fireCountDown -= Time.deltaTime; // At�� bekleme s�resini azalt�r, zamanlay�c�y� g�nceller

    }

    void Shoot()
    {
        //Debug.Log("Ate� Edildi"); // Ate�leme an�n� loglama amac�yla kullan�lan bir sat�r (iste�e ba�l�)

        // Kur�unun (bullet) nesnesini olu�turur ve ate�leme noktas�nda konumland�r�r
        GameObject bulletGameObject = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Olu�turulan kur�un nesnesinin Bullet bile�enini al�r
        Bullet bullet = bulletGameObject.GetComponent<Bullet>();

        // Bullet bile�eni varsa, hedefi belirlemek i�in hedefi atar (Seek fonksiyonunu �a��r�r)
        if (bullet != null)
        {
            bullet.Seek(target); // Kur�unun hedefi belirlemesi i�in Seek fonksiyonunu �a��r�r
        }
    }


    // Gizmos, sahne g�r�n�m�nde Turret'�n alg�lama menzilini �izer
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
