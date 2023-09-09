 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target; // Hedefin Transform bile�eni

    public float speed = 50f; // F�zenin h�z�
    public GameObject impactEffect; // Hedefe �arpma efekti

    // Hedefi ayarlamak i�in kullan�lan fonksiyon
    public void Seek(Transform _target)
    {
        target = _target; // Verilen hedefi atan�r
    }

    // Her g�ncelleme ad�m�nda �a�r�lan fonksiyon
    private void Update()
    {
        // E�er hedef yoksa, bu f�ze oyun nesnesini yok eder ve i�lemi sonland�r�r
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // F�zenin hedefe do�ru hareket etmesi i�in y�n vekt�r� hesaplan�r
        Vector3 direction = target.position - transform.position;

        // Bu ad�mda f�zenin bu g�ncelleme ad�m�nda gitmesi gereken mesafe hesaplan�r
        float distanceThisFrame = speed * Time.deltaTime;

        // E�er f�ze, bu ad�mda hedefe ula�acaksa, hedefe �arpt� fonksiyonunu �a��r�r ve i�lemi sonland�r�r
        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // F�ze, hedefe ula�mad�ysa, belirtilen h�z ve y�nde ilerlemeye devam eder
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    // Hedefe �arpt���nda �a�r�lan fonksiyon
    void HitTarget()
    {
        // Hedefe �arpma efektini olu�turur
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        // Olu�turulan efekti 2 saniye sonra yok eder
        Destroy(effectIns, 2f);

        // Hedefi yok eder
        Destroy(target.gameObject);
        // Bu f�ze oyun nesnesini yok eder
        Destroy(gameObject);
    }

}
