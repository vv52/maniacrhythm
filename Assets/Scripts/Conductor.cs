using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	Notes[] notes = new Notes[4];

	//the index of the next note to be spawned
	int nextIndex = 0;

	public float currentNotePos;

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

	    //TEST
	    bool[] testNote0 = { true, false, false, false };
	    bool[] testNote1 = { false, true, false, false };
	    bool[] testNote2 = { false, false, true, false };
	    bool[] testNote3 = { false, false, false, true };
	    bool[] testDoubleNote = { true, false, true, false };
	    notes = new Notes[] { new Notes(0.0f, testNote0), new Notes(2.0f, testNote1),
	    	new Notes(4.0f, testNote2), new Notes(6.0f, testNote3) };
	    beatsShownInAdvance = 8.0f;
	}

	void Update()
	{
	    //determine how many seconds since the song started
	    songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);

	    //determine how many beats since the song started
	    songPositionInBeats = songPosition / secPerBeat;

	    if (nextIndex < notes.Length && notes[nextIndex].pos < songPositionInBeats + beatsShownInAdvance)
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
}