using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navRight : MonoBehaviour
{
    public void OnClick()
    {
    	var inputHandlerObj = GameObject.Find("InputHandler");
    	var lsInput = inputHandlerObj.GetComponent<LS_input>();

        if (lsInput.cursor == lsInput.songs.Count - 1)
        {
            lsInput.cursor = 0;
        }
        else
        {
            lsInput.cursor += 1;
        }
        lsInput.lsGraphic.texture = lsInput.songImgs[lsInput.cursor];

        var noteSound = GameObject.Find("NoteSound");
        var fxSound = noteSound.GetComponent<AudioSource>();
        fxSound.Play(0);
    }
}
