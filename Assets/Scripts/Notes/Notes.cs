﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes
{
	//position in beats from start of song
    public float pos;

    //which note lanes are active
    public bool[] notes = new bool[] { false, false, false, false };

    public bool isGlide = false;
    public bool isGlideHold = false;
    public bool isBar = false;
    public bool isDown = false;
    public bool isUp = false;

    //constructor
    public Notes (float posBeats, bool[] activeNotes)
    {
    	pos = posBeats;
    	activeNotes.CopyTo(notes, 0);
    }

    //alernate constructor
    public Notes (float posBeats)
    {
        pos = posBeats;
        notes = new bool[] { false, false, false, false };
    }
}