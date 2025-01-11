using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TileBoard board;
    public CanvasGroup gameOver;
    public TextMeshProUGUI best;
    public TextMeshProUGUI current;
    private int score;

    private void Start()
    {
        Debug.Log("Start.");
        GameStart();
    }

    public void GameStart()
    {
        gameOver.alpha = 0f;
        gameOver.interactable = false;
        score = 0;
        best.text = LoadBestScore().ToString();
        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;
    }

    public void GameOver()
    {
        Debug.Log("Gameover.");
        board.enabled = false;
        gameOver.interactable = true;
        StartCoroutine(Fade(gameOver, 1f, 1f));
    }

    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay)
    {
        yield return new WaitForSeconds(1f);

        float elapsed = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;

        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = to;
    }

    public void IncreaseScore(int points)
    {
        score += points;
        SetScore(score);
    }

    private void SetScore(int score)
    {
        this.score = score;

        current.text = score.ToString();
        SaveBestScore();
    }

    private void SaveBestScore()
    {
        int bestScore = LoadBestScore();
        if (score > bestScore) {
            PlayerPrefs.SetInt("best", score);
        }
    }

    private int LoadBestScore()
    {
        return PlayerPrefs.GetInt("best", 0);
    }
}
