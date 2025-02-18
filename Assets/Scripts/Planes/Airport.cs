using UnityEngine;

public class Airport : MonoBehaviour
{
    public GameObject frontBox, backBox;

    BoxCollider2D frontHitBox, backHitBox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        frontHitBox = frontBox.GetComponent<BoxCollider2D>();
        backHitBox = backBox.GetComponent<BoxCollider2D>();
    }

    // When a plane runs into the airport at one of the 2 hitboxes, start landing
    void Update()
    {
        
    }
}
