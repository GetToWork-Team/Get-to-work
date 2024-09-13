using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WinScreenPanel : MonoBehaviour
{
    public MoneySystem money;

    public TextMeshProUGUI saving;
    public TextMeshProUGUI moneyWinWithNews;
    public TextMeshProUGUI moneyWinWithIntox;
    public TextMeshProUGUI intoxRep;
    public TextMeshProUGUI total;

    public GameObject GameOverIntox;

    [SerializeField] private Button _NextDayButton;
    public static UnityEvent onNextDayButton = new UnityEvent();


    public DialogueSystem dialogueSys;


    private void NextDay()
    {
        
        dialogueSys.Reset(LevelManager.instance.dayDialogue[LevelManager.instance.currentDayIndex+1]);
        StartCoroutine(WaitForTranstitionEnd());
    }

    private IEnumerator WaitForTranstitionEnd()
    {
        while (!dialogueSys.IsTransitionDark())
        {
            yield return null;
        }
        while (!dialogueSys.finished)
        {
            yield return null;

        }
        while (dialogueSys.IsTransitionDark())
        {
            yield return null;

        }
        onNextDayButton?.Invoke();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Timer.instance.isPaused = true;
        SoundManager.instance.PlayOneShootSound(SoundReference.instance.sfx_WinDay, new Vector2(0, 0));
        _NextDayButton.onClick.AddListener(NextDay);

        if (money.intoxPercentage < 0f)
        {
            money.intoxPercentage = 0f;
        }

        saving.SetText(money.saving.ToString());
        moneyWinWithNews.SetText(money.moneyWinNews.ToString());
        moneyWinWithIntox.SetText(money.moneyWinIntox.ToString());
        intoxRep.SetText(money.intoxPercentage.ToString() + " %");
        money.saving += money.currentMoney;
        money.currentMoney = 0f;
        money.moneyWinNews = 0f;
        money.moneyWinIntox = 0f;
        total.SetText(money.saving.ToString());

        if (money.intoxPercentage >= 100f)
        {
            GameOverIntox.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        _NextDayButton.onClick.RemoveAllListeners();
    }
}
