using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1;
    static bool check = true;
    bool hasSwiped = false;

    void Start()
    {
        if (check)
        {
            Invoke("LoadNextLevel", 3);
        }

    }


    void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
        Vibrator.Vibrate(100);
        check = false;
    }

}
