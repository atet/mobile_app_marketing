using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    private const bool MODE_TESTING_ADS = true;
    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    private string iDAdMobApp, iDAdMobAdBanner, iDAdMobAdInterstitial;
    void Start()
    {
        // NOTE: I currently have the AdMob IDs read from file in Awake() on Global.
        // Therefore, do the remainder in this Start()
        iDAdMobApp = Global.instance.GetSecret().iDAdMobApp;
        iDAdMobAdBanner = Global.instance.GetSecret().iDAdMobAdBanner;
        iDAdMobAdInterstitial = Global.instance.GetSecret().iDAdMobAdInterstitial;

        MobileAds.Initialize(iDAdMobApp);
    }

    public void OnClickShowAdBanner()
    {
        RequestAdBanner();
    }
    private void RequestAdBanner()
    {
        if(MODE_TESTING_ADS)
        {
            // Google "Sample Ad Unit": https://developers.google.com/admob/unity/test-ads
            bannerView = new BannerView("ca-app-pub-3940256099942544/6300978111", AdSize.Banner, AdPosition.Bottom);
        }
        else
        {
            bannerView = new BannerView(iDAdMobAdBanner, AdSize.Banner, AdPosition.Bottom);            
        }
        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
    }

    public void OnClickShowAdInterstitial()
    {
        RequestAdInterstitial();
    }
    private void RequestAdInterstitial()
    {
        if(MODE_TESTING_ADS)
        {
            // Google "Sample Ad Unit": https://developers.google.com/admob/unity/test-ads
            interstitialAd = new InterstitialAd("ca-app-pub-3940256099942544/1033173712");
        }
        else
        {
            interstitialAd = new InterstitialAd(iDAdMobAdInterstitial);
        }
        AdRequest request = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(request); 
    }
}
