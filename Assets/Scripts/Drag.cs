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

    private bool isInDrag = false;
    private Vector3 offset;

    private GameObject trashZone;
    private GameObject yesZone;

    private GameManager gameManager => GameManager.instance;

    private byte state = 0; //0=center; 1=trash; 2=good;

    private Animator _Animator;

    void Start()
    {
        _Animator = GetComponentInChildren<Animator>();
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
        if (transform.position.x < trashZone.transform.position.x)
        {
            Shake();
            state = 1;
            Debug.Log("To trash"); //TODO Remove Debug
        }
        else if (transform.position.x > yesZone.transform.position.x)
        {
            Shake();
            state = 2;
            Debug.Log("To yes"); //TODO Remove Debug
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
        SoundManager.instance.PlayOneShootSound(SoundReference.instance.sfx_GrabPaper, new Vector2(0, 0));

        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isInDrag = true;
        _Animator.SetTrigger("SetZoom");
    }
    
    private void OnMouseUp()
    {
        isInDrag = false;
        switch(state)
        {
            case 1:
                SoundManager.instance.PlayOneShootSound(SoundReference.instance.sfx_ThrowPaper, new Vector2(0, 0));
                _Animator.SetTrigger("SetThrow");
                //gameManager.DestoyNewsPaper();
                break;
            case 2:
                SoundManager.instance.PlayOneShootSound(SoundReference.instance.sfx_KeepPaper, new Vector2(0, 0));

                if (newsPaper.isFakeNews)
                {
                    moneySystem.currentMoney += moneySystem.intoxMoneyReward;
                    moneySystem.moneyWinIntox += moneySystem.intoxMoneyReward;
                    moneySystem.intoxPercentage += 20f;
                    Debug.Log(moneySystem.intoxPercentage);
                    Debug.Log("intox");
                    ResetObjectTransform();
                    _Animator.SetTrigger("SetIntoPaper");
                    
                    //gameManager.DestoyNewsPaper();
                }
                else
                {
                    moneySystem.currentMoney += moneySystem.newsMoneyReward;
                    moneySystem.moneyWinNews += moneySystem.newsMoneyReward;
                    moneySystem.intoxPercentage -= 5f;
                    Debug.Log(moneySystem.intoxPercentage);
                    Debug.Log("news");
                    ResetObjectTransform();
                    _Animator.SetTrigger("SetIntoPaper");
                    //gameManager.DestoyNewsPaper();
                }
                break;
            default:
                StartCoroutine(ResetPosition());
                break;
        }
    }

    private void ResetObjectTransform()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

}