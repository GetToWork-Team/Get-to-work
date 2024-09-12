using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _ReturnToMain;
    [SerializeField] private Timer _Timer; // Assure-toi que ce Timer est utilisé ou sinon enlève-le
    public Transform pointA;
    public Transform pointB;
    public float duration = 2.0f;

    private float timeElapsed = 0.0f;
    private bool isAnimating = false;

    private void OnEnable()
    {
        _ReturnToMain.onClick.AddListener(OnReturnToMenu);
        StartCoroutine(AnimateTransition(transform.position, pointB.position, false));
    }

    private void OnDisable()
    {
        _ReturnToMain.onClick.RemoveListener(OnReturnToMenu);
    }

    private void OnReturnToMenu()
    {
        if (!isAnimating)
        {
            
            StartCoroutine(AnimateTransition(transform.position, pointA.position, true));
        }
    }

    private IEnumerator AnimateTransition(Vector3 startPosition, Vector3 endPosition, bool deactivateOnEnd)
    {
        isAnimating = true;
        timeElapsed = 0.0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / duration;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        transform.position = endPosition; // S'assurer que la position finale est correcte

        if (deactivateOnEnd)
        {
            gameObject.SetActive(false);
        }

        isAnimating = false;
    }
}


