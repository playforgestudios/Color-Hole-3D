using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RewardedInterstitialAdController : MonoBehaviour
{
    [SerializeField] private Text timerText;

    private void OnEnable()
    {
        StartCoroutine(StartRewatdedInterstitialAdTimer());
    }

    IEnumerator StartRewatdedInterstitialAdTimer()
    {
        timerText.text = "5";
        int t = 5;
        while (t > 0)
        {
            t--;
            timerText.text = t.ToString();
            yield return new WaitForSeconds(1f);
        }

        // show rewarded interstitial ad
        AdManager.i.ShowRewardedInterstitialAd();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
