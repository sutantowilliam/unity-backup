using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataController : MonoBehaviour
{
    public static GlobalDataController Instance;
    public int scoreToBonus;
    public int difficultyLevel;
    public float lastDistance;
    public float starScore;
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
