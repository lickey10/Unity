namespace Mapbox.Unity.Utilities
{
	using UnityEngine;
	using UnityEngine.UI;

	[RequireComponent(typeof(Button))]
	public class CancelDungeonAddButton : MonoBehaviour
	{
        public MapController mapController;
        Button theButton;

  //      [SerializeField]
		//bool _booleanValue;

		protected virtual void Awake()
		{
            theButton = GetComponent<Button>();
            theButton.onClick.AddListener(buttonClicked);
		}

		void buttonClicked()
		{
            mapController.CancelDungeonAdd();
		}
	}
}