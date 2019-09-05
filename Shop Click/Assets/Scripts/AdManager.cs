using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using TMPro;

// Full code from Google to register your device's device ID as a test device:
// https://developers.google.com/admob/unity/banner#create_a_bannerview

public class AdManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tMPStatusAdRewardedVideo; //, tMPAdRewardedVideoClickThrough, tMPAdRewardedVideoRewarded;
    private int adRewardedVideoClickThrough, adRewardedVideoRewarded;

    // Re-defining: If the user is already opting-in to view the ad, then that is probably a click-through already,
    //   especially if they must watch the full ad to get the reward.
    // We could say an impression is when the user was presented with the option.
    // We could say a click-through is when the user satisfied what was needed for the reward.

    // 1. Rewarded videos will only get a reward when watched in full, regardless of clicking through to App store.
    // 2. Users must opt in for ads, so technically they must watch the entire ad and clicking through or closing afterwards is a "click-through"

    // Closing prematurely is not counted, and it should be disabled when this is in production (if you opt-in for the ad, you must watch it through)
    // Click-through counts as completely watching the video then closing or clicking through to app store

    // Google Test Device ID
    private string iDDeviceTest = "2077ef9a63d2b398840261c8221a0c9b";

    // Google Sample Ad Unit IDs
    //private string iDAdBanner = "ca-app-pub-3940256099942544/6300978111";
    //private string iDAdInterstitial = "ca-app-pub-3940256099942544/1033173712";
    private string iDAdRewardedVideo = "ca-app-pub-3940256099942544/5224354917";

    private RewardBasedVideoAd aDRewardedVideo;

    //private BannerView bannerView;
    public void Start()
    {
        adRewardedVideoClickThrough = adRewardedVideoRewarded = 0;
        InitAdRewardedVideo();
        RequestAdRewardedVideo();
    }
    public void Update()
    {
        // Uncomment when building to device, this floods the console during Play in IDE
        //UpdateStatusAdRewardedVideo();



        // UpdateStats();
    }


    private void UpdateStatusAdRewardedVideo(){
        if(aDRewardedVideo.IsLoaded()){
            tMPStatusAdRewardedVideo.text = "Bonus\nReady";
        }else{
            tMPStatusAdRewardedVideo.text = "Not\nReady";
        }
    }
    // public void UpdateStats()
    // {
    //     tMPAdRewardedVideoClickThrough.text = "Click-Through: " + adRewardedVideoClickThrough.ToString();
    //     tMPAdRewardedVideoRewarded.text     = "Reward: " + adRewardedVideoRewarded.ToString();
    // }

    private void InitAdRewardedVideo() // This only has to happen once at Start()
    {
        aDRewardedVideo                   = RewardBasedVideoAd.Instance;
        aDRewardedVideo.OnAdLoaded       += HandleRewardBasedVideoLoaded; // Called when an ad request has successfully loaded.
        aDRewardedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad; // Called when an ad request failed to load.
        aDRewardedVideo.OnAdOpening      += HandleRewardBasedVideoOpened; // Called when an ad is shown.
        aDRewardedVideo.OnAdStarted      += HandleRewardBasedVideoStarted; // Called when the ad starts to play.
        aDRewardedVideo.OnAdRewarded     += HandleRewardBasedVideoRewarded; // Called when the user should be rewarded for watching a video.
        aDRewardedVideo.OnAdClosed       += HandleRewardBasedVideoClosed; // Called when the ad is closed.
    }
    private void RequestAdRewardedVideo()
    {        
        aDRewardedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication; // Called when the ad click caused the user to leave the application.
        AdRequest aDRequest = new AdRequest.Builder().AddTestDevice(iDDeviceTest).Build();
        aDRewardedVideo.LoadAd(aDRequest, iDAdRewardedVideo);
    }
    public void OnClickDisplayAdRewardedVideo(){ if(aDRewardedVideo.IsLoaded()){ adRewardedVideoClickThrough +=1; aDRewardedVideo.Show(); } }
    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args){ }
    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args){ RequestAdRewardedVideo(); }
    public void HandleRewardBasedVideoOpened(object sender, EventArgs args){ }
    public void HandleRewardBasedVideoStarted(object sender, EventArgs args){ }
    public void HandleRewardBasedVideoClosed(object sender, EventArgs args){ RequestAdRewardedVideo(); }
    public void HandleRewardBasedVideoRewarded(object sender, Reward args){ adRewardedVideoRewarded += 1; Global.instance.GetStats()["Gems"].IncrementAmount(); string type = args.Type; double amount = args.Amount; }
    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args){ } // Warning this happens multiple times per click-through

}