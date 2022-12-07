using Infrastructure.Services;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour, IProgressWatcher 
{
    [SerializeField]private TMP_Text _highScoreText;
    [SerializeField]private TMP_Text _scoreText;
    public int HighScore;
    public int Score = 0;
    public void LoadProgress(PlayerProgress progress)
    {
        HighScore = progress.HighScore;
        ShowHighScore();
        ShowScore();
    }

    public void SaveProgress(PlayerProgress progress)
    {
        progress.HighScore = HighScore;
    }

    public bool CheckHighScore()
    {
        if (Score > HighScore)
        {
            HighScore = Score;
            return true;
        }
        return false;
        
    }
        
    public void ShowHighScore()
    {
        _highScoreText.text = "Highscore: " + HighScore.ToString();
    }

    public void ShowScore()
    {
        _scoreText.text = "Score: " + Score.ToString();
    }
    
    
}