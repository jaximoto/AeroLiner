using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameSettings : MonoBehaviour
{
    // Airports
    public float AngleThreshold;
    public float planeRotationSpeed;

    // End Game Settings
    public float GameOverUIFadeInDuration;

    public bool gameEnded = false;
    public bool endingGame = false;
    public CanvasGroup endGameUI;


    private void Update()
    {
        if (gameEnded) 
            EndGame();
    }

    public void EndGame()
    {
        if (endingGame)
            return;

        endingGame = true;

        FadeIn();

    }

    public void FadeIn()
    {
        // Fade in the CanvasGroup
        LeanTween.alphaCanvas(endGameUI, 1f, GameOverUIFadeInDuration).setOnComplete(() =>
        {
            endGameUI.interactable = true;
            endGameUI.blocksRaycasts = true;
        });
    }
}
