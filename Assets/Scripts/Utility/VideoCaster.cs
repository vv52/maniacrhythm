using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoCaster : MonoBehaviour
{
    public RawImage background;
    public VideoPlayer videoPlayer;
    public float delay;

    void Awake()
    {
    	videoPlayer.Prepare();
    }

    void FixedUpdate()
    {
    	var songManager = GameObject.Find("SongManager");
    	var conductor = songManager.GetComponent<Conductor>();
    	if (conductor.songPosition >= 0f)
    	{
    		StartCoroutine(PlayVideo());
    	}
        if (conductor.songPositionInBeats >= conductor.songEnd)
        {
            videoPlayer.Pause();
        }
    }

    IEnumerator PlayVideo()
    {
    	
    	WaitForSeconds waitForSeconds = new WaitForSeconds(delay);
    	while(!videoPlayer.isPrepared)
    	{
    		yield return waitForSeconds;
    		break;
    	}
    	background.texture = videoPlayer.texture;
    	videoPlayer.Play();
    }
}
