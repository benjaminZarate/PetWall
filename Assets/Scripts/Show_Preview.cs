using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class Show_Preview : MonoBehaviour
{
    [HideInInspector] public Animator _anim;

    public static Show_Preview Instance;

    public AudioSource click;

    public GameObject rawChild;

    private void Start()
    {
        Instance = this;
        _anim = GetComponent<Animator>();
    }

    public void Activate() {
        _anim.SetBool("Out", false);
        StopAllCoroutines();
        StartCoroutine(Deactivate());
    }

    IEnumerator Deactivate() {
        yield return new WaitForSeconds(3);
        _anim.SetBool("Out", true);
    }

    public void BigPreview(RawImage raw) {
        click.Play();
        Advertisement.Banner.Hide();
        raw.gameObject.SetActive(true);
        //raw.color = new Color(255, 255, 255, 255);
        raw.texture = GetComponent<RawImage>().texture;
    }

}
