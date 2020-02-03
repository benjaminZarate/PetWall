using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show_Preview : MonoBehaviour
{
    Animator _anim;

    private void Start()
    {
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



}
