﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note1 : MonoBehaviour
{
    float BeatsShownInAdvance = 4.0f;
    float current = 0.0f;
    Vector3 SpawnPos = new Vector3(239.697f, 1100.225f, 0.0f);
    Vector3 RemovePos = new Vector3(239.697f, 254.831f, 0.0f);
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

        if(transform.position.y < 250.787)
        {
            Destroy(gameObject);    
        }
    }
}
