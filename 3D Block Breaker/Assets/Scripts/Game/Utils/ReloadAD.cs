using UnityEngine;
using GoogleMobileAds.Api;

public class ReloadAD : MonoBehaviour
{
    private InterstitialAd interstitial;

    [SerializeField] int maxReloads = 5;
    private const string PREF_KEY = "SceneReloadCount";
    private int reloadCount = 0;

    [System.Obsolete]
    private void Start()
    {
        reloadCount = PlayerPrefs.GetInt(PREF_KEY);
        Debug.Log(reloadCount);

        if (reloadCount >= maxReloads)
        {
            PlayerPrefs.SetInt(PREF_KEY, 0);
            Debug.Log("Scene reload count: " + PlayerPrefs.GetInt(PREF_KEY));
            Debug.Log("Scene reload count reset to 0");
            
            MobileAds.Initialize(initstatus => { });
            RequestInterstitial();
            if (interstitial.IsLoaded())
                interstitial.Show();
        }
    }

    [System.Obsolete]
    private void RequestInterstitial()
    {
    #if UNITY_ANDROID
        string adUnitId = "ca-app-pub-9942015316512700/5936328123";
    #elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
    #else
        string adUnitId = "unexpected_platform";
    #endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }
}
