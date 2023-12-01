using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    private static BannerView bannerAd;
    private static InterstitialAd interstitialAd;
    private static RewardedAd rewardedAd;
    private static RewardedInterstitialAd rewardedInterstitialAd;
    static bool adsInitialized;
    public static bool showAdd;


    public static AdManager i;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if(i != null && i!= this)
            Destroy(this.gameObject);
        else
            i = this;
    }

    void Start()
    {
        //InitializeAds();
    }

    void InitializeAds()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            adsInitialized = true;
            LoadInterstitial();
            LoadRewardedInterstitialAd();
            LoadRewardedAd();
        });
        
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest();
    }

    public void RequestBanner()
    {
        if(!adsInitialized)
            InitializeAds();
        string adUnitId = "ca-app-pub-5080271606964191/8647981595";
        if (bannerAd != null)
        {
            bannerAd.Destroy();
            bannerAd = null;
        }
        AdSize adaptiveSize =
                AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        bannerAd = new BannerView(adUnitId, adaptiveSize, AdPosition.Bottom);
        AdRequest request = new AdRequest();
        bannerAd.LoadAd(request);
    }

    public void DestroyBanner()
    {
        if(bannerAd != null)
        {
            bannerAd.Destroy();
            bannerAd = null;
            print("destroyed");
        }
    }

    public void LoadInterstitial()
    {
        if (!adsInitialized)
            InitializeAds();
        string adUnitId = "ca-app-pub-5080271606964191/7977063605";
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        InterstitialAd.Load(adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    //Debug.LogError("interstitial ad failed to load an ad " +
                    //               "with error : " + error);
                    return;
                }

                //Debug.Log("Interstitial ad loaded with response : "
                //          + ad.GetResponseInfo());

                interstitialAd = ad;
            });
    }

    public void ShowInterstitial()
    {
        if (!adsInitialized)
            InitializeAds();
        print("showing interstitial ad");
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            print("showing");
            interstitialAd.Show();
        }
        else
        {
            print("failed to show interstitial ad");
        }
        LoadInterstitial();
    }

    public void LoadRewardedAd()
    {
        if (!adsInitialized)
            InitializeAds();
        string _adUnitId = "ca-app-pub-5080271606964191/7053443299";
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        print("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        RewardedAd.Load(_adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    //Debug.LogError("Rewarded ad failed to load an ad " +
                    //               "with error : " + error);
                    return;
                }

                //Debug.Log("Rewarded ad loaded with response : "
                //          + ad.GetResponseInfo());

                rewardedAd = ad;
            });
    }

    public bool ShowRewardedAd()
    {
        if (!adsInitialized)
            InitializeAds();
        //const string rewardMsg =
        //    "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                //Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
            });
            this.LoadRewardedAd();
            return true;
        }
        this.LoadRewardedAd();
        return false;
    }

    //////////////
    //////////////Rewarded Interstitial////////////////////////////
    /////////////
    public void LoadRewardedInterstitialAd()
    {
        if (!adsInitialized)
            InitializeAds();
        string adUnitId = "ca-app-pub-5080271606964191/2386561662";
        // Clean up the old ad before loading a new one.
        if (rewardedInterstitialAd != null)
        {
            rewardedInterstitialAd.Destroy();
            rewardedInterstitialAd = null;
        }

        //Debug.Log("Loading the rewarded interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        RewardedInterstitialAd.Load(adUnitId, adRequest,
            (RewardedInterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    //Debug.LogError("rewarded interstitial ad failed to load an ad " +
                    //               "with error : " + error);
                    return;
                }

                //Debug.Log("Rewarded interstitial ad loaded with response : "
                //          + ad.GetResponseInfo());

                rewardedInterstitialAd = ad;
            });
        
    }

    public void ShowRewardedInterstitialAd()
    {
        if (!adsInitialized)
            InitializeAds();
        //const string rewardMsg =
        //"Rewarded interstitial ad rewarded the user. Type: {0}, amount: {1}.";

        if (rewardedInterstitialAd != null && rewardedInterstitialAd.CanShowAd())
        {
            rewardedInterstitialAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                //Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
            });
        }
        RegisterEventHandlers(rewardedInterstitialAd);

    }

    private void RegisterEventHandlers(RewardedInterstitialAd ad)
    {
        if(ad == null)
        {
            LoadRewardedInterstitialAd();
            return;
        }
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            print(String.Format("Rewarded interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            UIController.uIController.ResumeGame();
            LoadRewardedInterstitialAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            LoadRewardedInterstitialAd();
            //Debug.LogError("Rewarded interstitial ad failed to open full screen content " +
            //               "with error : " + error);
        };
    }
}
