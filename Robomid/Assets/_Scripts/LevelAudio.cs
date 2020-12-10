using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelAudio : MonoBehaviour
{
    AudioSource source;
    [SerializeField]
    AudioClip[] clips;
    public static LevelAudio Instance;

    void Start()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            source = GetComponent<AudioSource>();
            source.clip = clips[Random.Range(0, clips.Length)];
            source.Play();
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void SetMute(bool val)
    {
        source.mute = val;
    }

    public void ResetClip()
    {
        Instance.source.clip = clips[Random.Range(0, clips.Length)];
        source.Play();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
