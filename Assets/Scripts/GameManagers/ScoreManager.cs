using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    internal int score;
    internal int hiddenGoodies;

    public static ScoreManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ScoreManager>();
            }

            return _instance;
        }
    }
    private static ScoreManager _instance;

    public void IncreaseScore( int scoreToAdd )
    {
        score += scoreToAdd;
    }

    public void DecreaseScore( int scoreToDecrease)
    {
        score -= scoreToDecrease;
    }

    
}
