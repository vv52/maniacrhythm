﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navLeft : MonoBehaviour
{
   	public void OnClick()
    {
    	var inputHandlerObj = GameObject.Find("InputHandler");
    	var lsInput = inputHandlerObj.GetComponent<LS_input>();

        if (lsInput.cursor == 0)
        {
            lsInput.cursor = lsInput.songs.Count - 1;
        }
        else
        {
            lsInput.cursor -= 1;
        }
        lsInput.lsGraphic.texture = lsInput.songImgs[lsInput.cursor];
    }
}
