using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;
using System;
public class GameSettings : MonoBehaviour
{

    public int planesLanded = 0;
    /*
     * Color Table
     * 0 = Red
     */
    public List<Color> colorTable;
    // Airports and airplanes
    public float AngleThreshold;
    public float planeRotationSpeed;

    //cameraZoom
    public float zoomMax;



    // End Game Settings
    public float GameOverUIFadeInDuration;

    public bool gameEnded = false;
    public bool endingGame = false;
    public CanvasGroup endGameUI;
    public TMP_Text planeCountUI;

    //planes to clear eachLevel
    public int[] levelClears;
    public CameraZoom zoom;
    public static event Action ZoomTriggered;

    private void Update()
    {
        if (gameEnded) 
            EndGame();

        if (planesLanded >= levelClears[zoom.zoomLevel])
            LevelUp();
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

    void LevelUp()
    {
        ZoomTriggered?.Invoke();
        
    }


}
