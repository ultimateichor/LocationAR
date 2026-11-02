using System.Buffers.Text;
using UnityEngine;
public class GeofenceManager : MonoBehaviour
{
    public LocationManager locationManager;
    public double targetLatitude = 33.37713;
    public double targetLongitude = -111.97558;
    public float activationRadius = 50f;
    public bool IsInside { get; private set; }
    void Update()
    {
        if (locationManager == null || !locationManager.IsLocationReady)
        {
            IsInside = false;
            return;
        }
        double distance = DistanceMeters(
        locationManager.Latitude, locationManager.Longitude,
        targetLatitude, targetLongitude);
        IsInside = distance <= activationRadius;
    }
    double DistanceMeters(double lat1, double lon1,
    double lat2, double lon2)
    {
        double r = System.Math.PI / 180.0;
        double dLat = (lat2 - lat1) * r;
        double dLon = (lon2 - lon1) * r;
        double a = System.Math.Pow(System.Math.Sin(dLat / 2), 2)
        + System.Math.Cos(lat1 * r) * System.Math.Cos(lat2 * r)
        * System.Math.Pow(System.Math.Sin(dLon / 2), 2);
        
    return 6371000.0 * 2.0 * System.Math.Atan2(
    System.Math.Sqrt(a), System.Math.Sqrt(1 - a));
    }
}