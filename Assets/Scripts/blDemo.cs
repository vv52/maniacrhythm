using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class blDemo : MonoBehaviour
{
    public void OnClick()
    {
    	SceneManager.LoadScene("twb_bl", LoadSceneMode.Single);
    }
}
