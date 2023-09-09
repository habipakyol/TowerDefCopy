using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBluePrint standardTurret; // Standart kule yapýsýnýn bilgilerini içeren TurretBluePrint sýnýfýndan bir nesne
    public TurretBluePrint secondTurret;   // Ýkinci bir kule yapýsýnýn bilgilerini içeren TurretBluePrint sýnýfýndan bir nesne

    BuildManager buildManager; // Kule inþa iþlemlerini yöneten BuildManager sýnýfýnýn bir örneði

    private void Start()
    {
        // BuildManager örneðini baþlangýçta alýr
        buildManager = BuildManager.instance;
    }

    // Standart kuleyi satýn almak için çaðrýlan fonksiyon
    public void PurchaseStandardTurret()
    {
        Debug.Log("Standart kule satýn alýndý."); // Satýn alma iþlemini kaydeder (loglar)

        // BuildManager üzerinden seçilen kuleyi ayarlar
        buildManager.SelectTurretToBuild(standardTurret);
    }

    // Ýkinci kuleyi satýn almak için çaðrýlan fonksiyon
    public void PurchaseSecondTurret()
    {
        Debug.Log("Ýkinci kule satýn alýndý."); // Satýn alma iþlemini kaydeder (loglar)

        // BuildManager üzerinden seçilen kuleyi ayarlar
        buildManager.SelectTurretToBuild(secondTurret);
    }

}
