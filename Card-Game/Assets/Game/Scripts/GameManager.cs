using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Shatter shatter;
    public Level currentLevel;
    

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }
}
