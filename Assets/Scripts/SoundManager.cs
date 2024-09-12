using Com.IsartDigital.Platerformer.Sound;
using FMODUnity;
using FMOD.Studio;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] public float masterVolume = 1f;
    [Range(0f, 1f)]
    [SerializeField] public float musicVolume = 1f;
    [Range(0f, 1f)]
    [SerializeField] public float sfxVolume = 1f;


    //private Bus _MasterBus;
    //private Bus _MusicBus;
    //private Bus _SfxBus;

    //[Header("Ambience")]
    //[SerializeField] private EventReference _AmbianceSound;

    [Header("MainMenu")]
    [SerializeField] private EventReference _MusicSound;
    [Header("Fight")]
    [SerializeField] private EventReference _Boss;
    [SerializeField] private EventReference _Fight;

    //[Header("Stinger")]
    //[SerializeField] private EventReference _StingerSound;

    public static SoundManager instance;

    private void Awake()
    {
        if (instance != null)
            print("SoundManager already existing in this scene");

        instance = this;

        //_MasterBus = RuntimeManager.GetBus("bus:/");
        //_MusicBus = RuntimeManager.GetBus("bus:/Music");
        //_SfxBus = RuntimeManager.GetBus("bus:/SFX");

        //_MasterBus.setVolume(StaticSettings.masterVolume);
        //_MusicBus.setVolume(StaticSettings.musicVolume);
        //_SfxBus.setVolume(StaticSettings.sfxVolume);

    }

    private void Start()
    {
        //if (!_AmbianceSound.IsNull)
        //    InitializedAbiance(_AmbianceSound);

        if (!_MusicSound.IsNull)
            InitializedMusic(_MusicSound);

        //if (!_StingerSound.IsNull)
        //    InitializedStinger(_StingerSound);
    }

    private void Update()
    {
        //_MasterBus.setVolume(masterVolume);
        //_MusicBus.setVolume(musicVolume);
        //_SfxBus.setVolume(sfxVolume);
    }

    #region PlayOneShoot
    /// <summary>
    /// PlayOneShootSound : Play a sond spacilized aroud the player position
    /// </summary>
    /// <param name="pSound">Sound to play</param>
    /// <param name="pPosition">Possition of the sound</param>
    public void PlayOneShootSound(EventReference pSound, Vector2 pPosition)
    {
        RuntimeManager.PlayOneShot(pSound, pPosition);
    }

    #endregion

    #region PlayActionSound

    private List<EventInstance> _FMODEvents = new List<EventInstance>();

    /// <summary>
    /// CreateFmodEventInstace :  create loop sound that need to be on a specific node on that specific zone
    /// </summary>
    /// <param name="pSound">Sound to play</param>
    /// <returns>EventInsance</returns>
    public EventInstance CreateFmodEventInstance(EventReference pSound, Transform pTransform = null)
    {
        EventInstance lEventInstance = RuntimeManager.CreateInstance(pSound);
        if (pTransform != null)
        {
            lEventInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(pTransform));
            RuntimeManager.AttachInstanceToGameObject(lEventInstance, pTransform);
        }

        _FMODEvents.Add(lEventInstance);

        return lEventInstance;
    }

    #endregion

    #region PlayProximitySound

    private List<StudioEventEmitter> _StudioEvents = new List<StudioEventEmitter>();

    /// <summary>
    /// InitializeFMODEventEmiter : Play a procimity sound, !!! the game object you use neet a "StudioEventEmitter" component !!!
    /// </summary>
    /// <param name="pSound">sound you want to play</param>
    /// <param name="pObject">object the sound emit from</param>
    /// <param name="pMinDist">Distance when the sound is at max value</param>
    /// <param name="pMaxDist">Distance when the sound is at min value, if the distance is over this value you will not hear the sound</param>
    /// <returns></returns>
    public StudioEventEmitter InitializeFMODEventEmiter(EventReference pSound, GameObject pObject, int pMinDist, int pMaxDist)
    {
        StudioEventEmitter lEmiter = pObject.GetComponent<StudioEventEmitter>();
        lEmiter.EventReference = pSound;
        _StudioEvents.Add(lEmiter);
        lEmiter.OverrideMinDistance = pMinDist;
        lEmiter.OverrideMaxDistance = pMaxDist;
        return lEmiter;
    }
    #endregion


    #region Ambiance&Music

    public EventInstance ambianceInstance;
    public EventInstance musicInstance;
    public EventInstance stingerIstance;

    public void InitializedAbiance(EventReference pSound)
    {
        ambianceInstance = CreateFmodEventInstance(pSound, transform);
        ambianceInstance.start();
    }

    public void InitializedMusic(EventReference pSound)
    {
        musicInstance = CreateFmodEventInstance(pSound, transform);
        musicInstance.start();
    }

    public void InitializedStinger(EventReference pSound)
    {
        stingerIstance = CreateFmodEventInstance(pSound, transform);
        stingerIstance.start();
    }

    public void SetParameter(EventInstance pSoundEvent, string pName, string pValue)
    {
        pSoundEvent.setParameterByNameWithLabel(pName, pValue);
    }

    #endregion

    /// <summary>
    /// FMODCleanup : Stop all sound
    /// </summary>
    private void FMODCleanup()
    {
        foreach (EventInstance lFMODEvent in _FMODEvents)
        {
            lFMODEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            lFMODEvent.release();
        }

        foreach (StudioEventEmitter lEvent in _StudioEvents)
        {
            lEvent.Stop();
        }
    }
    private void OnDestroy()
    {
        FMODCleanup();
    }
}