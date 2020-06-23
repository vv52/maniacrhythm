using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class res_input : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
        	SceneManager.LoadScene("level_select", LoadSceneMode.Single);
        }
    }
}
