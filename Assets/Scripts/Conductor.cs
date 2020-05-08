using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class Conductor : MonoBehaviour
{
    //Song beats per minute
	//This is determined by the song you're trying to sync up to
	public float songBpm;

	//Beat position at which the chart concludes
	public float songEnd;

	//How early or late in beats can a note be pressed and still register
	public float judgmentValue;

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

	static public float score = 0;

	//keep all the position-in-beats of notes in the song
	List<Notes> notes = new List<Notes>();

	//number of notes in chart
	public int numNotesTotal;

	//the index of the next note to be spawned
	int nextIndex = 0;

	//this is to keep track of the number of notes that have passed
	int numNotesPassed = 0;

	//this is to keep track of the number of notes hit
	int numNotesHit = 0;

	//this is to keep track of current combo
	int currentCombo = 0;

	//this is to keep track of max combo
	static int maxCombo = 0;

	//this is to keep track of total misses
	static int totalMissed = 0;

	//song accuracy
	static float accuracy = 100.0f;

	//keeps track of mania
	int maniaMultiplier = 1;

	float currentNotePos;
	bool[] currentNoteBoolArray = new bool[] { false, false, false, false };
	float lastPress = 0;

	bool lastNoteHit = false;

	List<Notes> currentNoteCheck = new List<Notes>();

	public string chartFilename;

	public GameObject Note0;
	public GameObject Note1;
	public GameObject Note2;
	public GameObject Note3;

	public GameObject Canvas;

	public GameObject ScoreSaver;

	public Text ScoreText;
	public Text ComboText;
	public Text AccuracyText;

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
	}

	void Update()
	{
		if (songPositionInBeats > songEnd)
		{
			loadResults();
		}

	    //determine how many seconds since the song started
	    songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);

	    //determine how many beats since the song started
	    songPositionInBeats = songPosition / secPerBeat;

	    if (nextIndex < numNotesTotal && notes[nextIndex].pos < songPositionInBeats + beatsShownInAdvance)
		{
			currentNotePos = notes[nextIndex].pos;
			notes[nextIndex].notes.CopyTo(currentNoteBoolArray, 0);
			currentNoteCheck.Add(new Notes(currentNotePos, currentNoteBoolArray));

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

		    if (nextIndex < numNotesTotal)
		    {
		    	nextIndex++;
			}
		}

		if (currentNoteCheck.Count > 0)
		{
			if (songPositionInBeats > (currentNoteCheck[0].pos + judgmentValue))
			{
				numNotesPassed++;
				if (lastPress < (currentNoteCheck[0].pos - judgmentValue) ||
					lastPress > (currentNoteCheck[0].pos + judgmentValue))
				{
					lastNoteHit = false;
					currentCombo = 0;
					totalMissed++;
				}
				currentNoteCheck.RemoveAt(0);
			}
		}

		ScoreText.text = score.ToString();
		ComboText.text = currentCombo.ToString();
		if (numNotesPassed > 0)
		{
			accuracy = ((Convert.ToSingle(numNotesPassed) - Convert.ToSingle(totalMissed)) 
						/ Convert.ToSingle(numNotesPassed)) * 100;
		}
		if (accuracy > 100.0f)
		{
			accuracy = 100.0f;
		}
		if (accuracy < 0.0f)
		{
			accuracy = 0.0f;
		}
		AccuracyText.text = Convert.ToInt32(accuracy).ToString() + "%";
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
	}

	public float getSongPositionInBeats()
	{
		return songPositionInBeats;
	}

	public float getCurrentNotePosition()
	{
		return currentNotePos;
	}

	public void note0Pressed()
    {
    	float pressTime = songPositionInBeats;
    	if (pressTime > (currentNoteCheck[0].pos - judgmentValue)
    		&& pressTime < (currentNoteCheck[0].pos + judgmentValue)
    		&& currentNoteCheck[0].notes[0] == true)
    	{
    		numNotesHit++;
    		lastNoteHit = true;
    		score += (10 * maniaMultiplier);
    	}
    	if (pressTime > lastPress)
    	{
    		lastPress = pressTime;
    	}

    	checkCombo();
    }

    public void note1Pressed()
    {
		float pressTime = songPositionInBeats;
    	if (pressTime > (currentNoteCheck[0].pos - judgmentValue)
    		&& pressTime < (currentNoteCheck[0].pos + judgmentValue)
    		&& currentNoteCheck[0].notes[1] == true)
    	{
    		numNotesHit++;
    		lastNoteHit = true;
    		score += (10 * maniaMultiplier);
    	}
    	if (pressTime > lastPress)
    	{
    		lastPress = pressTime;
    	}

    	checkCombo();
    }

    public void note2Pressed()
    {
    	float pressTime = songPositionInBeats;
    	if (pressTime > (currentNoteCheck[0].pos - judgmentValue)
    		&& pressTime < (currentNoteCheck[0].pos + judgmentValue)
    		&& currentNoteCheck[0].notes[2] == true)
    	{
    		numNotesHit++;
    		lastNoteHit = true;
    		score += (10 * maniaMultiplier);
    	}
    	if (pressTime > lastPress)
    	{
    		lastPress = pressTime;
    	}

    	checkCombo();
    }

    public void note3Pressed()
    {
    	float pressTime = songPositionInBeats;
    	if (pressTime > (currentNoteCheck[0].pos - judgmentValue)
    		&& pressTime < (currentNoteCheck[0].pos + judgmentValue)
    		&& currentNoteCheck[0].notes[3] == true)
    	{
    		numNotesHit++;
    		lastNoteHit = true;
    		score += (10 * maniaMultiplier);
    	}
    	if (pressTime > lastPress)
    	{
    		lastPress = pressTime;
    	}

    	checkCombo();
    }

    void checkCombo()
    {
    	if (lastNoteHit)
    	{
    		currentCombo++;
    		if (currentCombo >= 100 && currentCombo < 250)
    		{
    			maniaMultiplier = 2;
    		}
    		if (currentCombo >= 250)
    		{
    			maniaMultiplier = 4;
    		}
    		if (currentCombo < 100)
    		{
    			maniaMultiplier = 1;
    		}
    		if (currentCombo > maxCombo)
    		{
    			maxCombo = currentCombo;
    		}
    	}
    }

    void loadResults()
    {
    	var scoreSaverObj = GameObject.Find("ScoreSaver");
    	var scoreSaver = scoreSaverObj.GetComponent<scoreSaver>();
    	scoreSaver.playerScore = Convert.ToInt32(score);
    	scoreSaver.playerAccuracy = Convert.ToInt32(accuracy);
   		scoreSaver.playerMaxCombo = maxCombo;
    	if (accuracy == 100.0f)
    	{
    		scoreSaver.playerFullCombo = true;
    	}
    	else
    	{
    		scoreSaver.playerFullCombo = false;
    	}
    	if (accuracy >= 98.0f)
    	{
    		scoreSaver.playerGrade = "S";
    	}
    	else if (accuracy < 98.0f && accuracy >= 92.0f)
    	{
    		scoreSaver.playerGrade = "A";
    	}
    	else if (accuracy < 92.0f && accuracy >= 86.0f)
    	{
    		scoreSaver.playerGrade = "B";
    	}
    	else if (accuracy < 86.0f && accuracy >= 80.0)
    	{
    		scoreSaver.playerGrade = "C";
    	}
    	else
    	{
    		scoreSaver.playerGrade = "F";
    	}
    	SceneManager.LoadScene("results", LoadSceneMode.Single);
    }
}