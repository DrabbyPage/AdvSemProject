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
    public AudioClip Hit_Sound;
    public AudioClip ZombieMoveSound;
    public AudioClip CitizenDyingSound;
    public AudioClip ZombieDyingSound;
    public AudioClip Shoot_Sound;
    public AudioClip ButtonPressAudio;
    public AudioClip ButtonHoverAudio;

    [Header("RECORD PLAYERS")]
    [Header("player 1 specific record players")]
    public AudioSource MenuPlayer; //this is player 1's source
    public AudioSource PlayerWalking;
    public AudioSource PlayerHits;
    public AudioSource CharacterDeath;
    public AudioSource ShootingSource;
    
    [Header("music record players")]
    public AudioSource MusicRecordPlayer;
    public AudioClip FantasyMusicAudio;
    public AudioClip RaceMusicAudio;
    public AudioClip ArcadeMusicAudio;


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
            Destroy(instance.gameObject);
            instance = this;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //*************** UNIVERSAL SOUNDS***************

    // this function determins which player is being hit and then which record player plays the sound
    public void PlayHitSound()
    {
        if (MenuPlayer != null)
        {
            PlayerHits.clip = Hit_Sound;

            if (!PlayerHits.isPlaying)
            {
                PlayerHits.Play();
            }
        }
    }

    public void PlayShootSound()
    {
        if (ShootingSource != null)
        {
            ShootingSource.clip = Shoot_Sound;

            if (!ShootingSource.isPlaying)
            {
                ShootingSource.Play();
            }
        }
    }

    public void PlayCharDeathSound()
    {
        if (CharacterDeath != null)
        {
            CharacterDeath.clip = CitizenDyingSound;

            if (!CharacterDeath.isPlaying)
            {
                CharacterDeath.Play();
            }
        }
    }

    public void PlayerZombieDeathSound()
    {
        if (CharacterDeath != null)
        {
            CharacterDeath.clip = ZombieDyingSound;

            if (!CharacterDeath.isPlaying)
            {
                CharacterDeath.Play();
            }
        }
    }

    public void PlayerZombieMoveSound()
    {
        if (PlayerWalking != null)
        {
            PlayerWalking.clip = ZombieMoveSound;

            if (!PlayerWalking.isPlaying)
            {
                PlayerWalking.Play();
            }
        }
    }

    public void PlayButtonPress()
    {
        if (MenuPlayer != null)
        {
            MenuPlayer.clip = ButtonPressAudio;

            if(MenuPlayer.clip != null)
            {
                MenuPlayer.Play();
            }
        }
    }

    public void PlayButtonHover()
    {
        if (MenuPlayer != null)
        {
            MenuPlayer.clip = ButtonHoverAudio;

            if(MenuPlayer.clip != null)
            {
                MenuPlayer.Play();
            }
        }
    }

    public void PlayFantasyMusic()
    {
        if (MusicRecordPlayer != null)
        {
            MusicRecordPlayer.clip = FantasyMusicAudio;

            if(MusicRecordPlayer.clip != null)
            {
                MusicRecordPlayer.Play();
            }
        }
    }

    public void PlayRaceMusic()
    {
        if (MusicRecordPlayer != null)
        {
            MusicRecordPlayer.clip = RaceMusicAudio;

            if(MusicRecordPlayer.clip != null)
            {
                MusicRecordPlayer.Play();
            }
        }
    }

    public void PlayArcadeMusic()
    {
        if (MusicRecordPlayer != null)
        {
            MusicRecordPlayer.clip = ArcadeMusicAudio;

            if(MusicRecordPlayer.clip != null)
            {
                MusicRecordPlayer.Play();
            }
        }
    }
}
