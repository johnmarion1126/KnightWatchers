using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleCollide : MonoBehaviour
{
    public string sceneToLoad = "BattleScene";

    void OnTriggerEnter2D(Collider2D col)
    {
        SceneManager.LoadScene(sceneToLoad);
        Debug.Log("GameObject2 collided with " + col.name);
    }
}
