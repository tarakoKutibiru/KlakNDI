using UnityEngine;
using System.Collections.Generic;

public class WifiManager
{
#if UNITY_ANDROID && !UNITY_EDITOR
    AndroidJavaObject nsdManager = null;
#endif

    private static WifiManager instance = new WifiManager();

    public static WifiManager GetInstance()
    {
        return instance;
    }

    private WifiManager() { }

    public void SetupNetwork()
    {
        // The NDI SDK for Android uses NsdManager to search for NDI video sources on the local network.
        // So we need to create and maintain an instance of NSDManager before performing Find, Send and Recv operations.
    #if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity"))
        {
            using (AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext"))
            {
                using (AndroidJavaObject nsdManager = context.Call<AndroidJavaObject>("getSystemService", "servicediscovery"))
                {
                    this.nsdManager = nsdManager;
                }
            }
        }
    #endif
    }
}