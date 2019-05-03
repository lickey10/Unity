namespace Mapbox.Unity.Utilities
{
	using UnityEngine;
	using UnityEngine.UI;

	[RequireComponent(typeof(Button))]
	public class SaveDungeonButton : MonoBehaviour
	{
        private MapController mapController;
        private DungeonListController DungeonListControllerObject;
        private GameObject scripts;
        public InputField Name;
        public Dropdown Rating;
        public Toggle Favorite;
        string Lat = "-1";
        string Lon = "-1";

  //      [SerializeField]
		//bool _booleanValue;

        // Start is called before the first frame update
        void Start()
        {
            scripts = GameObject.FindGameObjectWithTag("scripts");
            DungeonListControllerObject = scripts.GetComponent< DungeonListController>();
            mapController = GameObject.FindGameObjectWithTag("map").GetComponent<MapController>();
        }

        protected virtual void Awake()
		{
			GetComponent<Button>().onClick.AddListener(buttonClicked);
		}

		void buttonClicked()
		{
            Dungeon newDungeon = new Dungeon();
            newDungeon.Name = Name.text;
            newDungeon.Lat = double.Parse(Lat);
            newDungeon.Lon = double.Parse(Lon);
            newDungeon.Rating = int.Parse(Rating.options[Rating.value].text);
            newDungeon.Favorite = Favorite.isOn;

            mapController.AddDungeon(newDungeon);

            DungeonListControllerObject.AddButton(newDungeon);

			//MapboxAccess.Instance.SetLocationCollectionState(_booleanValue);
			//PlayerPrefs.Save();
		}
	}
}