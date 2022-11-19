using Infrastructure.Services;
using TMPro;
using UnityEngine;

public class HighScoreCounter : MonoBehaviour, IProgressWatcher 
{
    [SerializeField]private TMP_Text _text;
    public int _score;

    public void LoadProgress(PlayerProgress progress)
    {
        _score = progress.Score;
        ShowScore();
    }

    public void SaveProgress(PlayerProgress progress)
    {
        progress.Score = _score;
    }
        
    public void ShowScore()
    {
        _text.text = "Highscore: " + _score.ToString();
    }
}