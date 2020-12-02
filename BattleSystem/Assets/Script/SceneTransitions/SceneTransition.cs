using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Image whiteFade;

    void Start()
    {
        whiteFade.canvasRenderer.SetAlpha(0.0f);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {   
        whiteFade.CrossFadeAlpha(1,2,false);
        if(other.CompareTag("Player") && !other.isTrigger)
        {   
           Invoke("changeScene",2);
        }
    }

    public void changeScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
