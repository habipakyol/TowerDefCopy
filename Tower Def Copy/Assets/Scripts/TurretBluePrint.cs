using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //Bu s�n�f�n �zellikleri, Unity Edit�r�nde d�zenlenebilir ve bu nesnenin verileri kolayca serile�tirilebilir veya ayarlanabilir hale gelir.
public class TurretBluePrint
{
    public GameObject prefab; // Kule �rne�ini i�eren oyun nesnesi
    public int cost; // Kule yap�s�n�n maliyeti

    // Kuleyi satmak i�in alınacak miktar� hesaplayan fonksiyon
    public int GetSellAmount()
    {
        return cost / 2; // Kuleyi satarken maliyetin yar�s� kadar para verir
    }
}

