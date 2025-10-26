using UnityEngine;
using TMPro;

[System.Serializable]
public class ScoreData
{
    public int score;

    public ScoreData()
    {
        score = GameManager.score;
    }
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private TMP_Text scoreText;

    public static int score = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    public void Score(int scoreGained)
    {
        score += scoreGained;
    }

    void Update()
    {
        scoreText.text = ("Score: " + score);
    }
}
