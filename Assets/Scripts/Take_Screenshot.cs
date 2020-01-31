using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Take_Screenshot : MonoBehaviour
{
    bool takeSS;

    Camera cam;

    int SScount;

    [SerializeField] TextMeshProUGUI path;

    private void Awake()
    {
        cam = gameObject.GetComponent<Camera>();
    }

    private void OnPostRender()
    {
        if (takeSS) {
            
            string fileName = "Screenshot" + SScount + ".png";
            string folderLocation = "/storage/emulated/0/CatWall/";
            string ssLocation = folderLocation + fileName;
            string defaultLocation = Application.persistentDataPath + "/" + fileName;

            if (!System.IO.Directory.Exists(folderLocation))
            {
                System.IO.Directory.CreateDirectory(folderLocation);
            }

            #region Screenshot itself
            RenderTexture renderTexture = cam.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);

            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(defaultLocation, byteArray);
            SScount++;
            RenderTexture.ReleaseTemporary(renderTexture);
            cam.targetTexture = null;
            #endregion

            //MOVE THE SCREENSHOT WHERE WE WANT IT TO BE STORED
            System.IO.File.Move(defaultLocation, ssLocation);

            //REFRESHING THE ANDROID PHONE PHOTO GALLERY IS BEGUN
            //AndroidJavaClass classPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            //AndroidJavaObject objActivity = classPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            //AndroidJavaClass classUri = new AndroidJavaClass("android.net.Uri");
            //AndroidJavaObject objIntent = new AndroidJavaObject("android.content.Intent", new object[2] { "android.intent.action.MEDIA_MOUNTED",
            //                                                    classUri.CallStatic<AndroidJavaObject>("parse", "file://" + ssLocation) });
            //objActivity.Call("sendBroadcast", objIntent);

            takeSS = false;
        }
    }

    public void TakeScreenshot() {
        cam.targetTexture = RenderTexture.GetTemporary(Screen.width, Screen.height, 16);
        takeSS = true;
    }

}
