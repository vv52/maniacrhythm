using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreLoader : MonoBehaviour
{
    public Text ScoreText;
    public Text AccuracyText;
    public Text MaxComboText;
    public Text FullComboText;
    public Text GradeText;

    void Awake()
    {
    	var scoreSaverObj = GameObject.Find("ScoreSaver");
    	var scoreSaver = scoreSaverObj.GetComponent<scoreSaver>();

    	ScoreText.text = scoreSaver.playerScore.ToString();
    	AccuracyText.text = scoreSaver.playerAccuracy.ToString() + "%";
    	MaxComboText.text = scoreSaver.playerMaxCombo.ToString();
    	if (scoreSaver.playerFullCombo)
    	{
    		FullComboText.text = "Yes";
    	}
    	else
    	{
    		FullComboText.text = "No";
    	}
    	GradeText.text = scoreSaver.playerGrade;
    }
}
