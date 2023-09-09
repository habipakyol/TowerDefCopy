using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // oyun nesneleri ve kullan�c� etkile�imi aras�nda olaylar� (events) i�lemek i�in kullan�l�r.

public class Node : MonoBehaviour
{

    public Color hoverColor; // Fare �zerine gelindi�inde d��me rengi
    public Vector3 positionOffset; // Kule yerle�tirme konumunun d��menin merkezinden ne kadar kayd�r�laca��

    [Header("Optional")]
    public GameObject turret; // Bu d��meye yerle�tirilmi� kule oyun nesnesi

    private Renderer rend; // D��menin rendere bile�eni
    private Color startColor; // D��menin ba�lang�� rengi

    BuildManager buildManager; // Kule in�a y�neticisi

    private void Start()
    {
        rend = GetComponent<Renderer>(); // D��menin Renderer bile�enini al�r
        startColor = rend.material.color; // D��menin ba�lang�� rengini kaydeder

        buildManager = BuildManager.instance; // Kule in�a y�neticisini al�r
    }

    // Kule in�a konumunu hesaplamak i�in kullan�lan fonksiyon
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset; // D��menin merkezine pozisyon ofsetini ekler
    }

    private void OnMouseDown()
    {
        // E�er fare �zerinde bir UI ��esi varsa i�lemi iptal eder
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // E�er in�a yapma yetene�i yoksa i�lemi iptal eder
        if (!buildManager.CanBuild)
        {
            return;
        }

        // E�er d��mede zaten bir kule varsa i�lemi iptal eder ve bir hata mesaj� g�sterir
        if (turret != null)
        {
            Debug.Log("�n�a edilemeyen alan");
            return;
        }

        // Kule in�a y�neticisi arac�l���yla kule in�a i�lemini ba�lat�r
        buildManager.BuildTurretOn(this);
    }

    // Fare d��menin �zerine gelindi�inde �a�r�lan fonksiyon
    void OnMouseEnter()
    {
        // E�er fare �zerinde bir UI ��esi varsa i�lemi iptal eder
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // E�er in�a yapma yetene�i yoksa i�lemi iptal eder
        if (!buildManager.CanBuild)
        {
            return;
        }

        // D��menin rengini fare �zerine gelindi�indeki rengiyle de�i�tirir
        rend.material.color = hoverColor;
    }

    // Fare d��menin �zerinden ��k�ld���nda �a�r�lan fonksiyon
    private void OnMouseExit()
    {
        // D��menin rengini ba�lang�� rengine geri d�nd�r�r
        rend.material.color = startColor;
    }
}


