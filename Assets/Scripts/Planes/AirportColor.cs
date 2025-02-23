using UnityEngine;

public class AirportColor : MonoBehaviour
{
    // May want a enum of common colors
    public int colorIndex;
    Collider2D[] childHitBoxes;
    SpriteRenderer spriteRenderer;
    GameSettings gameSettings;
    private void Awake()
    {
        gameSettings = FindFirstObjectByType<GameSettings>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        childHitBoxes = GetComponentsInChildren<Collider2D>();
        ChangeColor(colorIndex);
        gameObject.SetActive(false);
    }

    public void ChangeColor(int colorIndex)
    {
        Color derivedColor = gameSettings.colorTable[colorIndex];
        spriteRenderer.color = derivedColor;
        foreach(Collider2D child in childHitBoxes)
        {
            child.GetComponent<AirportCollider>().color = derivedColor;
        }
    }

}
