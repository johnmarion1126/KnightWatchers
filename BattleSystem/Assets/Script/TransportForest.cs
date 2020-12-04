using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportForest : MonoBehaviour
{
    public GameObject Gorgon;

    void OnTriggerEnter2D(Collider2D col)
    {
        Gorgon.transform.position = new Vector3(88.57f, -22.7f, 0f); 
    }
}
