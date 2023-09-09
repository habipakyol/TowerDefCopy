using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static int Money; // Oyuncunun para miktarýný temsil eden deðiþken (statik olarak tanýmlanmýþ, tüm örnekler için ayný deðeri paylaþýr)
    public int startMoney = 400; // Baþlangýçta oyuncuya verilen para miktarý

    void Start()
    {
        Money = startMoney; // Oyuncunun para miktarýný baþlangýçta belirtilen miktarla baþlatýr
    }

}
