using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static SoundManagerScript instance;
    public static SoundManagerScript getInstance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<SoundManagerScript>();

            if (instance == null)
            {
                GameObject tmp = new GameObject("TmpManager");
                instance = tmp.AddComponent<SoundManagerScript>();
            }

        }

        return instance;
    }

    [Header("universal sounds")]
    public AudioClip ReadyClip;
    public AudioClip CoolDownClip;
    public AudioClip Hit_snd;

    [Header("RECORD PLAYERS")]
    [Header("player 1 specific record players")]
    public AudioSource ASause; //this is player 1's source
    public AudioSource Player1Walking;
    public AudioSource Player1Hits;
    
    [Header("music record players")]
    public AudioSource MusicRecordPlayer;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //If instance already exists and it's not this:
        if (instance == null)
        {
            instance = this;
        }
        if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        ASause = gameObject.GetComponent<AudioSource>();
        Debug.Log(ASause);
        MusicRecordPlayer.Play();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //*************** UNIVERSAL SOUNDS***************

    public void EndCoolDownSound(bool isPlayer1)
    {
        if (isPlayer1)
        {
            // basically the tape in the player
            ASause.clip = ReadyClip;
            //this actually players whatever clip is loaded into the source
            ASause.Play();
        }

    }

    // this function determins which player is being hit and then which record player plays the sound
    public void PlayHitSound(bool isPlayer1)
    {
        if (isPlayer1)
        {
            Player1Hits.clip = Hit_snd;
            Player1Hits.Play();
        }
    }

}
