using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    float currentTime = 0;
    float startTime;
    public Text timeLimit;
    bool timeCheck;
    public bool check=false;
    public GameObject continueButton;
    public Text gameOverText;
    public GameObject filBar;
    public Text noThanks;
    public GameObject gameOverPanel;
    public GameObject rewardedInterstitialPanel;
    public static UIController uIController;
    public Image fillBarImage;
    public Animator anim;
    public static int vibrationVol=80;
    public Button onButton;
    public Button offButton;
    public GameObject settingsPanel;
    [SerializeField] Image progressFillImage;

    private void Awake()
    {
        if (uIController == null)
        {
            uIController = this;
        }
        else
        {
            Destroy(uIController);
        }
    }

    private void Start()
    {
        startTime = 15;
        currentTime = startTime;
        gameOverPanel.SetActive(false);
        rewardedInterstitialPanel.SetActive(false);
    }

    private void Update()
    {
        if (check)
        {
            currentTime -= 1 * Time.deltaTime;
            fillBarImage.fillAmount -= 0.067f*Time.deltaTime;
        }
        timeLimit.text = currentTime.ToString("0");
        if (currentTime<=0)
        {
            check = false;
            continueButton.SetActive(false);
            gameOverText.text = "";
            timeLimit.text = "";
            filBar.SetActive(false);
            noThanks.text = "Tap to Restart";
        }
        if (Magnet.instance.isGameOver)
        {
            //gameOverPanel.SetActive(true);
            rewardedInterstitialPanel.SetActive(true);
            check=true;
        }
    }

    public void UpdateLevelProgress()
    {
        float val = 1f - ((float)Level.Instance.objectsInScene / Level.Instance.totalObjects);
        //transition fill (0.4 seconds)
        progressFillImage.DOFillAmount(val, .4f);
    }


    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResumeGame()
    {
        //if (AdManager.i.ShowRewardedAd())
        //{
            currentTime = 15;
            check = false;
            fillBarImage.fillAmount = 1f;
            //gameOverPanel.SetActive(false);
            rewardedInterstitialPanel.SetActive(false);
            Game.isGameover = false;
            anim.SetBool("isSad", false);
        //}
    }

    public void VibrationON()
    {
        onButton.GetComponent<Image>().color = UnityEngine.Color.blue;
        offButton.GetComponent<Image>().color = UnityEngine.Color.white;
        vibrationVol = 100;
        
    }
    public void VibrationOFF()
    {
        onButton.GetComponent<Image>().color = UnityEngine.Color.white;
        offButton.GetComponent<Image>().color = UnityEngine.Color.blue;
        vibrationVol = 0;
    }

    public void EnableSettingPanel()
    {
        settingsPanel.SetActive(true);
    }
    public void DisableSettingPanel()
    {
        settingsPanel.SetActive(false);
    }

}
