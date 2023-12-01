using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1;
    static bool check = true;
    static int count;
    public GameObject swipePanel;
    static bool first=false;
    static bool touched = true;
    private bool isPressed = false;
    public HoleMovement holeMovement;
 

    void Start()
    {
        if (PlayerPrefs.GetInt("scene") <= 0)
        {
            count = 1;
        }
        else
        {
            count = PlayerPrefs.GetInt("scene");
        }
        if (check)
        {
            Invoke("LoadNextLevel", 2);
        }
        if (first)
        {
            swipePanel.SetActive(true);
            holeMovement.enabled = false;
            first= false; 
        }
        else
        {
            swipePanel.SetActive(false);
        }
        
        //if(AdManager.i != null)
        //{
        //    if (AdManager.showAdd)
        //    {
        //        AdManager.i.ShowInterstitial();
        //        Invoke("banner", 1f);
        //    }
        //    else
        //    {
        //        AdManager.showAdd = true;
        //    }
        //}
        
    }

    //void banner()
    //{
    //    AdManager.i.RequestBanner();
    //}

    
    private void Update()
    {
        if (!touched&& Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            isPressed = true;
        }

        if (!touched && Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            if (isPressed)
            {
                swipePanel.SetActive(false);
                holeMovement.enabled = true;
                //Debug.Log("User clicked or touched the screen!");
            }
            isPressed = false;
            touched = true;
        }
    }


    void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(count));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
        Vibrator.Vibrate(UIController.vibrationVol);
        check = false;
        print("INVOKEEEEEEEEEEEEEEE");
        first = true;
        touched = false;
    }
}