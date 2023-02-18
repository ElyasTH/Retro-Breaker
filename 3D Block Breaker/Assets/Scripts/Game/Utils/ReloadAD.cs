using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class ReloadAD : MonoBehaviour
{
    private InterstitialAd interstitial;

    [SerializeField] int maxReloads = 5;
    private const string PREF_KEY = "SceneReloadCount";
    private int reloadCount = 0;

#if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-9942015316512700/5936328123";
#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
  private string _adUnitId = "unused";
#endif
    private InterstitialAd interstitialAd;

    [System.Obsolete]
    private void Start()
    {
        MobileAds.Initialize(initStatus => { });
        reloadCount = PlayerPrefs.GetInt(PREF_KEY);
        Debug.Log(reloadCount);

        if (reloadCount >= maxReloads)
        {
            LoadInterstitialAd();
            PlayerPrefs.SetInt(PREF_KEY, 0);
            Debug.Log("Scene reload count: " + PlayerPrefs.GetInt(PREF_KEY));
            Debug.Log("Scene reload count reset to 0");

            ShowAd();
        }
    }

    [System.Obsolete]
    public void LoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest.Builder()
                .AddKeyword("unity-admob-sample")
                .Build();

        // send the request to load the ad.
        InterstitialAd.Load(_adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                interstitialAd = ad;
            });

    }
    public void ShowAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            interstitialAd.Show();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
        }
    }
}
