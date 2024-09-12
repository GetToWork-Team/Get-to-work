using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewJournalData", menuName = "Journal Data")]
public class NewsPaperScriptableObject : ScriptableObject
{
    public bool isFakeNews;

    public int numberOfInformation;
    [Header("Texture du Journal")]

    public Sprite newsPaperTexture;
}
