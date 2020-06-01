using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note0g : MonoBehaviour
{
    float BeatsShownInAdvance = 2.0f;   // NOTE SPEED
    float current = 0.0f;
    Vector3 SpawnPos = new Vector3(137.432f, 1100.225f, 0.0f);
    //Vector3 RemovePos = new Vector3(137.432f, 254.831f, 0.0f);
    //Vector3 RemovePos = new Vector3(137.432f, 252.5f, 0.0f);
    Vector3 RemovePos = new Vector3(137.432f, 250f, 0.0f);
    Conductor conductor;
    GameObject songManager;

    void Awake()
    {
        songManager = GameObject.FindWithTag("SongManager");
        conductor = songManager.GetComponent<Conductor>();
        current = conductor.getCurrentNotePosition();

    }

    void Update()
	{	
        songManager = GameObject.FindWithTag("SongManager");
        conductor = songManager.GetComponent<Conductor>();
	    float songPos = conductor.getSongPositionInBeats();

        float lerpValue = (BeatsShownInAdvance - (current - songPos)) / BeatsShownInAdvance;
        float currentYValue = SpawnPos.y - ((SpawnPos.y - RemovePos.y) * lerpValue);

        this.transform.position = new Vector3(SpawnPos.x, currentYValue, 0.0f);

        if (transform.position.y < 250.787)
        {
            if (Input.GetKey("d"))
            {
                conductor.numNotesHit++;
                conductor.lastNoteHit = true;
                conductor.score += (1 * conductor.maniaMultiplier);
            }
            else
            {
                conductor.lastNoteHit = false;
            }
            conductor.currentNoteCheck.RemoveAt(0);
            conductor.checkCombo();
            Destroy(gameObject);    
        }
	}
}
