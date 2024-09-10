using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewJournalData", menuName = "Journal Data")]
public class NewsPaperScriptableObject : ScriptableObject
{
    public NewsPaperEnum newsPaperType;
    public bool isFakeNews;

    public int numberOfInformation;
    [Header("Information du Journal")]
    [TextArea(3,10)]
    public string title;

    [TextArea(3, 10)]
    public List<string> Information = new List<string>();

    public Texture2D picture2D;

    public virtual void AdjustInformationList()
    {
        if (numberOfInformation < 0)
        {
            numberOfInformation = 0;
        }

        if (Information.Count > numberOfInformation)
        {
            Information.RemoveRange(numberOfInformation, Information.Count - numberOfInformation);
        }
        else
        {
            while (Information.Count < numberOfInformation)
            {
                Information.Add(string.Empty); // Ajoute des entrées vides
            }
        }
    }

}

public enum NewsPaperEnum
{
    Standard,
    Photo
}
