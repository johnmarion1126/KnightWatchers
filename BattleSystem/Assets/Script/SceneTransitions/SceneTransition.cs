using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public GameObject Gorgon;
    public string scene;

    public void OnTriggerEnter2D(Collider2D other)
    {   
        SceneManager.LoadScene(scene);
    }
}
