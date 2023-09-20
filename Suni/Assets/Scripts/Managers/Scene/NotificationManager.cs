using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
/// <summary>
/// No se puede verificar su funcionamiento sin un celular
/// </summary>
public class NotificationManager : MonoBehaviour
{
    public float intervalInSeconds = 60f;
    private float timeSinceLastNotification = 0f;
    [SerializeField] private List<string> messages = new List<string>();

    private void Start()
    {
        LoadMessages();
    }

    private void LoadMessages()
    {
        TextAsset[] messageAssets = Resources.LoadAll<TextAsset>("Message");

        foreach (TextAsset textAsset in messageAssets)
        {
            messages.Add(textAsset.text);
        }
    }

    private void Update()
    {
        timeSinceLastNotification += Time.deltaTime;

        if (timeSinceLastNotification >= intervalInSeconds)
        {
            timeSinceLastNotification = 0f;

            string randomMessage = GetRandomMessage();

            ScheduleNotification("Título de la notificación", randomMessage, 10);
        }
    }

    private string GetRandomMessage()
    {
        if (messages.Count == 0)
        {
            Debug.LogError("No se encontraron mensajes en la carpeta Resources.");
            return "";
        }

        int randomIndex = Random.Range(0, messages.Count);

        string randomMessage = messages[randomIndex];
        Debug.Log("Mensaje seleccionado: " + randomMessage);

        return messages[randomIndex];
    }

    public void ScheduleNotification(string title, string message, int secondsDelay)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaClass notificationManager = new AndroidJavaClass("android.app.NotificationManager");
        AndroidJavaClass notificationBuilder = new AndroidJavaClass("android.app.Notification$Builder");
        AndroidJavaClass pendingIntent = new AndroidJavaClass("android.app.PendingIntent");
        AndroidJavaClass intent = new AndroidJavaClass("android.content.Intent");

        intent.CallStatic<AndroidJavaObject>("setClassName", currentActivity, currentActivity.Call<string>("getPackageName"), currentActivity.Call<string>("getPackageName") + ".MainActivity");
        intent.Call<AndroidJavaObject>("setAction", "com.unity3d.player.UnityBroadcastReceiver.ACTION_NOTIFICATION");
        intent.Call<AndroidJavaObject>("setFlags", 0x00008000);

        AndroidJavaObject contentIntent = pendingIntent.CallStatic<AndroidJavaObject>("getActivity", currentActivity, 0, intent.Call<AndroidJavaObject>("addFlags", 0x8000), 0x00000004);

        AndroidJavaObject builder = new AndroidJavaObject("android.app.Notification$Builder", currentActivity)

            .Call<AndroidJavaObject>("setContentTitle", title)
            .Call<AndroidJavaObject>("setContentText", message)
            .Call<AndroidJavaObject>("setContentIntent", contentIntent);

        AndroidJavaObject notificationManagerObj = currentActivity.Call<AndroidJavaObject>("getSystemService", "notification");
        notificationManagerObj.Call("notify", 0, builder.Call<AndroidJavaObject>("build"));
    }
}