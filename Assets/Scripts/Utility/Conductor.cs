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

	//Judgement offset from music
	public float lagSetting;

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

	public float score = 0;

	//keep all the position-in-beats of notes in the song
	List<Notes> notes = new List<Notes>();

	//number of notes in chart
	public int numNotesTotal;

	//the index of the next note to be spawned
	int nextIndex = 0;

	//this is to keep track of the number of notes that have passed
	int numNotesPassed = 0;

	//this is to keep track of the number of notes hit
	public int numNotesHit = 0;

	//this is to keep track of current combo
	public int currentCombo = 0;

	//this is to keep track of max combo
	public int maxCombo = 0;

	//this is to keep track of total misses
	public int totalMissed = 0;

	//song accuracy
	public float accuracy = 100.0f;

	//keeps track of mania
	public int maniaMultiplier = 1;

	float currentNotePos;
	bool[] currentNoteBoolArray = new bool[] { false, false, false, false };
	public float lastPress = 0f;
    //public float lastRelease = 0f;

	public bool lastNoteHit = false;

	public List<Notes> currentNoteCheck = new List<Notes>();

	public string chartFilename;

	public GameObject Note0;
	public GameObject Note1;
	public GameObject Note2;
	public GameObject Note3;

	public GameObject Note0g;
	public GameObject Note1g;
	public GameObject Note2g;
	public GameObject Note3g;

    public GameObject Note0down;
    public GameObject Note0up;
    public GameObject Note1down;
    public GameObject Note1up;
    public GameObject Note2down;
    public GameObject Note2up;
    public GameObject Note3down;
    public GameObject Note3up;

    public GameObject Note0gHold;
    public GameObject Note1gHold;
    public GameObject Note2gHold;
    public GameObject Note3gHold;

	public GameObject NoteBar;

	public GameObject Canvas;

	public GameObject ScoreSaver;

	public Text ScoreText;
	public Text ComboText;
	public Text AccuracyText;
	public Text BlackLabelText;

	void Awake()
	{
		var modeManagerObj = GameObject.Find("ModeManager");
    	var modeManager = modeManagerObj.GetComponent<modeManager>();
    	if (modeManager.blackLabelMode)
    	{
    		judgmentValue = 0.14f;
    		BlackLabelText.text = "BLACK LABEL MODE";
    	}
    	else
    	{
    		judgmentValue = 0.22f;
    		BlackLabelText.text = "";
    	}
	}

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
	    	// + lagSetting;

	    //determine how many beats since the song started
	    songPositionInBeats = songPosition / secPerBeat;

	    if (nextIndex < numNotesTotal && notes[nextIndex].pos < songPositionInBeats + beatsShownInAdvance)
		{
			currentNotePos = notes[nextIndex].pos;
			notes[nextIndex].notes.CopyTo(currentNoteBoolArray, 0);
			currentNoteCheck.Add(new Notes(currentNotePos, currentNoteBoolArray));

			if (notes[nextIndex].isBar == true)
	    	{
	    		GameObject newNoteBar = Instantiate(NoteBar, GameObject.FindGameObjectWithTag("Canvas").transform);
	    		currentNoteCheck[currentNoteCheck.Count - 1].isBar = true;
	    	}
	    	else if (notes[nextIndex].notes[0] == true)
		    {
		    	if (notes[nextIndex].isGlide == true)
		    	{
		    		GameObject newNote0g = Instantiate(Note0g, GameObject.FindGameObjectWithTag("Canvas").transform);
                    currentNoteCheck[currentNoteCheck.Count - 1].isGlide = true;
		    	}
                else if (notes[nextIndex].isGlideHold == true)
                {
                    GameObject newNote0gHold = Instantiate(Note0gHold, GameObject.FindGameObjectWithTag("Canvas").transform);
                    currentNoteCheck[currentNoteCheck.Count - 1].isGlideHold = true;
                }
		    	else if (notes[nextIndex].isDown == true)
                {
                    GameObject NewNote0down = Instantiate(Note0down, GameObject.FindGameObjectWithTag("Canvas").transform);
                    currentNoteCheck[currentNoteCheck.Count - 1].isDown = true;
                }
                /*
                else if (notes[nextIndex].isUp == true)
                {
                    GameObject NewNote0up = Instantiate(Note0up, GameObject.FindGameObjectWithTag("Canvas").transform);
                    currentNoteCheck[currentNoteCheck.Count - 1].isUp = true;
                }
                */
                else
		    	{
		    		GameObject newNote0 = Instantiate(Note0, GameObject.FindGameObjectWithTag("Canvas").transform);
		    	}
		    }
		    if (notes[nextIndex].notes[1] == true)
		    {
		    	if (notes[nextIndex].isGlide == true)
                {
                    GameObject newNote1g = Instantiate(Note1g, GameObject.FindGameObjectWithTag("Canvas").transform);
                    currentNoteCheck[currentNoteCheck.Count - 1].isGlide = true;
                }
                else if (notes[nextIndex].isGlideHold == true)
                {
                    GameObject newNote1gHold = Instantiate(Note1gHold, GameObject.FindGameObjectWithTag("Canvas").transform);
                    currentNoteCheck[currentNoteCheck.Count - 1].isGlideHold = true;
                }
                else if (notes[nextIndex].isDown == true)
                {
                    GameObject NewNote1down = Instantiate(Note1down, GameObject.FindGameObjectWithTag("Canvas").transform);
                    currentNoteCheck[currentNoteCheck.Count - 1].isDown = true;
                }
                /*
                else if (notes[nextIndex].isUp == true)
                {
                    GameObject NewNote1up = Instantiate(Note1up, GameObject.FindGameObjectWithTag("Canvas").transform);
                    currentNoteCheck[currentNoteCheck.Count - 1].isUp = true;
                }
                */
                else
                {
                    GameObject newNote1 = Instantiate(Note1, GameObject.FindGameObjectWithTag("Canvas").transform);
                }
		    }
		    if (notes[nextIndex].notes[2] == true)
		    {
		    	if (notes[nextIndex].isGlide == true)
                {
                    GameObject newNote2g = Instantiate(Note2g, GameObject.FindGameObjectWithTag("Canvas").transform);
                    currentNoteCheck[currentNoteCheck.Count - 1].isGlide = true;
                }
                else if (notes[nextIndex].isGlideHold == true)
                {
                    GameObject newNote2gHold = Instantiate(Note2gHold, GameObject.FindGameObjectWithTag("Canvas").transform);
                    currentNoteCheck[currentNoteCheck.Count - 1].isGlideHold = true;
                }
                else if (notes[nextIndex].isDown == true)
                {
                    GameObject NewNote2down = Instantiate(Note2down, GameObject.FindGameObjectWithTag("Canvas").transform);
                    currentNoteCheck[currentNoteCheck.Count - 1].isDown = true;
                }
                /*
                else if (notes[nextIndex].isUp == true)
                {
                    GameObject NewNote2up = Instantiate(Note2up, GameObject.FindGameObjectWithTag("Canvas").transform);
                    currentNoteCheck[currentNoteCheck.Count - 1].isUp = true;
                }
                */
                else
                {
                    GameObject newNote2 = Instantiate(Note2, GameObject.FindGameObjectWithTag("Canvas").transform);
                }
		    }
		    if (notes[nextIndex].notes[3] == true)
		    {
		    	if (notes[nextIndex].isGlide == true)
                {
                    GameObject newNote3g = Instantiate(Note3g, GameObject.FindGameObjectWithTag("Canvas").transform);
                    currentNoteCheck[currentNoteCheck.Count - 1].isGlide = true;
                }
                else if (notes[nextIndex].isGlideHold == true)
                {
                    GameObject newNote3gHold = Instantiate(Note3gHold, GameObject.FindGameObjectWithTag("Canvas").transform);
                    currentNoteCheck[currentNoteCheck.Count - 1].isGlideHold = true;
                }
                else if (notes[nextIndex].isDown == true)
                {
                    GameObject NewNote3down = Instantiate(Note3down, GameObject.FindGameObjectWithTag("Canvas").transform);
                    currentNoteCheck[currentNoteCheck.Count - 1].isDown = true;
                }
                /*
                else if (notes[nextIndex].isUp == true)
                {
                    GameObject NewNote3up = Instantiate(Note3up, GameObject.FindGameObjectWithTag("Canvas").transform);
                    currentNoteCheck[currentNoteCheck.Count - 1].isUp = true;
                }
                */
                else
                {
                    GameObject newNote3 = Instantiate(Note3, GameObject.FindGameObjectWithTag("Canvas").transform);
                }
		    }

		    if (nextIndex < numNotesTotal)
		    {
		    	nextIndex++;
			}
		}

		if (currentNoteCheck.Count > 0)
		{
			if (songPositionInBeats > (currentNoteCheck[0].pos + judgmentValue)
				&& currentNoteCheck[0].isGlide == false)
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
		using (StreamReader readChart = File.OpenText(Application.dataPath + "/StreamingAssets/" + chartFilename))
		{
			while (!readChart.EndOfStream)
			{
				string notesAtBeat = readChart.ReadLine();
				string[] data = notesAtBeat.Split(' ');
				float posBeats = float.Parse(data[0]);
				Notes newNote = new Notes(posBeats);
				for (int i = 1; i < 5; i++)
	            {
	            	if (data[i] == "b")
	            	{
	            		newNote.notes[i - 1] = false;
	            		newNote.isBar = true;
	            	}
	            	if (data[i] == "t")
	            	{
	            		newNote.notes[i - 1] = true;
	            	}
	            	if (data[i] == "g")
	            	{
	            		newNote.notes[i - 1] = true;
	            		newNote.isGlide = true;
	            	}
                    if (data[i] == "d")
                    {
                        newNote.notes[i - 1] = true;
                        newNote.isDown = true;
                    }
                    /*
                    if (data[i] == "u")
                    {
                        newNote.notes[i - 1] = true;
                        newNote.isUp = true;
                    }
                    */
                    if (data[i] == "h")
                    {
                        newNote.notes[i - 1] = true;
                        newNote.isGlideHold = true;
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
    		// + lagSetting;
    	if (pressTime > (currentNoteCheck[0].pos - (judgmentValue / 10f))
    		&& pressTime < (currentNoteCheck[0].pos + (judgmentValue / 10f))
    		&& currentNoteCheck[0].notes[0] == true
    		&& currentNoteCheck[0].isGlide == false)
    	{
    		numNotesHit++;
    		lastNoteHit = true;
    		score += (15 * maniaMultiplier);
    		//text: MANIAC
    	}
    	else if (pressTime > (currentNoteCheck[0].pos - judgmentValue)
    		&& pressTime < (currentNoteCheck[0].pos + judgmentValue)
    		&& currentNoteCheck[0].notes[0] == true
    		&& currentNoteCheck[0].isGlide == false)
    	{
    		numNotesHit++;
    		lastNoteHit = true;
    		score += (10 * maniaMultiplier);
    		//text: GREAT
    	}
    	else if (pressTime > (currentNoteCheck[0].pos - (judgmentValue * 1.5f))
    		&& pressTime < (currentNoteCheck[0].pos + (judgmentValue * 1.5f))
    		&& currentNoteCheck[0].notes[0] == true
    		&& currentNoteCheck[0].isGlide == false)
    	{
    		numNotesHit++;
    		lastNoteHit = true;
    		score += (5 * maniaMultiplier);
    		//text: OK
    	}
    	if (pressTime > lastPress)
    	{
    		lastPress = pressTime;
    	}

    	if (currentNoteCheck[0].isGlide == false)
    	{
    		checkCombo();
    	}
    }

    public void note1Pressed()
    {
		float pressTime = songPositionInBeats;
    		// + lagSetting;
    	if (pressTime > (currentNoteCheck[0].pos - (judgmentValue / 10f))
    		&& pressTime < (currentNoteCheck[0].pos + (judgmentValue / 10f))
    		&& currentNoteCheck[0].notes[1] == true
    		&& currentNoteCheck[0].isGlide == false)
    	{
    		numNotesHit++;
    		lastNoteHit = true;
    		score += (15 * maniaMultiplier);
    		//text: MANIAC
    	}
    	else if (pressTime > (currentNoteCheck[0].pos - judgmentValue)
    		&& pressTime < (currentNoteCheck[0].pos + judgmentValue)
    		&& currentNoteCheck[0].notes[1] == true
    		&& currentNoteCheck[0].isGlide == false)
    	{
    		numNotesHit++;
    		lastNoteHit = true;
    		score += (10 * maniaMultiplier);
    		//text: GREAT
    	}
    	else if (pressTime > (currentNoteCheck[0].pos - (judgmentValue * 1.5f))
    		&& pressTime < (currentNoteCheck[0].pos + (judgmentValue * 1.5f))
    		&& currentNoteCheck[0].notes[1] == true
    		&& currentNoteCheck[0].isGlide == false)
    	{
    		numNotesHit++;
    		lastNoteHit = true;
    		score += (5 * maniaMultiplier);
    		//text: OK
    	}
    	if (pressTime > lastPress)
    	{
    		lastPress = pressTime;
    	}

    	if (currentNoteCheck[0].isGlide == false)
    	{
    		checkCombo();
    	}
    }

    public void note2Pressed()
    {
    	float pressTime = songPositionInBeats;
    		// + lagSetting;
    	if (pressTime > (currentNoteCheck[0].pos - (judgmentValue / 10f))
    		&& pressTime < (currentNoteCheck[0].pos + (judgmentValue / 10f))
    		&& currentNoteCheck[0].notes[2] == true
    		&& currentNoteCheck[0].isGlide == false)
    	{
    		numNotesHit++;
    		lastNoteHit = true;
    		score += (15 * maniaMultiplier);
    		//text: MANIAC
    	}
    	else if (pressTime > (currentNoteCheck[0].pos - judgmentValue)
    		&& pressTime < (currentNoteCheck[0].pos + judgmentValue)
    		&& currentNoteCheck[0].notes[2] == true
    		&& currentNoteCheck[0].isGlide == false)
    	{
    		numNotesHit++;
    		lastNoteHit = true;
    		score += (10 * maniaMultiplier);
    		//text: GREAT
    	}
    	else if (pressTime > (currentNoteCheck[0].pos - (judgmentValue * 1.5f))
    		&& pressTime < (currentNoteCheck[0].pos + (judgmentValue * 1.5f))
    		&& currentNoteCheck[0].notes[2] == true
    		&& currentNoteCheck[0].isGlide == false)
    	{
    		numNotesHit++;
    		lastNoteHit = true;
    		score += (5 * maniaMultiplier);
    		//text: OK
    	}
    	if (pressTime > lastPress)
    	{
    		lastPress = pressTime;
    	}

    	if (currentNoteCheck[0].isGlide == false)
    	{
    		checkCombo();
    	}
    }

    public void note3Pressed()
    {
    	float pressTime = songPositionInBeats;
    		// + lagSetting;
    	if (pressTime > (currentNoteCheck[0].pos - (judgmentValue / 10f))
    		&& pressTime < (currentNoteCheck[0].pos + (judgmentValue / 10f))
    		&& currentNoteCheck[0].notes[3] == true
    		&& currentNoteCheck[0].isGlide == false)
    	{
    		numNotesHit++;
    		lastNoteHit = true;
    		score += (15 * maniaMultiplier);
    		//text: MANIAC
    	}
    	else if (pressTime > (currentNoteCheck[0].pos - judgmentValue)
    		&& pressTime < (currentNoteCheck[0].pos + judgmentValue)
    		&& currentNoteCheck[0].notes[3] == true
    		&& currentNoteCheck[0].isGlide == false)
    	{
    		numNotesHit++;
    		lastNoteHit = true;
    		score += (10 * maniaMultiplier);
    		//text: GREAT
    	}
    	else if (pressTime > (currentNoteCheck[0].pos - (judgmentValue * 1.5f))
    		&& pressTime < (currentNoteCheck[0].pos + (judgmentValue * 1.5f))
    		&& currentNoteCheck[0].notes[3] == true
    		&& currentNoteCheck[0].isGlide == false)
    	{
    		numNotesHit++;
    		lastNoteHit = true;
    		score += (5 * maniaMultiplier);
    		//text: OK
    	}
    	if (pressTime > lastPress)
    	{
    		lastPress = pressTime;
    	}

    	if (currentNoteCheck[0].isGlide == false)
    	{
    		checkCombo();
    	}
    }

    public void noteBarPressed()
    {
    	float pressTime = songPositionInBeats;
    		// + lagSetting;
    	if (pressTime > (currentNoteCheck[0].pos - (judgmentValue / 10f))
    		&& pressTime < (currentNoteCheck[0].pos + (judgmentValue / 10f))
    		&& currentNoteCheck[0].isBar == true)
    	{
    		numNotesHit++;
    		lastNoteHit = true;
    		score += (15 * maniaMultiplier);
    		//text: MANIAC
    	}
    	else if (pressTime > (currentNoteCheck[0].pos - judgmentValue)
    		&& pressTime < (currentNoteCheck[0].pos + judgmentValue)
    		&& currentNoteCheck[0].isBar == true)
    	{
    		numNotesHit++;
    		lastNoteHit = true;
    		score += (10 * maniaMultiplier);
    		//text: GREAT
    	}
    	else if (pressTime > (currentNoteCheck[0].pos - (judgmentValue * 1.5f))
    		&& pressTime < (currentNoteCheck[0].pos + (judgmentValue * 1.5f))
    		&& currentNoteCheck[0].isBar == true)
    	{
    		numNotesHit++;
    		lastNoteHit = true;
    		score += (5 * maniaMultiplier);
    		//text: OK
    	}
    	if (pressTime > lastPress)
    	{
    		lastPress = pressTime;
    	}
    	checkCombo();
    }

    /*

    public void note0Released()
    {
        float releaseTime = songPositionInBeats;

        if (currentNoteCheck[0].notes[0] == true && currentNoteCheck[0].isUp == true)
        {
            if (releaseTime > (currentNoteCheck[0].pos - (judgmentValue / 10f))
                && releaseTime < (currentNoteCheck[0].pos + (judgmentValue / 10f)))
            {
                numNotesHit++;
                lastNoteHit = true;
                score += (15 * maniaMultiplier);
                //text: MANIAC
            }
            else if (releaseTime > (currentNoteCheck[0].pos - judgmentValue)
                && releaseTime < (currentNoteCheck[0].pos + judgmentValue))
            {
                numNotesHit++;
                lastNoteHit = true;
                score += (10 * maniaMultiplier);
                //text: GREAT
            }
            else if (releaseTime > (currentNoteCheck[0].pos - (judgmentValue * 1.5f))
                && releaseTime < (currentNoteCheck[0].pos + (judgmentValue * 1.5f)))
            {
                numNotesHit++;
                lastNoteHit = true;
                score += (5 * maniaMultiplier);
                //text: OK
            }
        }
        if (releaseTime > lastRelease)
        {
            lastRelease = releaseTime;
        }
        checkCombo();
    }

    public void note1Released()
    {
        float releaseTime = songPositionInBeats;
  
        if (currentNoteCheck[0].notes[1] == true && currentNoteCheck[0].isUp == true)
        {
            if (releaseTime > (currentNoteCheck[0].pos - (judgmentValue / 10f))
                && releaseTime < (currentNoteCheck[0].pos + (judgmentValue / 10f)))
            {
                numNotesHit++;
                lastNoteHit = true;
                score += (15 * maniaMultiplier);
                //text: MANIAC
            }
            else if (releaseTime > (currentNoteCheck[0].pos - judgmentValue)
                && releaseTime < (currentNoteCheck[0].pos + judgmentValue))
            {
                numNotesHit++;
                lastNoteHit = true;
                score += (10 * maniaMultiplier);
                //text: GREAT
            }
            else if (releaseTime > (currentNoteCheck[0].pos - (judgmentValue * 1.5f))
                && releaseTime < (currentNoteCheck[0].pos + (judgmentValue * 1.5f)))
            {
                numNotesHit++;
                lastNoteHit = true;
                score += (5 * maniaMultiplier);
                //text: OK
            }
        }
        if (releaseTime > lastRelease)
        {
            lastRelease = releaseTime;
        }
        checkCombo();
    }

    public void note2Released()
    {
        float releaseTime = songPositionInBeats;
            
        if (currentNoteCheck[0].notes[2] == true && currentNoteCheck[0].isUp == true)
        {
            if (releaseTime > (currentNoteCheck[0].pos - (judgmentValue / 10f))
                && releaseTime < (currentNoteCheck[0].pos + (judgmentValue / 10f)))
            {
                numNotesHit++;
                lastNoteHit = true;
                score += (15 * maniaMultiplier);
                //text: MANIAC
            }
            else if (releaseTime > (currentNoteCheck[0].pos - judgmentValue)
                && releaseTime < (currentNoteCheck[0].pos + judgmentValue))
            {
                numNotesHit++;
                lastNoteHit = true;
                score += (10 * maniaMultiplier);
                //text: GREAT
            }
            else if (releaseTime > (currentNoteCheck[0].pos - (judgmentValue * 1.5f))
                && releaseTime < (currentNoteCheck[0].pos + (judgmentValue * 1.5f)))
            {
                numNotesHit++;
                lastNoteHit = true;
                score += (5 * maniaMultiplier);
                //text: OK
            }
        }
        if (releaseTime > lastRelease)
        {
            lastRelease = releaseTime;
        }
        checkCombo();
    }

    public void note3Released()
    {
        float releaseTime = songPositionInBeats;

        if (currentNoteCheck[0].notes[3] == true && currentNoteCheck[0].isUp == true)
        {
            if (releaseTime > (currentNoteCheck[0].pos - (judgmentValue / 10f))
                && releaseTime < (currentNoteCheck[0].pos + (judgmentValue / 10f)))
            {
                numNotesHit++;
                lastNoteHit = true;
                score += (15 * maniaMultiplier);
                //text: MANIAC
            }
            else if (releaseTime > (currentNoteCheck[0].pos - judgmentValue)
                && releaseTime < (currentNoteCheck[0].pos + judgmentValue))
            {
                numNotesHit++;
                lastNoteHit = true;
                score += (10 * maniaMultiplier);
                //text: GREAT
            }
            else if (releaseTime > (currentNoteCheck[0].pos - (judgmentValue * 1.5f))
                && releaseTime < (currentNoteCheck[0].pos + (judgmentValue * 1.5f)))
            {
                numNotesHit++;
                lastNoteHit = true;
                score += (5 * maniaMultiplier);
                //text: OK
            }
        }
        if (releaseTime > lastRelease)
        {
            lastRelease = releaseTime;
        }
        checkCombo();
    }

    */

    public void checkCombo()
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
    	if (accuracy > 99.0f && accuracy < 100.0f)
    	{
    		accuracy = 99.0f;
    	}
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