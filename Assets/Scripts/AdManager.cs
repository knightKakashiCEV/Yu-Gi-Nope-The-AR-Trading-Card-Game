using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener, IUnityAdsInitializationListener
{
    public static AdManager instance;
    [SerializeField] private string android_ID;
    [SerializeField] private string iOS_ID;
    [SerializeField] private bool testMode;
    private string placementID = "Interstitial_";

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
#if UNITY_ANDROID || UNITY_EDITOR || UNITY_STANDALONE_WIN
    Advertisement.Initialize(android_ID, testMode, this);
    placementID += "Android";
#elif UNITY_IOS
    Advertisement.Initialize(iOS_ID, testMode, this);
    placementID += "iOS";
#endif
    }

    public void ShowAd()
    {
        Advertisement.Load(placementID, this);
        
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("El anuncio ha fallado en mostrarse");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("El anuncio se ha iniciado");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("Unity Ads.");

    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("Unity Ads showed completed.");
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization completed.");

    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("Unity Ads initialization failed.");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Advertisement.Show(placementId, this);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log("Unity Ads failed to load.");

    }
}
