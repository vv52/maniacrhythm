using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judgment : MonoBehaviour
{
    void Update()
    {
    	var songManager = GameObject.Find("SongManager");
    	var conductor = songManager.GetComponent<Conductor>();

        if (Input.GetKeyDown("d"))
        {
            conductor.note0Pressed();
        }
        if (Input.GetKeyDown("f"))
        {
        	conductor.note1Pressed();
        }
        if (Input.GetKeyDown("j"))
        {
        	conductor.note2Pressed();
        }
        if (Input.GetKeyDown("k"))
        {
        	conductor.note3Pressed();
        }
        if (Input.GetKeyDown("space"))
        {
            conductor.noteBarPressed();
        }
    }
}
