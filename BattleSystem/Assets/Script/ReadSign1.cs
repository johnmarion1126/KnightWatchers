using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadSign1 : MonoBehaviour
{
    public GameObject Gorgon;
    public string text;

    public void OnTriggerStay2D(Collider2D Gorgon) 
    {
        Debug.Log(text);
    }
}
