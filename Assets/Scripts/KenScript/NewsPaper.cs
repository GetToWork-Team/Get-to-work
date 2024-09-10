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
    }

    public void UpdateNewspaperInformation()
    {
        titleText.text = title;

        for (int i = 0; i < informationList.Count; i++)
        {
            Debug.Log(informationList[i]);
            if(i >= textMeshPro.Count)
            {
                break;
            }
            textMeshPro[i].text = informationList[i];
        }
    }
}
