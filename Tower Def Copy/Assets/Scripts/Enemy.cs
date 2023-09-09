using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;

    private Transform target; //Hedef noktay� temsil eden de�i�ken

    private int wavepointIndex = 0; //Yol noktas� dizinini temsil eden de�i�ken
   
    void Start()
    {
        target = WayPoints.points[0]; // �lk hedef noktas�
    }

    // Update is called once per frame
    void Update()
    {
        // Hedef noktaya y�nelmek i�in gereken vekt�r hesapland�.
        Vector3 direction = target.position - transform.position;
        //Nesne, hedefe y�nelmek ve belirtilen h�zda hareket etmek �zere g�ncellenir.
        transform.Translate(direction.normalized * speed*Time.deltaTime,Space.World);
        //E�er nesne ve hedef nokta aras�ndaki mesafe belirtilen e�ik de�erden k���k veya e�itse
        if(Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            GetNextWayPoint();
        }
    }
    //Bir sonraki hedef noktaya belirlemek i�n kullan�lan metod
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
