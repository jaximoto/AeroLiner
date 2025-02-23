using UnityEngine;
using System.Collections;

public class WarningSign : MonoBehaviour
{
    SpriteRenderer rend;
    Color color;
    public float flashTime;
    public float life;
    void Start()
    {
        
        rend = GetComponent<SpriteRenderer>();
        color = rend.color;

        StartCoroutine(Flash());
        StartCoroutine(WaitAndDie());
    }

    IEnumerator Flash()
    {
        Debug.Log($"alpha is {rend.color.a}");
        while (gameObject.activeSelf)
        {
            if (rend.color.a == 0)
            {
                color = new Color(rend.color.r, rend.color.g, rend.color.b, 1);
                rend.color = color;

            }
            else if (rend.color.a == 1)
            {
                color = new Color(rend.color.r, rend.color.g, rend.color.b, 0);
                rend.color = color;
            }
            yield return new WaitForSeconds(flashTime);
        }
        if (!gameObject.activeSelf) 
        {
            yield return null;
        }
    }

    void OnDeactivate()
    {
        StopCoroutine(Flash());
    }

    IEnumerator WaitAndDie()
    {
        yield return new WaitForSeconds(life);
        gameObject.SetActive(false);
    }
}
