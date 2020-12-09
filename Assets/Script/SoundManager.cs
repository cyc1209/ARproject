using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;

    [SerializeField]
    private AudioSource bgmSource = null;
    [SerializeField]
    private AudioClip bgm;

    void Awake()
    {
        if (SoundManager.instance == null)
            SoundManager.instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void bgmPlay()
    {
        bgmSource.clip = bgm;
        bgmSource.Play();
    }
}
