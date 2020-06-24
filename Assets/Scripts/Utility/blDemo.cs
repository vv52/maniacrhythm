using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class blDemo : MonoBehaviour
{
	public float delay = 0f;

    public void OnClick()
    {
    	var modeManagerObj = GameObject.Find("ModeManager");
    	var modeManager = modeManagerObj.GetComponent<modeManager>();
    	modeManager.blackLabelMode = true;
    	var titleSound = GameObject.Find("TitleSound");
        var fxSound = titleSound.GetComponent<AudioSource>();
        fxSound.Play(0);
        //StartCoroutine(LoadLevelAfterDelay(delay));
        SceneManager.LoadScene("level_select", LoadSceneMode.Single);
    }
 
	IEnumerator LoadLevelAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
    	SceneManager.LoadScene("level_select", LoadSceneMode.Single);
	}
}
