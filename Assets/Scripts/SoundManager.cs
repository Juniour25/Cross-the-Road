using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Timeline.Actions;
using UnityEngine;

public class SoundManager : MonoBehaviour
{   
    private AudioSource sfxSource;
    public AudioClip knockedDownClip;
    public AudioClip waterSplashClip;
    public AudioClip coinClip;
    private AudioSource bgMusicSource;
    public AudioClip bgMusic;
    public AudioClip menuMusic;
    public static SoundManager instance=null;
    

    // Start is called before the first frame update
    void Awake()
    {
        if(instance==null){
            instance=this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance!=this){
            Destroy(gameObject);
            return;
        }
        // Get the AudioSource components from this game object
        AudioSource[] audioSources = gameObject.GetComponents<AudioSource>();
        // Assign the first AudioSource component to the sfxSource
        sfxSource = audioSources[0];
        // Assign the second AudioSource component to the bgMusicSource
        bgMusicSource = audioSources[1];
        
        if(bgMusicSource==null){
            bgMusicSource=gameObject.AddComponent<AudioSource>();
        }
       
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayBgMusic(){
        if(bgMusic!=null){
            bgMusicSource.clip=bgMusic;
            bgMusicSource.loop=true;
            bgMusicSource.Play();
        }
        else{
            Debug.LogWarning("Bg music missing");
        }
    }
    public void PauseBgMusic(){
        bgMusicSource.Pause();
    }
    public void ResumeBGMusic(){
        bgMusicSource.UnPause();
    }
    public void StopBgMusic(){
        bgMusicSource.Stop();
    }
    public void PlayMenuMusic(){
        if(menuMusic!=null){
            bgMusicSource.clip=menuMusic;
            bgMusicSource.loop=true;
            bgMusicSource.Play();
        }
        else{
            Debug.LogWarning("Bg music missing");
        }
    }
    public void StopMenuMusic(){
        bgMusicSource.Stop();
    }
    public void PlaySound(AudioClip clip){
        if(clip!=null){
            sfxSource.clip=clip;
            sfxSource.loop=false;
            sfxSource.Play();
        }
        else{
            Debug.LogWarning("Audio clip missing");
        }
        
    }
    public void PlayCoinCollect(){
        PlaySound(coinClip);
    }
    public void PlayKnockedDown(){
        PlaySound(knockedDownClip);
    }
    public void PlayWaterSplash(){
        PlaySound(waterSplashClip);
    }
}
