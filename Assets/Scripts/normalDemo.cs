using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class normalDemo : MonoBehaviour
{
    public void OnClick()
    {
    	SceneManager.LoadScene("twb", LoadSceneMode.Single);
    }
}
