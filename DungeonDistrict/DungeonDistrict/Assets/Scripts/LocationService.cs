using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationService : MonoBehaviour
{
    public float Latitude = 0;
    public float Longitude = 0;

    private void Start()
    {
        
    }

    public IEnumerator GetLocation(System.Action<int> result)
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            result(-1);

            yield break;
        }

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");

            Latitude = 0;
            Longitude = 0;

            result(-1);

            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");

            Latitude = 0;
            Longitude = 0;

            result(-1);

            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);

            Latitude = Input.location.lastData.latitude;
            Longitude = Input.location.lastData.longitude;

            result(1);
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }
}
