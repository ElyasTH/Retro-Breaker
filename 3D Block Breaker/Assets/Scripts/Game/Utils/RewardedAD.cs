using GoogleMobileAds.Api;
using UnityEngine;
using System;
public class RewardedAD : MonoBehaviour
{
    private RewardedAd rewardedAd;
    public GameObject player;
    private BallMovement ball;
    public GameObject gameOverCanvas;
    public GameHandler gameHandler;
    public AudioSource music;
    private void Start()
    {
        MobileAds.Initialize(initialize => { });
    }

    [Obsolete]
    public void ShowRewardedAD() 
    {
        RequestRewardedVideo();
        if (rewardedAd.IsLoaded())
            rewardedAd.Show();
    }

    [Obsolete]
    public void RequestRewardedVideo()
    {
        string adUnitId;
#if UNITY_ANDROID
        adUnitId = "ca-app-pub-9942015316512700/8760450944";
#elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            adUnitId = "unexpected_platform";
#endif

        this.rewardedAd = new RewardedAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.ToString());
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        gameHandler.addLife();
        gameHandler.addLife();
        gameHandler.ball = this.ball.gameObject;
        player.SetActive(true);
        ball.gameObject.SetActive(true);
        ball.Lock();
        gameOverCanvas.SetActive(false);
        BlockDestroyerScript.init();
        Time.timeScale = 1f;
        music.pitch = 1f;
    }

    public void SetBall(BallMovement ball){
        this.ball = ball;
    }
}
