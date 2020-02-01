using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banner_Ads : MonoBehaviour
{
    public string gameId = "1234567";
    public string placementId = "bannerPlacement";
    public bool testMode = true;

    void Start()
    {
        // Initialize the SDK if you haven't already done so:
        UnityEngine.Advertisements.Advertisement.Initialize(gameId, testMode);
        StartCoroutine(ShowBannerWhenReady());
    }

    IEnumerator ShowBannerWhenReady()
    {
        while (!UnityEngine.Advertisements.Advertisement.IsReady(placementId))
        {
            yield return new WaitForSeconds(0.5f);
        }
        UnityEngine.Advertisements.Advertisement.Banner.Show(placementId);
        ShowBannerWhenReady();
    }
}
