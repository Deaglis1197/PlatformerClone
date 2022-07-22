using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playersLives=3;
    [SerializeField] int playersScore=0;
    [SerializeField] float gameResetWaitTime=5f; 
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;           
    void Awake()
    {
        int numGameSessions=FindObjectsOfType<GameSession>().Length;
        if(numGameSessions>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        WritePlayerLivesText();
        WritePlayerScoreText();
    }

    private void WritePlayerLivesText()
    {
        
        livesText.text = playersLives.ToString();
    }
    private void WritePlayerScoreText()
    {
        scoreText.text = playersScore.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if(playersLives>1)
        {
            TakeLife();
        }
        else
        {
            DropLive();
            WaitForResetGame();
        }
    }

    private void TakeLife()
    {
        StartCoroutine(ResetLevel());
    }

    IEnumerator ResetLevel()
    {
        DropLive();
        yield return new WaitForSeconds(gameResetWaitTime);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void AddScore(int score)
    {
        playersScore+=score;
        WritePlayerScoreText();

    }

    public void DropLive()
    {
        playersLives--;
        WritePlayerLivesText();
    }

    public void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
    IEnumerator WaitForResetGame()
    {
        yield return new WaitForSeconds(gameResetWaitTime);
        
    }
}
