using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//NOTE: we're using LukeWaffel.AndroidGallery, without this it won't work
using LukeWaffel.AndroidGallery;

public class DemoScript : MonoBehaviour {

	[Header ("Refrences")]
	public Image frame;
    public Sprite MyImage;
    public Texture2D MyTexture;


	// Use this for initialization
	void Start () {
        if (MyTexture != null)
        {
            //frame.sprite = MyImage;

            //var bytes = MyTexture.EncodeToPNG();
            //Sprite sprite = new Sprite();
            //sprite = Sprite.Create(MyTexture, new Rect(0, 0, MyTexture.width, MyTexture.height), new Vector2(MyTexture.width / 2, MyTexture.height / 2));
            frame.sprite = Sprite.Create(MyTexture, new Rect(0, 0, MyTexture.width, MyTexture.height), new Vector2(MyTexture.width / 2, MyTexture.height / 2));
            //image.GetComponent<Image>().overrideSprite = sprite;


            //if (frame.sprite.rect.width != frame.sprite.texture.width)
            //{
            //    Texture2D newText = new Texture2D((int)frame.sprite.rect.width, (int)frame.sprite.rect.height);
            //    Color[] newColors = frame.sprite.texture.GetPixels((int)frame.sprite.textureRect.x,
            //                                                 (int)frame.sprite.textureRect.y,
            //                                                 (int)frame.sprite.textureRect.width,
            //                                                 (int)frame.sprite.textureRect.height);
            //    newText.SetPixels(newColors);
            //    newText.Apply();




            //    //return newText;
            //}
        }

        //Renderer renderer = frame.GetComponent<Renderer>();
        //renderer.SetTexture(MyTexture);


    }
	
	// Update is called once per frame
	void Update () {
		
	}

	//This function is called by the Button
	public void OpenGalleryButton(){

		//NOTE: we're using LukeWaffel.AndroidGallery (As seen at the top of this script), without this it won't work

		//This line of code opens the Android image picker, the parameter is a callback function the AndroidGallery script will call when the image has finished loading
		AndroidGallery.Instance.OpenGallery (ImageLoaded);

	}

	//This is the callback function we created
	public void ImageLoaded(){

		//You can put anything in the callback function. You can either just grab the image, or tell your other scripts the custom image is available

		Debug.Log("The image has succesfully loaded!");
        //frame.sprite = AndroidGallery.Instance.GetSprite();
        MyTexture = (Texture2D)AndroidGallery.Instance.GetTexture();
        frame.sprite = Sprite.Create(MyTexture, new Rect(0, 0, MyTexture.width, MyTexture.height), new Vector2(MyTexture.width / 2, MyTexture.height / 2));
    }

	//This function exits the app
	public void Exit(){
		Application.Quit ();
	}
}
