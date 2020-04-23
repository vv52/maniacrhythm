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

	//keep all the position-in-beats of notes in the song
	Notes[] notes = new Notes[4];

	//the index of the next note to be spawned
	int nextIndex = 0;

	public GameObject Note0;
	Vector3 note0StartPos = new Vector3(-821.9f, 554.0f, 0.0f);
	public GameObject Note1;
	Vector3 note1StartPos = new Vector3(-720.2f, 554.0f, 0.0f);
	public GameObject Note2;
	Vector3 note2StartPos = new Vector3(-624.2f, 554.0f, 0.0f);
	public GameObject Note3;
	Vector3 note3StartPos = new Vector3(-523.8f, 554.0f, 0.0f);

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
	    bool[] testNote = { true, false, false, false };
	    notes = new Notes[] { new Notes(0.0f, testNote), new Notes(2.0f, testNote) };
	}

	void Update()
	{
	    //determine how many seconds since the song started
	    songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);

	    //determine how many beats since the song started
	    songPositionInBeats = songPosition / secPerBeat;

	    if (nextIndex < notes.Length && notes[nextIndex].pos < songPositionInBeats)
		{
		    if (notes[nextIndex].notes[0] == true)
		    {
		    	GameObject newNote = Instantiate(Note0, note0StartPos,
		    		Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
		    }

		    //initialize the fields of the music note

		    nextIndex++;
		}
	}
}