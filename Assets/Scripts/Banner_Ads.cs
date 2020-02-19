using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Monetization;

public class Banner_Ads : MonoBehaviour
{
    public string gameId = "1234567";
    public string placementIdBanner = "bannerPlacement";
    public string placementIdFull = "fullPlacement";
    public bool testMode = true;

    void Start()
    {
        // Initialize the SDK if you haven't already done so:
        Advertisement.Initialize(gameId, testMode);
        StartCoroutine(ShowBannerWhenReady());
    }

    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(placementIdBanner))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(placementIdBanner);
        ShowBannerWhenReady();
    }

    public void Ads() {
        StartCoroutine(ShowAdWhenReady());
    }

    private IEnumerator ShowAdWhenReady()
    {
        while (!Advertisement.IsReady(placementIdFull))
        {
            yield return new WaitForSeconds(0.25f);
        }

        Advertisement.Show(placementIdFull);
    }
}
