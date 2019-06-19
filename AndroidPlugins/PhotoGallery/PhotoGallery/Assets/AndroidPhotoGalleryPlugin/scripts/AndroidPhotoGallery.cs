using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class AndroidPhotoGallery : MonoBehaviour {
	private AndroidPhotoGalleryHelperClass androidPhotoGalleryHelperClass;

	// Use this for initialization
	void Start () {
		androidPhotoGalleryHelperClass = AndroidPhotoGalleryHelperClass.Instance;
	}

	public void Open()
	{
		androidPhotoGalleryHelperClass.Open();
	}
}

