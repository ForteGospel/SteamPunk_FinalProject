using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    [SerializeField]
    AudioClip[] clips;

    AudioSource source;
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

    public void changeClip(int clipNum)
    {
        source.clip = clips[clipNum];
        source.Play();
    }
}
