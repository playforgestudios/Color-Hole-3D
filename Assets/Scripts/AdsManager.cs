using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine;


public class AdsManager : MonoBehaviour
{
    private BannerView bannerAd;
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;
    public static bool showAdd = false;


    public static AdsManager ads;

    private void Awake()
    {
        if (ads == null)
        {
            ads = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        

    }
    void Start()
    {
        MobileAds.Initialize(initStatus =>
        {

        });
        this.RequestBanner();
        this.LoadRewardedAd();
        if (showAdd)
        {
            StartCoroutine(RequestInterstitial());
        }

    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest();
    }

    private void RequestBanner()
    {
        string adUnitId = "ca-app-pub-5080271606964191/8647981595";
        AdSize adaptiveSize =
                AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        this.bannerAd = new BannerView(adUnitId, adaptiveSize, AdPosition.Bottom);
        AdRequest request = new AdRequest();
        // Load a banner ad.
        this.bannerAd.LoadAd(request);
    }

    
    public IEnumerator RequestInterstitial()
    {
        string adUnitId = "ca-app-pub-5080271606964191/7977063605";
        // Clean up the old ad before loading a new one.
        if (interstitial != null)
        {
            interstitial.Destroy();
            interstitial = null;
        }

        print("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        //adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        InterstitialAd.Load(adUnitId, adRequest, (InterstitialAd ad, LoadAdError error) =>
        {
            // if error is not null, the load request failed.
            if (error != null || ad == null)
            {
                return;
            }

            print("Interstitial ad loaded with response : " + ad.GetResponseInfo());

            interstitial = ad; 
        });

        yield return new WaitForSeconds(2f);
        ShowLoadedInterstitial();

    }




    public void ShowLoadedInterstitial()
    {
        if (interstitial != null && interstitial.CanShowAd())
        {
            interstitial.Show();
        }
        else
        {

        }  
    }

    public void LoadRewardedAd()
    {
        string _adUnitId = "ca-app-pub-5080271606964191/7053443299";
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        //adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        RewardedAd.Load(_adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    return;
                }

                rewardedAd = ad;
            });
    }

    public void ShowRewardedAd()
    {
        /*const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";*/

        if (rewardedAd != null && rewardedAd.CanShowAd())
        {

            rewardedAd.Show((Reward reward) =>
            {
                UIController.uIController.ResumeGame();
            });
        }
        LoadRewardedAd();
    }
}

