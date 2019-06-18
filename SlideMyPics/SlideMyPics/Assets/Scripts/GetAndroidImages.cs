using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using LukeWaffel.AndroidGallery;

public class GetAndroidImages : MonoBehaviour
{
    string imagePath = "";

    [Header("Refrences")]
    public Image image;
    public ST_PuzzleDisplay Puzzle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        //NativeToolkit.OnImagePicked += NativeToolkit_OnImagePicked;
    }

    void OnDisable()
    {
        //NativeToolkit.OnImagePicked -= NativeToolkit_OnImagePicked;
    }

    private void NativeToolkit_OnImagePicked(Texture2D img, string path)
    {
        imagePath = path;
        //console.text += "\nImage picked at: " + imagePath;

        ST_PuzzleDisplay puzzle = FindObjectOfType<ST_PuzzleDisplay>();
        puzzle.SetImage(img);
        puzzle.ResetGame();

        Destroy(img);
    }

    public void GetImages()
{
        AndroidGallery.Instance.OpenGallery(ImageLoaded);

        //NativeToolkit.PickImage();



        //NativeGallery.Permission getFromGalleryPermission;

        // This operation is asynchronous! After user selects an image/video or cancels the operation, the callback is called (on main thread)
        // MediaPickCallback takes a string parameter which stores the path of the selected image/video, or null if nothing is selected
        // MediaPickMultipleCallback takes a string[] parameter which stores the path(s) of the selected image(s)/video(s), or null if nothing is selected
        // title: determines the title of the image picker dialog on Android. Has no effect on iOS
        // mime: filters the available images/videos on Android. For example, to request a JPEG image from the user, mime can be set as "image/jpeg". Setting multiple mime types is not possible (in that case, you should leave mime as is). On iOS, selected images will always be in PNG format and thus, this parameter has no effect on iOS
        // maxSize: determines the maximum size of the returned image in pixels on iOS. A larger image will be down-scaled for better performance. If untouched, its value will be set to SystemInfo.maxTextureSize. Has no effect on Android
        //getFromGalleryPermission = NativeGallery.GetImageFromGallery(MediaPickCallback callback, string title = "", string mime = "image/*", int maxSize = -1);
        //NativeGallery.Permission NativeGallery.GetVideoFromGallery(MediaPickCallback callback, string title = "", string mime = "video/*");
        //NativeGallery.Permission NativeGallery.GetImagesFromGallery(MediaPickMultipleCallback callback, string title = "", string mime = "image/*", int maxSize = -1);
        //NativeGallery.Permission NativeGallery.GetVideosFromGallery(MediaPickMultipleCallback callback, string title = "", string mime = "video/*");

        //// Returns true if selecting multiple images/videos from Gallery/Photos is possible on this device (only available on Android 18 and later; iOS not supported)
        //bool CanSelectMultipleFiles = NativeGallery.CanSelectMultipleFilesFromGallery();

        //// Returns true if the user is currently picking media from Gallery/Photos. In that case, another GetImageFromGallery or GetVideoFromGallery request will simply be ignored
        //bool NativeGallery.IsMediaPickerBusy();
    }

    //This is the callback function we created
    public void ImageLoaded()
    {

        //You can put anything in the callback function. You can either just grab the image, or tell your other scripts the custom image is available

        Debug.Log("The image has succesfully loaded!");
        //image.sprite = AndroidGallery.Instance.GetSprite();

        //if (image.sprite.rect.width != image.sprite.texture.width)
        //{
        //Texture2D newText = new Texture2D((int)image.sprite.rect.width, (int)image.sprite.rect.height);
        //Color[] newColors = image.sprite.texture.GetPixels((int)image.sprite.textureRect.x,
        //                                             (int)image.sprite.textureRect.y,
        //                                             (int)image.sprite.textureRect.width,
        //                                             (int)image.sprite.textureRect.height);
        //newText.SetPixels(newColors);
        //newText.Apply();
        //    return newText;
        //}
        //else
        //    return sprite.texture;

        //Sprite newImage = AndroidGallery.Instance.GetSprite();
        //frame.sprite = Sprite.Create(MyTexture, new Rect(0, 0, MyTexture.width, MyTexture.height), new Vector2(MyTexture.width / 2, MyTexture.height / 2));

        Puzzle.PuzzleImages = new Texture[1];
        
        //Puzzle.PuzzleImages[0] = AndroidGallery.Instance.GetTexture();
        Puzzle.ResetGame(Puzzle.AddPuzzleImage(AndroidGallery.Instance.GetTexture()));
    }
}
