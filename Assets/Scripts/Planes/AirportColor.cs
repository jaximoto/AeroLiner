using UnityEngine;

public class AirportColor : MonoBehaviour
{
    // May want a enum of common colors
    public Color color;
    Collider2D[] childHitBoxes;
    SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        childHitBoxes = GetComponentsInChildren<Collider2D>();
        ChangeColor(color);
    }

    public void ChangeColor(Color color)
    {
        spriteRenderer.color = color;
        foreach(Collider2D child in childHitBoxes)
        {
            child.GetComponent<AirportCollider>().color = color;
        }
    }

}
