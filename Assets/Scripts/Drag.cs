using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    public NewsPaper newsPaper;
    public float resetSpeed = 0.1f;

    private GameObject moneySystemOBJ;
    private MoneySystem moneySystem;

    private bool isInDrag = false;
    private bool shouldReCenter = true;
    private Vector3 offset;
    
    private GameObject trashZone;
    private GameObject yesZone;

    void Start()
    {
        moneySystemOBJ = GameObject.Find("MoneySystem");
        moneySystem = GetComponent<MoneySystem>();
        trashZone = GameObject.Find("Canvas/Trash");
        yesZone = GameObject.Find("Canvas/Valid");
        //newsPaper = ;
    }

    void Update()
    {
        if(isInDrag)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }
    private void FixedUpdate()
    {
        if (transform.position.x < trashZone.transform.position.x)
        {
            shouldReCenter = false;
            Debug.Log("To trash");
        }
        else if (transform.position.x > yesZone.transform.position.x)
        {
            shouldReCenter = false;

            if (!isInDrag)
            {
                if (newsPaper.isFakeNews)
                {
                    moneySystem.currentMoney += moneySystem.intoxMoneyReward;
                    Debug.Log("intox");
                }
                else
                {
                    moneySystem.currentMoney += moneySystem.newsMoneyReward;
                    Debug.Log("news");
                }
            }
            Debug.Log("To yes");
            Debug.Log(moneySystem.currentMoney);
        }
        else
        {
            shouldReCenter = true;
        }
    }


    IEnumerator ResetPosition()
    {
        Vector3 curentPosition = transform.position;
        Vector3 origin = new Vector3( 0, 0, 0);

        for (float percent = 0; transform.position != origin; percent += resetSpeed)
        {
            transform.position = Vector3.Lerp(curentPosition, origin, percent);
            yield return null;
        }

    }



    private void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isInDrag = true;
    }
    
    private void OnMouseUp()
    {
        isInDrag = false;
        if(shouldReCenter)
        {
            StartCoroutine(ResetPosition());
        }
    }

}
