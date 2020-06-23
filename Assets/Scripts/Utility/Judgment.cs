using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judgment : MonoBehaviour
{
    void Update()
    {
    	var songManager = GameObject.Find("SongManager");
    	var conductor = songManager.GetComponent<Conductor>();

        if (Input.GetKeyDown(KeyCode.D))
        {
            conductor.note0Pressed();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
        	conductor.note1Pressed();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
        	conductor.note2Pressed();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
        	conductor.note3Pressed();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            conductor.noteBarPressed();
        }
        /*
        if (Input.GetKeyUp(KeyCode.D))
        {
            conductor.note0Released();
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            conductor.note1Released();
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            conductor.note2Released();
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            conductor.note3Released();
        }
        */
    }
}
