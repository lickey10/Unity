namespace Mapbox.Examples
{
	using UnityEngine;
	using Mapbox.Utils;
	using Mapbox.Unity.Map;
	using Mapbox.Unity.MeshGeneration.Factories;
	using Mapbox.Unity.Utilities;
	using System.Collections.Generic;
    using System;
    using System.Linq;
    using System.Xml;

    public class SpawnOnMap : MonoBehaviour
	{
		[SerializeField]
		AbstractMap _map;

		[SerializeField]
		[Geocode]
		string[] _locationStrings;
		Vector2d[] _locations;

		[SerializeField]
		float _spawnScale = 100f;

		[SerializeField]
		GameObject _markerPrefab;

        [SerializeField]
        List<Dungeon> dungeons = new List<Dungeon>();

        List<GameObject> _spawnedObjects = new List<GameObject>();

		void Start()
		{
            RefreshLocations();
        }

		private void Update()
		{
            if (_spawnedObjects != null)
            {
                int count = _spawnedObjects.Count;
                for (int i = 0; i < count; i++)
                {
                    var spawnedObject = _spawnedObjects[i];
                    //var location = new Vector2d(double.Parse(_spawnedObjects[i].name.Split(',')[0]), double.Parse(_spawnedObjects[i].name.Split(',')[1])); 
                    var location = _locations[i];
                    spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
                    spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
                }
            }
		}

        public void RefreshLocations()
        {
            if (dungeons.Count > 0)
            {
                _locations = new Vector2d[dungeons.Count];

                foreach (GameObject gameObject in _spawnedObjects)
                    Destroy(gameObject);

                _spawnedObjects.Clear();

                int i = 0;
                
                //for (int i = 0; i < dungeons.Count; i++)
                foreach(Dungeon dungeon in dungeons)
                {
                    if (_spawnedObjects == null || !_spawnedObjects.Where(x => x.transform.localPosition == _map.GeoToWorldPosition(_locations[i], true)).Any() || _spawnedObjects.Count == 0)
                    {
                        //var locationString = dungeons[i].Lat.ToString() +","+ dungeons[i].Lon.ToString();
                        var locationString = dungeon.Lat.ToString() + "," + dungeon.Lon.ToString();
                        _locations[i] = Conversions.StringToLatLon(locationString);

                        var instance = Instantiate(_markerPrefab);
                        PoiLabelTextSetter text = (PoiLabelTextSetter)instance.GetComponent("PoiLabelTextSetter");

                        if (text)
                        {
                            Dictionary<string, object> dungeonDict = new Dictionary<string, object>();
                            dungeonDict.Add("name", dungeon.Name);

                            text.Set(dungeonDict);
                        }

                        //instance.name = dungeons[i].Lat + "," + dungeons[i].Lon;
                        instance.name = dungeon.Lat + "," + dungeon.Lon;
                        //instance.tag = "Dungeon";
                        instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
                        instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
                        _spawnedObjects.Add(instance);

                        i++;
                    }
                }

                DungeonHandler.SaveDungeons(dungeons);
            }
        }

        public void RefreshDungeons(List<Dungeon> lDungeons)
        {
            foreach(GameObject dungeon in _spawnedObjects)
                Destroy(dungeon);

            _spawnedObjects.Clear();
            dungeons.Clear();

            //foreach (XmlElement dungeon in xDungeons.DocumentElement)
            //{
            //    Dungeon newDungeon = new Dungeon();
            //    newDungeon.Name = dungeon.SelectSingleNode("Name").InnerText;
            //    newDungeon.Lat = double.Parse(dungeon.SelectSingleNode("Lat").InnerText);
            //    newDungeon.Lon = double.Parse(dungeon.SelectSingleNode("Lon").InnerText);

            //    dungeons.Add(newDungeon);
            //}

            foreach (Dungeon dungeon in lDungeons)
                dungeons.Add(dungeon);

            RefreshLocations();
        }

        public void AddLocation(float lat, float lon)
        {
            Array.Resize(ref _locationStrings, _locationStrings.Length + 1);
            _locationStrings[_locationStrings.Length - 1] = lat.ToString() +","+ lon.ToString();
        }

        public void AddLocation(string lat, string lon)
        {
            Array.Resize(ref _locationStrings, _locationStrings.Length + 1);
            _locationStrings[_locationStrings.Length - 1] = lat + "," + lon;
        }

        public void AddDungeon(Vector2d location)
        {
            //Array.Resize(ref _locationStrings, _locationStrings.Length + 1);
            //_locationStrings[_locationStrings.Length - 1] = location.x.ToString() + "," + location.y.ToString();

            if (!dungeons.Where(x => x.Lat == location.x).Where(x => x.Lon == location.y).Any())
            {
                Dungeon newDungeon = new Dungeon();
                newDungeon.Lat = location.x;
                newDungeon.Lon = location.y;
                newDungeon.Name = "Dungeon " + (dungeons.Count + 1).ToString();
                //newDungeon.tag = "Dungeon";

                dungeons.Add(newDungeon);
            }
        }

        public void AddDungeon(Dungeon NewDungeon)
        {
            //Array.Resize(ref _locationStrings, _locationStrings.Length + 1);
            //_locationStrings[_locationStrings.Length - 1] = location.x.ToString() + "," + location.y.ToString();

            if (!dungeons.Where(x => x.Lat == NewDungeon.Lat).Where(x => x.Lon == NewDungeon.Lon).Any())
            {
                //NewDungeon.tag = "Dungeon";

                dungeons.Add(NewDungeon);
            }
        }

        public void UpdateDungeon(Dungeon updatedDungeon, double PrevLat, double PrevLon)
        {
            if (!dungeons.Contains(updatedDungeon))
            {
                Dungeon prevDungeon = dungeons.Where(x => x.Lat == PrevLat).Where(x => x.Lon == PrevLon).FirstOrDefault();

                if (prevDungeon != null)
                {
                    dungeons.Remove(prevDungeon);

                    //updatedDungeon.tag = "Dungeon";

                    dungeons.Add(updatedDungeon);
                }
            }
        }
    }
}