using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Take_Screenshot : MonoBehaviour
{
    bool takeSS;

    Camera cam;

    int SScount;

    [SerializeField] RawImage preview;
    [SerializeField] Show_Preview show;

    public AudioSource click;
    public Banner_Ads ads;

    private void Awake()
    {
        cam = gameObject.GetComponent<Camera>();
    }

    #region AndroidStuff
#if PLATFORM_ANDROID
    protected const string MEDIA_STORE_IMAGE_MEDIA = "android.provider.MediaStore$Images$Media";

    protected static AndroidJavaObject m_Activity;

    protected static string SaveImageToGallery(Texture2D a_Texture, string a_Title, string a_Description)
    {
        using (AndroidJavaClass mediaClass = new AndroidJavaClass(MEDIA_STORE_IMAGE_MEDIA))
        {
            using (AndroidJavaObject contentResolver = Activity.Call<AndroidJavaObject>("getContentResolver"))
            {
                AndroidJavaObject image = Texture2DToAndroidBitmap(a_Texture);
                return mediaClass.CallStatic<string>("insertImage", contentResolver, image, a_Title, a_Description);
            }
        }
    }

    protected static AndroidJavaObject Texture2DToAndroidBitmap(Texture2D a_Texture)
    {
        byte[] encodedTexture = a_Texture.EncodeToPNG();
        using (AndroidJavaClass bitmapFactory = new AndroidJavaClass("android.graphics.BitmapFactory"))
        {
            return bitmapFactory.CallStatic<AndroidJavaObject>("decodeByteArray", encodedTexture, 0, encodedTexture.Length);
        }
    }

    protected static AndroidJavaObject Activity
    {
        get
        {
            if (m_Activity == null)
            {
                AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                m_Activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            }
            return m_Activity;
        }
    }
#endif
    #endregion

    private void OnPostRender()
    {
        if (takeSS) {
            takeSS = false;
            
            string fileName = "Screenshot" + "-"+ System.DateTime.Now.Day + "-" + System.DateTime.Now.Hour +"-"+ System.DateTime.Now.Minute +"-" +
                              System.DateTime.Now.Second + ".png";
            
            string defaultLocation = Application.persistentDataPath + "/" + fileName;
            
            //string defaultLocation = Application.dataPath + "/" + fileName;

            Preview_Behaviour.Instance.ssName = defaultLocation;
            
            //path.text = defaultLocation;

            #region Screenshot itself
            RenderTexture renderTexture = cam.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            

            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();
            Preview_Behaviour.Instance.ss = byteArray;
            System.IO.File.WriteAllBytes(defaultLocation, byteArray);

            renderResult.LoadImage(byteArray);

            preview.texture = renderResult;
            preview.mainTexture.IncrementUpdateCount();

            string path = SaveImageToGallery(renderResult, fileName, defaultLocation);

            VoxelBusterManager.Instance.texture = renderResult;

            RenderTexture.ReleaseTemporary(renderTexture);
            cam.targetTexture = null;
            #endregion
            show.Activate();
            _ShowAndroidToastMessage("Enjoy your new Wallpaper!");
            click.Play();
            ads.Ads();
        }
    }

    public void TakeScreenshot() {
        _ShowAndroidToastMessage("Capturing cutenesss");
        cam.targetTexture = RenderTexture.GetTemporary(Screen.width, Screen.height, 16);
        takeSS = true;
    }

    private void _ShowAndroidToastMessage(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
                toastObject.Call("show");
            }));
        }
    }

}
