using UnityEngine;

public class Ads : MonoBehaviour
{
    public static Ads Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(Instance);
    }

    void Start()
    {
        Gley.MobileAds.API.Initialize();
    }

    public void ShowAds()
    {
        Gley.MobileAds.API.ShowInterstitial();
    }
}
