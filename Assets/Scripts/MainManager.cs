using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public Text ScoreText;
    public Text CurrentPlayerName;
    public Text BestPlayerName;

    public GameObject GameOverText;

    private PlayerController playerControllerScript;
    private static int BestScore;
    private static string BestPlayer;

    private int currentScore;
    private string currentName;
    private void Awake()
    {
        LoadGameRank();
    }

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        CurrentPlayerName.text = PlayerDataHandle.Instance.playerName;
        currentName = PlayerDataHandle.Instance.playerName;
        SetBestScore();
    }


    void Update()
    {
        if (playerControllerScript.gameOver == true)
        {
            CheckBestScore();
            GameOverText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

    }

    public void UpdateScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        PlayerDataHandle.Instance.score = currentScore;
        ScoreText.text = $"Score: {currentScore}";
    }

    private void CheckBestScore()
    {
        if (currentScore > BestScore)
        {
            BestScore = currentScore;
            BestPlayer = PlayerDataHandle.Instance.playerName;

            BestPlayerName.text = $"Best: {BestPlayer} - {BestScore}";
            SaveGameRank();
        }
    }
    public void SetBestScore()
    {
        if (BestPlayer == null && BestScore == 0)
        {
            BestPlayerName.text = "Best: ";
        }
        else
        {
            BestPlayerName.text = $"Best: {BestPlayer} - {BestScore}";
        }
    }

    public void SaveGameRank()
    {
        SaveData data = new SaveData();
        data.highestScore = currentScore;
        data.highestPlayer = currentName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadGameRank()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            BestScore = data.highestScore;
            BestPlayer = data.highestPlayer;
        }

    }

    [System.Serializable]
    class SaveData
    {
        public int highestScore;
        public string highestPlayer;
    }
}
