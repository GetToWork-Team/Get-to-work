using UnityEngine;


public class NewsPaper : MonoBehaviour
{
    [Header("Donnee Necessaire")]
    [SerializeField] private SpriteRenderer _SpriteRenderer;
    public NewsPaperScriptableObject newsPaperScriptableObject;

    [Header("Information du Journal")]
    public bool isFakeNews;
    public Sprite newspaperTexture2D;

    private Animator _NewspaperAnimator;

    [SerializeField] private GameObject _AnimNewsPaperParent;

    private void Start()
    {
        _NewspaperAnimator = GetComponent<Animator>();
        SetUpScriptableObjectNewsPaper(newsPaperScriptableObject);
        UpdateSprite();
        _NewspaperAnimator.SetTrigger("SetMove");
    }

    public void SetUpScriptableObjectNewsPaper(NewsPaperScriptableObject pNewsPaperScriptableObject)
    {
        isFakeNews = pNewsPaperScriptableObject.isFakeNews;
        newspaperTexture2D = pNewsPaperScriptableObject.newsPaperTexture;
    }

    public void UpdateSprite()
    {
        if (_SpriteRenderer) { _SpriteRenderer.sprite = newspaperTexture2D; }
        
    }

    public void ChangeParentForAnimation()
    {
        transform.parent = _AnimNewsPaperParent.transform;
    }
}
