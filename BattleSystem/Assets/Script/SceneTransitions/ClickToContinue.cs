using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickToContinue : MonoBehaviour
{
    public string scene;
    public Image whiteFade;
    
    void Start()
    {
        whiteFade.canvasRenderer.SetAlpha(0.0f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            whiteFade.CrossFadeAlpha(1,2,false);
            Invoke("changeScene",2);
        }
    }

    public void changeScene()
    {
        SceneManager.LoadScene(scene);
    }
}
