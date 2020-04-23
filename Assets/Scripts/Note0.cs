using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note0 : MonoBehaviour
{
    float BeatsShownInAdvance = 8.0f;
    float current = 0.0f;
    Vector2 SpawnPos = new Vector2(-821.9f, 554.0f);
    Vector2 RemovePos = new Vector2(-821.9f, -279.4f);
    GameObject songManager;

    void Awake()
    {
    	songManager = GameObject.Find("SongManager");
        Conductor conductor = songManager.GetComponent<Conductor>();
        current = conductor.currentNotePos;
    }

    void Update()
	{	
        Conductor conductor = songManager.GetComponent<Conductor>();
	    float songPos = conductor.songPositionInBeats;

	    this.transform.position = Vector2.Lerp(SpawnPos,
	    	RemovePos, (BeatsShownInAdvance - (current - songPos)) / BeatsShownInAdvance);    
	}
}
