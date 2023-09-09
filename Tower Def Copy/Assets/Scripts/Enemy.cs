using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;

    private Transform target; //Hedef noktayý temsil eden deðiþken

    private int wavepointIndex = 0; //Yol noktasý dizinini temsil eden deðiþken
   
    void Start()
    {
        target = WayPoints.points[0]; // Ýlk hedef noktasý
    }

    // Update is called once per frame
    void Update()
    {
        // Hedef noktaya yönelmek için gereken vektör hesaplandý.
        Vector3 direction = target.position - transform.position;
        //Nesne, hedefe yönelmek ve belirtilen hýzda hareket etmek üzere güncellenir.
        transform.Translate(direction.normalized * speed*Time.deltaTime,Space.World);
        //Eðer nesne ve hedef nokta arasýndaki mesafe belirtilen eþik deðerden küçük veya eþitse
        if(Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            GetNextWayPoint();
        }
    }
    //Bir sonraki hedef noktaya belirlemek içn kullanýlan metod
    void GetNextWayPoint()
    {
        if(wavepointIndex >= WayPoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        wavepointIndex++;
        target = WayPoints.points[wavepointIndex];
    }
}
