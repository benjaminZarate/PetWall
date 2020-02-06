using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class Show_Preview : MonoBehaviour
{
    [HideInInspector] public Animator _anim;

    public static Show_Preview Instance;

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
        Advertisement.Banner.Hide();
        raw.color = new Color(255, 255, 255, 255);
        for (int i = 0; i < raw.transform.childCount; i++)
        {
            raw.transform.GetChild(i).gameObject.SetActive(true);
        }
        raw.texture = GetComponent<RawImage>().texture;
    }

}
