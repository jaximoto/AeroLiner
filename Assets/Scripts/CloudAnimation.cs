using UnityEngine;

public class CloudAnimation : MonoBehaviour
{
    public bool transition, goUp;
    Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
        GameSettings.ZoomTriggered += LowerClouds;
        CameraZoom.zoomedOut += RaiseClouds;
    }


    void Update()
    {
        if(Input.GetButtonDown("Fire1")) 
        {
            Debug.Log("clouds fired");
            LowerClouds();
        }
    }
    void LowerClouds()
    {
        goUp = false;
        transition = true;

        anim.SetTrigger("goDown");
    }

    void RaiseClouds()
    {
        transition = false;
        goUp = true;

        anim.SetTrigger("goUp");
    }

}
