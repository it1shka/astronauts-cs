using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicpicker : MonoBehaviour
{
    public AudioSource audioPlayer;
    public AudioClip[] clips;
    void Awake() {
        if (!audioPlayer) audioPlayer = GetComponent<AudioSource>();
        audioPlayer.clip = clips[Random.Range(0,clips.Length)];
        audioPlayer.Play();
    }
}
