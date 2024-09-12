using Com.IsartDigital.Platerformer.Sound;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundReference : MonoBehaviour
{
    public static SoundReference instance;

    private void Awake()
    {
        if (instance != null)
            print("SoundReference already existing in this scene");

        instance = this;
    }

    [Header("SFX")]
    [SerializeField] public EventReference damageToPlayer;
    [SerializeField] public EventReference buttonPressed;
    [SerializeField] public EventReference damageToEnemy;
    [SerializeField] public EventReference collisionDice;
    [SerializeField] public EventReference ActionError;
    [SerializeField] public EventReference GrabDice;
    [SerializeField] public EventReference HardCurency;
    [SerializeField] public EventReference PlaceDice;
    [SerializeField] public EventReference PutCard;
    [SerializeField] public EventReference RunningGrass;
    [SerializeField] public EventReference SoftCurency;
    [SerializeField] public EventReference UpgradeCard;

    [Header("Jingle")]
    [SerializeField] public EventReference jingleWin;
    [SerializeField] public EventReference jingleDefeat;
    [SerializeField] public EventReference Opening;
}