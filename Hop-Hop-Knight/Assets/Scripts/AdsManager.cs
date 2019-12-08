using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;
using UnityEngine.SceneManagement;

public class AdsManager : MonobehaviourSingleton<AdsManager>
{
    private string GameIdAndroid = "3390452";
    private string VideoKey = "video";
    private string RewardedKey = "rewardedVideo";

    public override void Awake()
    {
        base.Awake();
        Advertisement.Initialize(GameIdAndroid, true);
    }

    public void UIWatchAd()
    {
        WatchVideoAd(VideoAdEnded);
    }

    public void UIWatchRewardedAd()
    {
        WatchRewardedVideoAd(VideoRewardedAdEnded);
    }

    public void WatchVideoAd(Action<ShowResult> result)
    {
        ShowOptions so = new ShowOptions();
        so.resultCallback = result;

        if (Advertisement.IsReady(VideoKey))
        {
            Advertisement.Show(VideoKey, so);
            Time.timeScale = 0;
        }
            
        else
            Debug.Log("No carga");
    }

    public void WatchRewardedVideoAd(Action<ShowResult> result)
    {
        ShowOptions so = new ShowOptions();
        so.resultCallback = result;

        if (Advertisement.IsReady(RewardedKey))
            Advertisement.Show(RewardedKey, so);
        else
            Debug.Log("No carga");
    }

    public void VideoAdEnded(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Failed:
                Debug.Log("El Ad fallo");
                break;
            case ShowResult.Finished:
                Debug.Log("El Ad termino");
                Time.timeScale = 1;
                break;
            case ShowResult.Skipped:
                Debug.Log("El Ad se skipeo");
                Time.timeScale = 1;
                break;
        }
    }

    public void VideoRewardedAdEnded(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Failed:
                Debug.Log("El Ad Rewarded fallo");
                break;
            case ShowResult.Finished:
                Debug.Log("El Ad Rewarded termino");
                break;
            case ShowResult.Skipped:
                Debug.Log("El Ad Rewarded se skipeo");
                break;
        }
    }
}