using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modeManager : MonoBehaviour
{
    public static modeManager Instance;

    public bool blackLabelMode;
    public float songSpeedMult;

    void Awake ()   
       {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy (gameObject);
        }
      }
}
