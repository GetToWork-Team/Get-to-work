using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class NewsPaper : MonoBehaviour
{
    public bool isFakeNews;
    public string title;
    public List<string> informationList = new List<string>();

    public NewsPaperScriptableObject newsPaperScriptableObject;

    public TextMeshPro titleText;
    public List<TextMeshPro> textMeshPro = new List<TextMeshPro>();

    private int _InformationNumber;

    private void Start()
    {
        SetUpScriptableObjectNewsPaper(newsPaperScriptableObject);
        UpdateNewspaperInformation();
    }

    public void SetUpScriptableObjectNewsPaper(NewsPaperScriptableObject pNewsPaperScriptableObject)
    {
        isFakeNews = pNewsPaperScriptableObject.isFakeNews;
        title = pNewsPaperScriptableObject.title;
        informationList = pNewsPaperScriptableObject.Information;
        _InformationNumber = pNewsPaperScriptableObject.numberOfInformation;
    }

    public void UpdateNewspaperInformation()
    {
        titleText.text = title;

        for (int i = 0; i < informationList.Count; i++)
        {
            if(i > textMeshPro.Count)
            {
                break;
            }
            textMeshPro[i].text = informationList[i];
        }
    }
}
