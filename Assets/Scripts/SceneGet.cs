using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneGet : MonoBehaviour
{
   public void LoadA(string scenename)
    {
        Debug.Log("sceneName to load: " + scenename);
        SceneManager.LoadScene(scenename);
    }
}
