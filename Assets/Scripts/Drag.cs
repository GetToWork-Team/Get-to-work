using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Drag : MonoBehaviour
{
    public float resetSpeed = 0.1f;

    public float shakeSpeed = 1f;
    public float shakeStrenght = 0.05f;

    private NewsPaper newsPaper;
    private GameObject moneySystemOBJ;
    private MoneySystem moneySystem;
    private GameObject intoxBarOBJ;
    private Image intoxBar;

    private bool isInDrag = false;
    private Vector3 offset;

    private GameObject trashZone;
    private GameObject yesZone;

    private GameManager gameManager => GameManager.instance;

    private byte state = 0; //0=center; 1=trash; 2=good;


    void Start()
    {
        intoxBarOBJ = GameObject.Find("UI Canvas/Background/IntoxBar");
        intoxBar = intoxBarOBJ.GetComponent<Image>();
        moneySystemOBJ = GameObject.Find("MoneySystem");
        moneySystem = moneySystemOBJ.GetComponent<MoneySystem>();
        trashZone = GameObject.Find("Canvas/Trash");
        yesZone = GameObject.Find("Canvas/Valid");
    }

    void Update()
    {
        newsPaper = GetComponent<NewsPaper>();

        if(isInDrag)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }
    private void FixedUpdate()
    {
        //if (transform.position.x < trashZone.transform.position.x && !isInDrag)
        if (transform.position.x < trashZone.transform.position.x)
        {
            Shake();
            state = 1;
            //
            Debug.Log("To trash");
        }
        else if (transform.position.x > yesZone.transform.position.x)
        {
            Shake();
            state = 2;
            Debug.Log("To yes");
        }
        else
        {
            ResetRotation();
        }
    }

    void Shake()
    {
        Quaternion rotation = transform.rotation;
        rotation.z = Mathf.Cos(Time.fixedTime * shakeSpeed) * shakeStrenght;
        transform.rotation = rotation;
    }
    void ResetRotation()
    {
        Quaternion rotation = transform.rotation;
        rotation.z = 0f;
        transform.rotation = rotation;
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
        switch(state)
        {
            case 1:
                gameManager.DestoyNewsPaper();
                break;
            case 2:
                if (newsPaper.isFakeNews)
                {
                    moneySystem.currentMoney += moneySystem.intoxMoneyReward;
                    intoxBar.fillAmount += 0.1f;
                    if (intoxBar.fillAmount > 1f)
                    {
                        // TO DO : GAME OVER
                    }

                    Debug.Log("intox");
                    gameManager.DestoyNewsPaper();
                }
                else
                {
                    moneySystem.currentMoney += moneySystem.newsMoneyReward;
                    intoxBar.fillAmount -= 0.05f;
                    Debug.Log("news");
                    gameManager.DestoyNewsPaper();
                }
                break;
            default:
                StartCoroutine(ResetPosition());
                break;
        }
    }

}