using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake instance;
    [SerializeField] private float shakeMagnitude = 0.1f; 
    [SerializeField] private float violentShakeMagnitude = 0.5f;
    [SerializeField] private float violentShakeDuration = 0.5f;

    private Vector3 originalPosition;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        instance = this;
        originalPosition = transform.localPosition;
    }

    private void Start()
    {
        
        StartCoroutine(ConstantShake());
        StartCoroutine(ViolentShakeRoutine());
    }

    
    private IEnumerator ConstantShake()
    {
        while (true) 
        {
            
            Vector3 randomOffset = Random.insideUnitSphere * shakeMagnitude;
            transform.localPosition = originalPosition + randomOffset;

            yield return null;
        }
    }

    private IEnumerator ViolentShakeRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(15f, 30f);
            yield return new WaitForSeconds(waitTime);

            yield return StartCoroutine(ViolentShake());
        }
    }

    
    private IEnumerator ViolentShake()
    {
        float elapsed = 0.0f;

        while (elapsed < violentShakeDuration)
        {
            
            Vector3 randomOffset = Random.insideUnitSphere * violentShakeMagnitude;
            transform.localPosition = originalPosition + randomOffset;

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }

    private void OnDisable()
    {
        StopCoroutine(ConstantShake());
        StopCoroutine(ViolentShakeRoutine());
        StopCoroutine(ViolentShake());
    }
}
