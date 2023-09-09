using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBluePrint standardTurret; // Standart kule yap�s�n�n bilgilerini i�eren TurretBluePrint s�n�f�ndan bir nesne
    public TurretBluePrint secondTurret;   // �kinci bir kule yap�s�n�n bilgilerini i�eren TurretBluePrint s�n�f�ndan bir nesne

    BuildManager buildManager; // Kule in�a i�lemlerini y�neten BuildManager s�n�f�n�n bir �rne�i

    private void Start()
    {
        // BuildManager �rne�ini ba�lang��ta al�r
        buildManager = BuildManager.instance;
    }

    // Standart kuleyi sat�n almak i�in �a�r�lan fonksiyon
    public void PurchaseStandardTurret()
    {
        Debug.Log("Standart kule sat�n al�nd�."); // Sat�n alma i�lemini kaydeder (loglar)

        // BuildManager �zerinden se�ilen kuleyi ayarlar
        buildManager.SelectTurretToBuild(standardTurret);
    }

    // �kinci kuleyi sat�n almak i�in �a�r�lan fonksiyon
    public void PurchaseSecondTurret()
    {
        Debug.Log("�kinci kule sat�n al�nd�."); // Sat�n alma i�lemini kaydeder (loglar)

        // BuildManager �zerinden se�ilen kuleyi ayarlar
        buildManager.SelectTurretToBuild(secondTurret);
    }

}
