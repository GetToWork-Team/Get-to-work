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

    private void NextDay()
    {
        onNextDayButton?.Invoke();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
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
