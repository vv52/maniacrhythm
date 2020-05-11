using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class normalDemo : MonoBehaviour
{
    public void OnClick()
    {
    	var modeManagerObj = GameObject.Find("ModeManager");
    	var modeManager = modeManagerObj.GetComponent<modeManager>();
    	modeManager.blackLabelMode = false;
    	SceneManager.LoadScene("level_select", LoadSceneMode.Single);
    }
}
