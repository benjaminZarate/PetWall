﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Change_Object : MonoBehaviour
{
    [SerializeField] List<Sprite> objectList = new List<Sprite>();

    [SerializeField] SpriteRenderer objectSprite;

    [SerializeField] TextMeshProUGUI currentSprite;

    int index = 0;

    public void NextObject(int dir)
    {
        index += dir;
        if (index >= objectList.Count)
        {
            index = 0;
        }
        else if (index < 0)
        {
            index = objectList.Count - 1;
        }

        objectSprite.sprite = objectList[index];
        currentSprite.text = index.ToString();
    }
}
