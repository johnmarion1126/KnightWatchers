using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportTown : MonoBehaviour
{
    public GameObject Gorgon;

    void OnTriggerEnter2D(Collider2D col)
    {
        Gorgon.transform.position = new Vector3(0f,13f,0f);
    }
}
