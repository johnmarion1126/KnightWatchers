using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeEnemy : MonoBehaviour
{
    public GameObject Enemy1;

    void Start()
    {
        for (int i = 0; i < 5; ++i){
            Vector3 pos = new Vector3 (Random.Range(-18f, 18f), Random.Range(-15f, 11f), 0);
            Instantiate(Enemy1, pos, Quaternion.identity);
        }
    }
}
