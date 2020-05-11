using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class blDemo : MonoBehaviour
{
    public void OnClick()
    {
    	var modeManagerObj = GameObject.Find("ModeManager");
    	var modeManager = modeManagerObj.GetComponent<modeManager>();
    	modeManager.blackLabelMode = true;
    	SceneManager.LoadScene("level_select", LoadSceneMode.Single);
    }
}
