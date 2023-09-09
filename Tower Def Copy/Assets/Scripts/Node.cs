using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // oyun nesneleri ve kullanýcý etkileþimi arasýnda olaylarý (events) iþlemek için kullanýlýr.

public class Node : MonoBehaviour
{

    public Color hoverColor; // Fare üzerine gelindiðinde düðme rengi
    public Vector3 positionOffset; // Kule yerleþtirme konumunun düðmenin merkezinden ne kadar kaydýrýlacaðý

    [Header("Optional")]
    public GameObject turret; // Bu düðmeye yerleþtirilmiþ kule oyun nesnesi

    private Renderer rend; // Düðmenin rendere bileþeni
    private Color startColor; // Düðmenin baþlangýç rengi

    BuildManager buildManager; // Kule inþa yöneticisi

    private void Start()
    {
        rend = GetComponent<Renderer>(); // Düðmenin Renderer bileþenini alýr
        startColor = rend.material.color; // Düðmenin baþlangýç rengini kaydeder

        buildManager = BuildManager.instance; // Kule inþa yöneticisini alýr
    }

    // Kule inþa konumunu hesaplamak için kullanýlan fonksiyon
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset; // Düðmenin merkezine pozisyon ofsetini ekler
    }

    private void OnMouseDown()
    {
        // Eðer fare üzerinde bir UI öðesi varsa iþlemi iptal eder
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // Eðer inþa yapma yeteneði yoksa iþlemi iptal eder
        if (!buildManager.CanBuild)
        {
            return;
        }

        // Eðer düðmede zaten bir kule varsa iþlemi iptal eder ve bir hata mesajý gösterir
        if (turret != null)
        {
            Debug.Log("Ýnþa edilemeyen alan");
            return;
        }

        // Kule inþa yöneticisi aracýlýðýyla kule inþa iþlemini baþlatýr
        buildManager.BuildTurretOn(this);
    }

    // Fare düðmenin üzerine gelindiðinde çaðrýlan fonksiyon
    void OnMouseEnter()
    {
        // Eðer fare üzerinde bir UI öðesi varsa iþlemi iptal eder
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // Eðer inþa yapma yeteneði yoksa iþlemi iptal eder
        if (!buildManager.CanBuild)
        {
            return;
        }

        // Düðmenin rengini fare üzerine gelindiðindeki rengiyle deðiþtirir
        rend.material.color = hoverColor;
    }

    // Fare düðmenin üzerinden çýkýldýðýnda çaðrýlan fonksiyon
    private void OnMouseExit()
    {
        // Düðmenin rengini baþlangýç rengine geri döndürür
        rend.material.color = startColor;
    }
}


