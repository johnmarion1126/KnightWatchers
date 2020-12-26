using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportPlayer : MonoBehaviour
{
    public GameObject Gorgon;
    private float x;
    private float y;
    private float z;

    void Start()
    {
        Gorgon.transform.position = new Vector3(
        PlayerPrefs.GetFloat("x"),
        PlayerPrefs.GetFloat("y"),
        PlayerPrefs.GetFloat("z")
        );
        
    }

    void Update()
    {
        PlayerPrefs.SetFloat("x",Gorgon.transform.position.x);
        PlayerPrefs.SetFloat("y",Gorgon.transform.position.y);
        PlayerPrefs.SetFloat("z",Gorgon.transform.position.z);
    }
}
