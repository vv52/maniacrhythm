using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class blDemo : MonoBehaviour
{
    public void OnClick()
    {
    	SceneManager.LoadScene("level_select_bl", LoadSceneMode.Single);
    }
}
