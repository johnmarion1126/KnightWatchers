using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Story4Continue : MonoBehaviour
{
    public void ContinueButton()
    {
        SceneManager.LoadScene("StoryScene1");
    }
}
