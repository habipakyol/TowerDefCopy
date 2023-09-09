using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance; // BuildManager s�n�f�n�n bir �rne�i (singleton) 
    // // Bu s�n�f�n tek bir �rne�ine eri�im sa�layacak bir "instance" adl� statik bir �zellik (property) tan�mlan�yor.

    private void Awake()
    {
        // E�er daha �nce bir BuildManager �rne�i olu�turulmu�sa hata verir
        if (instance != null)
        {
            Debug.Log("Sahnede birden fazla BuildManager bulunuyor.");
            return;
        }
        instance = this; // Singleton �rne�ini ayarlar
    }

    public GameObject standartTurretPrefab; // Standart kule �rne�i
    public GameObject anotherTurretPrefab; // Ba�ka bir kule �rne�i

    private TurretBluePrint turretToBuild; // �n�a edilecek kule bilgisi

    public bool CanBuild { get { return turretToBuild != null; } } // Kule in�a edilebilir mi?

    public void BuildTurretOn(Node node)
    {
        // Oyuncunun paras�, se�ilen kuleyi in�a etmek i�in yeterli mi kontrol edilir
        if (PlayerState.Money < turretToBuild.cost)
        {
            Debug.Log("Yeterli Para Yok.");
            return;
        }

        // Kuleyi in�a etmek i�in gerekli paray� oyuncunun paras�ndan ��kar
        PlayerState.Money -= turretToBuild.cost;

        // Se�ilen kuleyi d��menin konumunda olu�tur
        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret; // D��menin �zerine in�a edilen kuleyi atar

        // �n�a edildi�ini ve kalan para miktar�n� bildirir
        Debug.Log("�n�a edildi. Kalan Para: " + PlayerState.Money);
    }

    // �n�a edilecek kuleyi se�mek i�in kullan�lan fonksiyon
    public void SelectTurretToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret; // Se�ilen kuleyi ayarlar
    }

    
}
