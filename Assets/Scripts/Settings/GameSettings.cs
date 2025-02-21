using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using TMPro;
public class GameSettings : MonoBehaviour
{

    public int planesLanded = 0;
    // Airports
    public float AngleThreshold;
    public float planeRotationSpeed;

    // End Game Settings
    public float GameOverUIFadeInDuration;

    public bool gameEnded = false;
    public bool endingGame = false;
    public CanvasGroup endGameUI;
    public TMP_Text planeCountUI;

    private void Update()
    {
        if (gameEnded) 
            EndGame();
    }

    public void IncrementPlaneCount()
    {
        planesLanded++;
        planeCountUI.text = "Planes Landed: " + planesLanded;
        
       
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

    public void ChangeScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }
}
