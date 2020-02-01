using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show_Hide : MonoBehaviour
{
    bool isShowing = false;

    [SerializeField] GameObject arrows;

    private void Start()
    {
        arrows.SetActive(isShowing);
    }

    public void ChangeState() {
        isShowing = !isShowing;
        arrows.SetActive(isShowing);
    }
}
