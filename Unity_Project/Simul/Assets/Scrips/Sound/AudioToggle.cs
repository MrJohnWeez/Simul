using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioToggle : MonoBehaviour
{
    private List<AudioSource> audios = new List<AudioSource>();

    void Start()
    {
        GameObject[] possibleAudioSources = GameObject.FindGameObjectsWithTag("BGMusic");

        foreach(GameObject go in possibleAudioSources)
        {
            AudioSource aud = go.GetComponent<AudioSource>();
            if(aud)
            {
                audios.Add(aud);
            }
        }
        ForceUpdate();
    }

    public void ForceUpdate()
    {
        if(!SoundControl.SountIsOn)
        {
            foreach(AudioSource aud in audios)
            {
                aud.Pause();
            }
        }
        else
        {
            foreach(AudioSource aud in audios)
            {
                aud.Play();
            }
        }
    }
}
