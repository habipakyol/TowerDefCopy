using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target; // Turret'ýn hedefi

    [Header("Öznitelik")]

    public float fireRate = 1f;
    public float fireCountDown = 0f;
    public float range = 15f; // Turret'ýn algýlama menzili

    [Header("Unity Kurulum Alanlarý")]
    public string enemyTag = "Enemy"; // Turret'ýn hedef olarak göreceði düþmanlarýn etiketi

    public Transform partToRotate; // Turret'ýn döneceði bölüm
    public float turnSpeed = 5f; // Turret'ýn dönme hýzý

    public GameObject bulletPrefab;
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        // Turret'ýn hedefini düzenli olarak güncellemek için UpdateTarget fonksiyonunu belirli bir sýklýkla çaðýrýr.
        InvokeRepeating("UpdateTarget", 0, 0.5f);
    }

    // Hedefi güncellemek için kullanýlan fonksiyon
    void UpdateTarget()
    {
        // Düþman etiketini taþýyan tüm GameObject'leri alýr
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity; // En kýsa mesafeyi sýfýra ayarlar
        GameObject nearestEnemy = null; // En yakýn düþmaný saklamak için bir deðiþken

        foreach (GameObject enemy in enemies)
        {
            // Turret ile düþman arasýndaki mesafeyi hesaplar
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            // Eðer bu düþman, önceki en yakýn düþmandan daha yakýnsa, bu düþmaný en yakýn düþman olarak iþaretler
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        // Eðer en yakýn düþman bulunmuþsa ve mesafe belirlenen menzil içindeyse, bu düþmaný hedef olarak belirler
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
        // Eðer bir hedef yoksa, dönmeye gerek yoktur, bu yüzden fonksiyonu terk eder
        if (target == null)
        {
            return;
        }

        // Turret'ýn hedefe doðru dönmesini hesaplar
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountDown <= 0f)
        {
            Shoot(); // Eðer ateþleme süresi sýfýrsa, atýþ yapar
            fireCountDown = 1f / fireRate; // Bir sonraki atýþýn bekleme süresini ayarlar
        }

        fireCountDown -= Time.deltaTime; // Atýþ bekleme süresini azaltýr, zamanlayýcýyý günceller

    }

    void Shoot()
    {
        //Debug.Log("Ateþ Edildi"); // Ateþleme anýný loglama amacýyla kullanýlan bir satýr (isteðe baðlý)

        // Kurþunun (bullet) nesnesini oluþturur ve ateþleme noktasýnda konumlandýrýr
        GameObject bulletGameObject = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Oluþturulan kurþun nesnesinin Bullet bileþenini alýr
        Bullet bullet = bulletGameObject.GetComponent<Bullet>();

        // Bullet bileþeni varsa, hedefi belirlemek için hedefi atar (Seek fonksiyonunu çaðýrýr)
        if (bullet != null)
        {
            bullet.Seek(target); // Kurþunun hedefi belirlemesi için Seek fonksiyonunu çaðýrýr
        }
    }


    // Gizmos, sahne görünümünde Turret'ýn algýlama menzilini çizer
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
