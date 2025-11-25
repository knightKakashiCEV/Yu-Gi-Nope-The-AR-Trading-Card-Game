using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsShowListener, IUnityAdsInitializationListener
{
    public static AdManager instance;
    [SerializeField] private string android_ID;
    [SerializeField] private string iOS_ID;
    [SerializeField] private bool testMode;

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
#elif UNITY_IOS
    Advertisement.Initialize(iOS_ID, testMode, this);
#endif
    }

    public void ShowAd()
    {
        Advertisement.Show("Interstitial");
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
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        throw new System.NotImplementedException();
    }

    public void OnInitializationComplete()
    {
        throw new System.NotImplementedException();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        throw new System.NotImplementedException();
    }
}
