using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class LocationARController : MonoBehaviour
{
    public GeofenceManager geofenceManager;
    public ARRaycastManager raycastManager;
    public GameObject treasurePrefab;
    GameObject spawnedTreasure;
    static readonly List<ARRaycastHit> hits = new List<ARRaycastHit>();
    void Update()
    {
        if (geofenceManager == null || raycastManager == null
        || treasurePrefab == null) return;
        if (!geofenceManager.IsInside)
        {
            if (spawnedTreasure != null) Destroy(spawnedTreasure);
            return;
        }
        if (spawnedTreasure != null) return;
        Vector2 center = new Vector2(Screen.width * 0.5f,
        Screen.height * 0.5f);
        if (raycastManager.Raycast(center, hits,
        TrackableType.PlaneWithinPolygon))
        {
            spawnedTreasure = Instantiate(treasurePrefab,
            hits[0].pose.position, hits[0].pose.rotation);
        }
    }
}