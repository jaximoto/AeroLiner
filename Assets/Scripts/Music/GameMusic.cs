using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMusic : MonoBehaviour
{
    public AudioClip track1, track2;
    AudioSource source;
    public float volume, startVolume;
    public float playVolume;
    public float deltaVolume;
    public bool subscribed = false;
    Button button;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        source = GetComponent<AudioSource>();
        source.clip = track1;
        source.Play();

        //StartButton.click += StartPlayback;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Tutorial" && !subscribed)
        {
            GameObject okay = GameObject.Find("Okay Button");
            button = okay.GetComponent<Button>();
            button.onClick.AddListener(StartPlayback);
            subscribed = false;
        }
    }
    void StartPlayback()
    {
        volume = source.volume;
        startVolume = volume;

        StartCoroutine(PlayOut(volume));
        button.onClick.RemoveListener(StartPlayback);
        subscribed = false;
        //StartButton.click -= StartPlayback;
    }


    //smooth transition to main track
    private IEnumerator PlayOut(float volume)
    {

        //Debug.Log($"GameMusic PlayOut(volume {volume}) called");
        while (volume > 0)
        {
            //Debug.Log($"GameMusic volume = {volume}");
            volume -= deltaVolume;
            source.volume = volume;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        if (volume <= 0)
        {
            source.clip = track2;
            StartCoroutine(PlayIn(volume));
            yield return null;
        }

    }

    private IEnumerator PlayIn(float volume)
    {
        source.Play();
        //Debug.Log($"GameMusic PlayIn(volume {volume}) called");
        while (volume < playVolume)
        {
            //Debug.Log($"GameMusic volume = {volume}");
            volume += deltaVolume;
            source.volume = volume;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        if (volume >= startVolume)
        {
            volume = playVolume;
            source.volume = volume;
            yield return null;
        }

    }


}
