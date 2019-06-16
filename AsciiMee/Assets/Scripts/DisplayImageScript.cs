using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;
using System.Drawing;
using System.IO;

public class DisplayImageScript : MonoBehaviour {
    string resolution = "3";
    //System.IO.FileStream fileStream = System.IO.File.OpenRead(imagePath);
    public UnityEngine.UI.Text txtResults;
    public Texture2D picture;

	// Use this for initialization
	void Start () {
		//GetComponent<Renderer>().material.mainTexture = StaticVariables.Picture;
		//GetComponent<Text>().material.mainTexture = CopyTexture2D(StaticVariables.Picture);

		if(txtResults == null)
			txtResults = GetComponent<Text>();

        //ConvertToAscii (StaticVariables.Picture);

        if (StaticVariables.Picture != null)
            picture = StaticVariables.Picture;//get the picture from camera

        //var path = "assets\\temp_ascii.png";

        //System.IO.Stream filestream = System.IO.File.OpenRead(path);
        //CopyTexture2D(picture);

        //TextureScale.Bilinear(picture, 128, 200);
        //if (picture.width > 300)
        //    picture.Resize(picture.width / 2, picture.height / 2);

        //var newTex = Instantiate(tex);
        //renderer.material.mainTexture = newTex;
        if (picture.width > 300)
            TextureScale.Bilinear(picture, 100, 250);
        //TextureScale.Bilinear(picture, (int)(picture.width / 1.3), (int)(picture.height / 1.3));

        //System.IO.File.WriteAllBytes(path, picture.EncodeToPNG());

        if (picture != null)
        {
            //txtResults.text = ImageToAscii.Converter.ConvertToAscii(System.IO.File.ReadAllBytes(path));

            string pictureString = ImageToAscii.Converter.ConvertToAscii(picture.EncodeToPNG());
            txtResults.text = pictureString;
        }
        //txtResults.text = StaticDust.AsciiArt.ConvertImage(filestream, resolution, "False");
        //txtResults.text = ConvertToAscii(picture);
        //TempStart ();
    }

    private static Bitmap byteArrayToImage(byte[] byteArrayIn)
    {
        MemoryStream ms = new MemoryStream(byteArrayIn);
        Bitmap returnImage = new Bitmap(ms);
        return returnImage;
    }

    public void Update()
	{
//		if (UpdateAnimation)
//		{
//			UpdateAnimationImage();
//		}
//		
//		if (UpdateColors)
//		{
//			UpdateCharacterTexture();
//			UpdateColors = false;
//		}
	}
}
