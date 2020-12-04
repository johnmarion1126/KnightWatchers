using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeEnemyTown : MonoBehaviour
{
    public GameObject Enemy;

    void Start()
    {
     for (int i = 0; i < 5; ++i){
            Vector3 pos = new Vector3 (Random.Range(61f, 130f), Random.Range(-15f, 11f), 0);
            Instantiate(Enemy, pos, Quaternion.identity);   
    }
}
}