using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
Audio cross fade by losingisfun at:
https://forum.unity.com/threads/audio-crossfade-how.144606/
*/

public class AudioManager : MonoBehaviour
{
    //We create an array with 2 audio sources that we will swap between for transitions
    public static AudioSource[] aud = new AudioSource[2];
    //We will use this boolean to determine which audio source is the current one
    bool activeMusicSource;
    //We will store the transition as a Coroutine so that we have the ability to stop it halfway if necessary
    IEnumerator musicTransition;

    public enum VolumeType { Master, Background, SFX }

    public float backgroundVolume = 1f;
    public float sfxVolume = 1f;

    [Header("Background clips")]
    [Tooltip("Musica di sottofondo del menu e del gioco")]
    [SerializeField] AudioClip generalBackgroundClip = null;

    public static AudioManager instance = null;

    private void Awake()
    {
        //Verifica se esiste un'altra istanza dell'AudioManager nella scena
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this);

            //Create the AudioSource components that we will be using
            aud[0] = gameObject.AddComponent<AudioSource>();
            aud[1] = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        SetVolume(.5f, VolumeType.Master);
        newSoundtrack(generalBackgroundClip);
    }

    //use this method to start a new soundtrack, with a reference to the AudioClip that you want to use
    //    such as:        newSoundtrack((AudioClip)Resources.Load("Audio/soundtracks/track01"));
    public void newSoundtrack(AudioClip clip)
    {
        //This ?: operator is short hand for an if/else statement, eg.
        //
        //      if (activeMusicSource) {
        //          nextSource = 1;
        //      } else {
        //           nextSource = 0;
        //      }

        int nextSource = !activeMusicSource ? 0 : 1;
        int currentSource = activeMusicSource ? 0 : 1;

        //If the clip is already being played on the current audio source, we will end now and prevent the transition
        if (clip == aud[currentSource].clip)
            return;

        //If a transition is already happening, we stop it here to prevent our new Coroutine from competing
        if (musicTransition != null)
            StopCoroutine(musicTransition);

        aud[nextSource].clip = clip;
        aud[nextSource].Play();

        musicTransition = Transition(10); //20 is the equivalent to 2 seconds (More than 3 seconds begins to overlap for a bit too long)
        StartCoroutine(musicTransition);
    }

    //  'transitionDuration' is how many tenths of a second it will take, eg, 10 would be equal to 1 second
    IEnumerator Transition(int transitionDuration)
    {

        for (int i = 0; i < transitionDuration + 1; i++)
        {
            aud[0].volume = activeMusicSource ? (transitionDuration - i) * (1f / transitionDuration) : (0 + i) * (1f / transitionDuration);
            aud[1].volume = !activeMusicSource ? (transitionDuration - i) * (1f / transitionDuration) : (0 + i) * (1f / transitionDuration);

            //  Here I have a global variable to control maximum volume.
            //  options.musicVolume is a float that ranges from 0f - 1.0f
            //------------------------------------------------------------//
            aud[0].volume *= backgroundVolume;
            aud[1].volume *= backgroundVolume;
            //------------------------------------------------------------//

            yield return new WaitForSecondsRealtime(0.1f);
            //use realtime otherwise if you pause the game you could pause the transition half way
        }

        //finish by stopping the audio clip on the now silent audio source
        aud[activeMusicSource ? 0 : 1].Stop();

        activeMusicSource = !activeMusicSource;
        musicTransition = null;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
    {
        //Se siamo tornati al menù principale
        if (scene.buildIndex == 0)
        {
            //Torna ad eseguire la musica di background del menù se si usano le altre tracce
            if (aud[activeMusicSource ? 0 : 1].clip != generalBackgroundClip)
                newSoundtrack(generalBackgroundClip);
        }
        //Fai loopare la musica di background
        aud[activeMusicSource ? 0 : 1].loop = true;
    }

    //Suona l'effetto sonoro 
    public void PlayAudioSourceWithClip(AudioSource audioSource, AudioClip clip, bool isOneShot)
    {
        if (isOneShot)
            audioSource.PlayOneShot(clip, backgroundVolume);
        else
        {
            audioSource.clip = clip;
            audioSource.volume = backgroundVolume;
            audioSource.Play();
        }
    }

    //Imposta uno dei volumi: Master, Background, Effetti sonori
    public void SetVolume(float value, VolumeType volumeType)
    {
        switch (volumeType)
        {
            case VolumeType.Background:
                backgroundVolume = value;
                aud[activeMusicSource ? 0 : 1].volume = backgroundVolume;
                break;

            case VolumeType.Master:
                AudioListener.volume = value;
                break;

            case VolumeType.SFX:
                sfxVolume = value;
                break;
        }
    }
}