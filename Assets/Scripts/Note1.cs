using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note1 : MonoBehaviour
{
    float BeatsShownInAdvance = 8.0f;
    Vector2 SpawnPos = new Vector2(-720.2f, 554.0f);
    Vector2 RemovePos = new Vector2(-720.2f, -279.4f);
    GameObject songManager;

    void Awake()
    {
    	songManager = GameObject.Find("SongManager");
    }

    void Update()
	{
		Conductor conductor = songManager.GetComponent<Conductor>();
	    float current = conductor.currentNotePos;
	    float songPos = conductor.songPositionInBeats;

	    this.transform.position = Vector2.Lerp(SpawnPos,
	    	RemovePos, (BeatsShownInAdvance - (current - songPos)) / BeatsShownInAdvance);    
	}
}
