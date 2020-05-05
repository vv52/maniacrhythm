using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreSaver : MonoBehaviour
{
    public static scoreSaver Instance;

    public int playerScore;
    public int playerAccuracy;
    public int playerMaxCombo;
    public bool playerFullCombo;
    public string playerGrade;

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
