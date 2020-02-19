using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Preview_Behaviour : MonoBehaviour
{
    [HideInInspector]public string ssName;
    [HideInInspector]public byte[] ss;

    [SerializeField] GameObject childs;

    public static Preview_Behaviour Instance;

    public AudioSource click;

    RawImage image;

    private void Start()
    {
        Instance = this;
        image = GetComponent<RawImage>();
        //image.color = new Color(255, 255, 255, 0);
        image.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (this.gameObject.activeSelf) {
                Close();
            }
        }
    }

    public void Close()
    {
        click.Play();
        //image.color = new Color(255,255,255,0);
        image.gameObject.SetActive(false);
        //childs.SetActive(false);
        Show_Preview.Instance._anim.SetBool("Out", true);
        UnityEngine.Advertisements.Advertisement.Banner.Show();
    }
}
