 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target; // Hedefin Transform bileþeni

    public float speed = 50f; // Füzenin hýzý
    public GameObject impactEffect; // Hedefe çarpma efekti

    // Hedefi ayarlamak için kullanýlan fonksiyon
    public void Seek(Transform _target)
    {
        target = _target; // Verilen hedefi atanýr
    }

    // Her güncelleme adýmýnda çaðrýlan fonksiyon
    private void Update()
    {
        // Eðer hedef yoksa, bu füze oyun nesnesini yok eder ve iþlemi sonlandýrýr
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Füzenin hedefe doðru hareket etmesi için yön vektörü hesaplanýr
        Vector3 direction = target.position - transform.position;

        // Bu adýmda füzenin bu güncelleme adýmýnda gitmesi gereken mesafe hesaplanýr
        float distanceThisFrame = speed * Time.deltaTime;

        // Eðer füze, bu adýmda hedefe ulaþacaksa, hedefe çarptý fonksiyonunu çaðýrýr ve iþlemi sonlandýrýr
        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // Füze, hedefe ulaþmadýysa, belirtilen hýz ve yönde ilerlemeye devam eder
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    // Hedefe çarptýðýnda çaðrýlan fonksiyon
    void HitTarget()
    {
        // Hedefe çarpma efektini oluþturur
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        // Oluþturulan efekti 2 saniye sonra yok eder
        Destroy(effectIns, 2f);

        // Hedefi yok eder
        Destroy(target.gameObject);
        // Bu füze oyun nesnesini yok eder
        Destroy(gameObject);
    }

}
