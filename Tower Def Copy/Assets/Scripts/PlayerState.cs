using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static int Money; // Oyuncunun para miktar�n� temsil eden de�i�ken (statik olarak tan�mlanm��, t�m �rnekler i�in ayn� de�eri payla��r)
    public int startMoney = 400; // Ba�lang��ta oyuncuya verilen para miktar�

    void Start()
    {
        Money = startMoney; // Oyuncunun para miktar�n� ba�lang��ta belirtilen miktarla ba�lat�r
    }

}
