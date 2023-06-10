using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score Instance
    {
        get;
        private set;
    }
    [SerializeField] private TextMeshPro scoreText;
    private int currentScore;

    private void Awake() 
    {
        currentScore = 0;

        CorrectAcid.OnCorrectPlace += CorrectAcid_OnCorrectPlace;
        InCorrectAcid.OnIncorrectPlace += InCorrectAcid_OnIncorrectPlace;
    }

    private void InCorrectAcid_OnIncorrectPlace(object sender, EventArgs e)
    {
        currentScore -= 5;

        scoreText.text = currentScore.ToString();
    }

    private void CorrectAcid_OnCorrectPlace(object sender, EventArgs e)
    {
        currentScore += 10;

        scoreText.text = currentScore.ToString();
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }
}
