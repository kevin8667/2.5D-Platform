using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instanceRef;
    void Awake()
    {
        if (instanceRef == null)  //this code hasn't been executed before
        {
            instanceRef = this; //point to object instance currently executing this code
            DontDestroyOnLoad(gameObject); //don't destroy the gameObject this is attached to
        }
        else  //this object is not the first, it's an imposter that must be destroyed
        {
            DestroyImmediate(gameObject);
            Debug.Log("Destroy GameManager Imposter");
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
