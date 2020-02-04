using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;

public class Preview_Behaviour : MonoBehaviour
{
    public string ssName;
    public byte[] ss;

    public static Preview_Behaviour Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Close()
    {
        this.GetComponent<RawImage>().texture = null;
    }

    public void ShareImage() {
        FB.Init(OnInit);

        var wwwForm = new WWWForm();
        wwwForm.AddBinaryData("image", ss, ssName);
        wwwForm.AddField("message", "Write description here");
        //calling graph api 
        FB.API("me/photos", HttpMethod.POST, ShareScreenShotCallback, wwwForm);
    }

    public void OnInit() {
        LoginToFB();
    }

    public void LoginToFB() {
        FB.LogInWithPublishPermissions(new List<string>() { "publish_actions" });
    }

    public void ShareScreenShotCallback(IResult result) {
    }



}
