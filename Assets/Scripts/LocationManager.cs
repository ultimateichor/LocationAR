using System.Buffers.Text;
using System.Collections;
using UnityEngine;
public class LocationManager : MonoBehaviour
{
    public float Latitude { get; private set; }
    public float Longitude { get; private set; }
    public bool IsLocationReady { get; private set; }
    IEnumerator Start()
    {
#if UNITY_ANDROID
        string permission = UnityEngine.Android.Permission.FineLocation;
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(permission))
            UnityEngine.Android.Permission.RequestUserPermission(permission);
        float permissionWait = 15f;
        while (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(permission)
        && permissionWait > 0f)
        {
            permissionWait -= Time.deltaTime;
            yield return null;
        }
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(permission))
        {
            Debug.LogWarning("Precise location permission not granted.");
            yield break;
        }
#endif
        if (!Input.location.isEnabledByUser)
        {
            Debug.LogWarning("Location services are disabled.");
            yield break;
        }
        Input.location.Start(10f, 5f);
        int seconds = 20;
        while (Input.location.status == LocationServiceStatus.Initializing
        && seconds > 0)
        {
            yield return new WaitForSeconds(1f);
            seconds--;
        }
        if (Input.location.status != LocationServiceStatus.Running)
            // GDT - 220 | Topic 4 - Location - Based AR 3
    {
            Debug.LogWarning("Could not start GPS.");
            yield break;
        }
        while (Input.location.status == LocationServiceStatus.Running)
        {
            Latitude = Input.location.lastData.latitude;
            Longitude = Input.location.lastData.longitude;
            IsLocationReady = true;
            Debug.Log("GPS: " + Latitude + ", " + Longitude);
            yield return new WaitForSeconds(1f);
        }
        IsLocationReady = false;
    }
    void OnDisable()
    {
        IsLocationReady = false;
        if (Input.location.status == LocationServiceStatus.Running)
            Input.location.Stop();
    }
}