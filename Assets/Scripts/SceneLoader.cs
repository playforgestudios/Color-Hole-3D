using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private int randomNumber;
    void Start()
    {
        randomNumber = Random.Range(1, 6);
        Invoke("LoadScene", 5f);
    }

    void LoadScene()
    {
        SceneManager.LoadScene(randomNumber);
    }
}
