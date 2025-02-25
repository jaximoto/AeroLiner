using UnityEngine;
using System.Collections;

public class WarningSign : MonoBehaviour
{
    //SceneManager sceneManager;
    SpriteRenderer rend;
    Color color;
    Color invis;
    public float flashTime;
    public float life;
    void Awake()
    {
        
        rend = GetComponent<SpriteRenderer>();
        color = rend.color;
        invis = new Color(rend.color.r, rend.color.g, rend.color.b, 0);
    }
    void OnEnable()
    {
        GameSettings.ZoomTriggered += ClearSign;
        StartCoroutine(Flash());
        StartCoroutine(WaitAndDie());
    }
    void OnDisable()
    {
        StopCoroutine(Flash());
        GameSettings.ZoomTriggered -= ClearSign;
    }
    IEnumerator Flash()
    {
        //Debug.Log($"alpha is {rend.color.a}");
        while (gameObject.activeSelf)
        {
            if (rend.color.a == 0)
            {
                rend.color = color;

            }
            else if (rend.color.a == 1)
            { 
                rend.color = invis;
            }
            yield return new WaitForSeconds(flashTime);
        }
        if (!gameObject.activeSelf) 
        {
            yield return null;
        }
    }

    IEnumerator WaitAndDie()
    {
        yield return new WaitForSeconds(life);
        gameObject.SetActive(false);
    }

    void ClearSign()
    {
        gameObject.SetActive(false);
    }
}
