﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Conductor : MonoBehaviour
{
    //Song beats per minute
	//This is determined by the song you're trying to sync up to
	public float songBpm;

	//The number of seconds for each song beat
	public float secPerBeat;

	//Current song position, in seconds
	public float songPosition;

	//Current song position, in beats
	public float songPositionInBeats;

	//How many seconds have passed since the song started
	public float dspSongTime;

	//an AudioSource attached to this GameObject that will play the music.
	public AudioSource musicSource;

	//The offset to the first beat of the song in seconds
	public float firstBeatOffset;

	//Number of beats shown in advance before reaching the judgement line
	public float beatsShownInAdvance;

	//keep all the position-in-beats of notes in the song
	List<Notes> notes = new List<Notes>();

	//the index of the next note to be spawned
	int nextIndex = 0;

	float currentNotePos;

	public string chartFilename;

	public GameObject Note0;
	public GameObject Note1;
	public GameObject Note2;
	public GameObject Note3;

	public GameObject Canvas;

	void Start()
	{
	    //Load the AudioSource attached to the Conductor GameObject
	    musicSource = GetComponent<AudioSource>();

	    //Calculate the number of seconds in each beat
	    secPerBeat = 60f / songBpm;

	    //Record the time when the music starts
	    dspSongTime = (float)AudioSettings.dspTime;

	    //Start the music
	    musicSource.Play();

	    //Load the chart for the current song
	    loadChart();

	    //TEST
	    /*
	    bool[] testNote0 = { true, false, false, false };
	    bool[] testNote1 = { false, true, false, false };
	    bool[] testNote2 = { false, false, true, false };
	    bool[] testNote3 = { false, false, false, true };
	    bool[] testDoubleNote = { true, false, true, false };
	    bool[] testDoubleNote2 = { false, true, false, true };
	    notes = new Notes[] { new Notes(0.0f, testNote0), new Notes(0.5f, testNote0), new Notes(1.0f, testNote1),
	    	new Notes(1.5f, testNote2), new Notes(2.0f, testNote3), new Notes(2.5f, testNote1),
	    	new Notes(3.0f, testDoubleNote), new Notes(3.5f, testDoubleNote2) };
	    beatsShownInAdvance = 4.0f;
	    */
	}

	void Update()
	{
	    //determine how many seconds since the song started
	    songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);

	    //determine how many beats since the song started
	    songPositionInBeats = songPosition / secPerBeat;

	    if (nextIndex < notes.Count && notes[nextIndex].pos < songPositionInBeats + beatsShownInAdvance)
		{
			currentNotePos = notes[nextIndex].pos;
		    if (notes[nextIndex].notes[0] == true)
		    {
		    	GameObject newNote0 = Instantiate(Note0, GameObject.FindGameObjectWithTag("Canvas").transform);
		    }
		    if (notes[nextIndex].notes[1] == true)
		    {
		    	GameObject newNote1 = Instantiate(Note1, GameObject.FindGameObjectWithTag("Canvas").transform);
		    }
		    if (notes[nextIndex].notes[2] == true)
		    {
		    	GameObject newNote2 = Instantiate(Note2, GameObject.FindGameObjectWithTag("Canvas").transform);
		    }
		    if (notes[nextIndex].notes[3] == true)
		    {
		    	GameObject newNote3 = Instantiate(Note3, GameObject.FindGameObjectWithTag("Canvas").transform);
		    }

		    nextIndex++;
		}
	}

	void loadChart()
	{	
		using (StreamReader readChart = File.OpenText(chartFilename))
		{
			while (!readChart.EndOfStream)
			{
				string notesAtBeat = readChart.ReadLine();
				string[] data = notesAtBeat.Split(' ');
				float posBeats = float.Parse(data[0]);
				Notes newNote = new Notes(posBeats);
				for (int i = 1; i < 5; i++)
	            {
	            	if (data[i] == "t")
	            	{
	            		newNote.notes[i - 1] = true;
	            	}
	            }
	            notes.Add(newNote);
			}
			
		}

		//TODO: FIX THIS FUNCTION

    	//

		/*
		string unParsedChart = System.IO.File.ReadAllText(@"Assets/Charts/the_world_between3.txt");
		string[] chart = unParsedChart.Split(',');
        for (int i = 0; i < chart.Length; i++)
        {
        	string temp = chart[i];
        	float notePosition = float.Parse(temp, System.Globalization.CultureInfo.InvariantCulture);
        	bool[] temp2 = new bool[] { false, false, false, false };
        	Notes newNote = new Notes(notePosition, temp2);
            for (int j = 0; i < 4; i++)
            {
            	if (chart[i + 1 + j] == "t")
            	{
            		newNote.notes[j] = true;
            	}
            }
            notes.Add(newNote);
            i += 4;
        }
        */
	}

	public float getSongPositionInBeats()
	{
		return songPositionInBeats;
	}

	public float getCurrentNotePosition()
	{
		return currentNotePos;
	}
}