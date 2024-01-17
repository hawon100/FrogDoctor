using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    private void Awake()
    {
        instance = this;
    }

    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource source = go.AddComponent<AudioSource>();
        source.clip = clip;
        source.Play();

        Destroy(go, clip.length);
    }
}
