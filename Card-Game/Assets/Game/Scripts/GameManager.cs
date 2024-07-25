using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene("Game");
    }
}
