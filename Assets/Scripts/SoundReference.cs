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

    /*[Header("SFX")]
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
    [SerializeField] public EventReference Opening;*/


    [SerializeField] public EventReference amb_Subway;
    [SerializeField] public EventReference mus_Day5;
    [SerializeField] public EventReference mus_Game;
    [SerializeField] public EventReference mus_Menu;
    [SerializeField] public EventReference sfx_Chime;
    [SerializeField] public EventReference sfx_DoorClose;
    [SerializeField] public EventReference sfx_GrabPaper;
    [SerializeField] public EventReference sfx_Info;
    [SerializeField] public EventReference sfx_Intox;
    [SerializeField] public EventReference sfx_KeepPaper;
    [SerializeField] public EventReference sfx_Lose;
    [SerializeField] public EventReference sfx_Phone;
    [SerializeField] public EventReference sfx_ThrowPaper;
    [SerializeField] public EventReference sfx_VoiceText;
    [SerializeField] public EventReference sfx_WinDay;
    [SerializeField] public EventReference ui_Click;
    [SerializeField] public EventReference ui_Highlight;
    [SerializeField] public EventReference ui_Play;

}