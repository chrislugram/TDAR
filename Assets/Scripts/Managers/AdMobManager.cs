using UnityEngine;
using System.Collections;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class AdMobManager {

    #region STATIC_ENUM_CONSTANTS
    #endregion

    #region FIELDS
    private BannerView bannerView;
    private bool showingBanner;

    private static AdMobManager instance = null;
    #endregion

    #region ACCESSORS
    public static AdMobManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AdMobManager();
            }

            return instance;
        }
    }
    #endregion

    #region METHODS_CONSTRUCTORS
    private AdMobManager() {}
    #endregion

    #region METHODS_CUSTOM
    public void Init()
    {
        showingBanner = false;
    }

    public void ShowBanner()
    {
#if UNITY_ANDROID
        if (!showingBanner)
        {
            bannerView = new BannerView(AppGooglePlayIDs.ADMOB_ID, AdSize.SmartBanner, AdPosition.Top);
            bannerView.LoadAd(CreateAdRequest());
            bannerView.Show();
            showingBanner = true;
        }
#endif
    }

    public void HideBanner()
    {
#if UNITY_ANDROID
        if (showingBanner)
        {
            bannerView.Hide();
            showingBanner = false;
        }
#endif
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().AddTestDevice(AppGooglePlayIDs.PHONE_TEST_ID)
            .AddKeyword("tower").AddKeyword("defense").AddKeyword("strategic").AddKeyword("game")
            .Build();
    }
    #endregion

    #region EVENTS
    #endregion
}
