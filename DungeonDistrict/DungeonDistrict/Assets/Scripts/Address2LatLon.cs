using Mapbox.Unity;
using UnityEngine;
using UnityEngine.UI;
using System;
using Mapbox.Geocoding;
using Mapbox.Utils;
using System.Collections.Generic;

public class Address2LatLon : MonoBehaviour
{
    ForwardGeocodeResource _resource;
    bool _hasResponse;
    public bool HasResponse
    {
        get
        {
            return _hasResponse;
        }
    }

    Vector2d _coordinate;
    public Vector2d Coordinate
    {
        get
        {
            return _coordinate;
        }
    }

    public ForwardGeocodeResponse Response { get; private set; }

    List<Dungeon> dungeons = new List<Dungeon>();

    List<Dungeon> dungeonsUpdated = new List<Dungeon>();

    //public event Action<ForwardGeocodeResponse> OnGeocoderResponse = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        dungeons = DungeonHandler.GetDungeonsList();

        //ConvertDungeons();
    }

    void Awake()
    {
        _resource = new ForwardGeocodeResource("");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConvertDungeons()
    {
        if (dungeons.Count > 0)
        {
            dungeonsUpdated.Add(dungeons[0]);

            if (dungeons[0].Lat == -1)
            {
                ConvertAddress(dungeons[0].Address +", "+ dungeons[0].City +", "+ dungeons[0].State);

                dungeons.RemoveAt(0);
            }
            else
            {
                dungeons.RemoveAt(0);

                ConvertDungeons();
            }
        }
        else
            DungeonHandler.SaveDungeons(dungeonsUpdated);
    }

    public void ConvertAddress(string searchString)
    {
        _hasResponse = false;
        if (!string.IsNullOrEmpty(searchString))
        {
            _resource.Query = searchString;
            MapboxAccess.Instance.Geocoder.Geocode(_resource, HandleGeocoderResponse);
        }
    }

    void HandleGeocoderResponse(ForwardGeocodeResponse res)
    {
        _hasResponse = true;
        if (null == res)
        {
            //_inputField.text = "no geocode response";
        }
        else if (null != res.Features && res.Features.Count > 0)
        {
            var center = res.Features[0].Center;
            //_inputField.text = string.Format("{0},{1}", center.x, center.y);
            _coordinate = res.Features[0].Center;
        }

        Response = res;
        //OnGeocoderResponse(res);

        dungeonsUpdated[dungeonsUpdated.Count - 1].Lat = _coordinate.x;
        dungeonsUpdated[dungeonsUpdated.Count - 1].Lon = _coordinate.y;

        ConvertDungeons();
    }
}
