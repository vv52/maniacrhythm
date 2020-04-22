using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
	//position in beats from start of song
    public float pos;
    public bool[] notes = new bool[] { false, false, false, false };

    public Notes (float posBeats, bool[] activeNotes)
    {
    	pos = posBeats;
    	activeNotes.CopyTo(notes, 0);
    }
}