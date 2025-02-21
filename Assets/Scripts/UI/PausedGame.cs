using UnityEngine;

public class PausedGame : MonoBehaviour
{
    public bool gamePaused = false;
    public float UIFadeDuration;
    CanvasGroup group;
    void Start()
    {
        group = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gamePaused)
            {
                gamePaused = true;
                fadeCanvasGroup(group, true, UIFadeDuration);
            }

            else
            {
                gamePaused = false;
                fadeCanvasGroup(group, false, UIFadeDuration);
            }
            
        }
    }

    
    public void fadeCanvasGroup(CanvasGroup group, bool fadeIn, float duration)
    {
        if (fadeIn)
        {
            // Fade in the CanvasGroup
            LeanTween.alphaCanvas(group, 1f, duration).setOnComplete(() =>
            {
                Time.timeScale = 0;
                group.interactable = true;
                group.blocksRaycasts = true;
            });
        }

        else
        {
            {
                Time.timeScale = 1;
                // Fade out the CanvasGroup
                LeanTween.alphaCanvas(group, 0f, duration).setOnComplete(() =>
                {
                    
                    group.interactable = false;
                    group.blocksRaycasts = false;
                });
            }
        }

    }
}
