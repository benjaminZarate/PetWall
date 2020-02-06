using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Preview_Behaviour : MonoBehaviour
{
    [HideInInspector]public string ssName;
    [HideInInspector]public byte[] ss;

    public static Preview_Behaviour Instance;

    private void Start()
    {
        Instance = this;
        this.GetComponent<RawImage>().color = new Color(255, 255, 255, 0);
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void Close()
    {
        this.GetComponent<RawImage>().color = new Color(255,255,255,0);
        for (int i = 0; i < this.transform.childCount; i++) 
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
        Show_Preview.Instance._anim.SetBool("Out", true);
        UnityEngine.Advertisements.Advertisement.Banner.Show();
    }
}
