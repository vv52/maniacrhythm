using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judgment : MonoBehaviour
{
    void Update()
    {
    	var songManager = GameObject.Find("SongManager");
    	var conductor = songManager.GetComponent<Conductor>();

        //var noteSound = GameObject.Find("NoteSound");
        //var fxSound = noteSound.GetComponent<AudioSource>();

        if (Input.GetKeyDown(KeyCode.D))
        {
            conductor.note0Pressed();
            //fxSound.Play(0);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
        	conductor.note1Pressed();
            //fxSound.Play(0);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
        	conductor.note2Pressed();
            //fxSound.Play(0);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
        	conductor.note3Pressed();
            //fxSound.Play(0);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            conductor.noteBarPressed();
            //fxSound.Play(0);
        }
    }
}
