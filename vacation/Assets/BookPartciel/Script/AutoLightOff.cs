using UnityEngine;
using System.Collections;

public class AutoLightOff : MonoBehaviour 
{
    public bool destroy = true;

    public float duration = 0.2f;

    public float delayTime = 0.1f;    
    
    public float targetValue = 0.0f;

    float startValue = 1.0f;
    float oldValue = 0.0f;
    


    void OnEnable()
    {
        StartCoroutine(LightOffProcess());
    }

    void OnDisable()
    {
        GetComponent<Light>().intensity = oldValue;
        StopAllCoroutines();        
    }

    IEnumerator LightOffProcess()
    {
        yield return null;

        oldValue = GetComponent<Light>().intensity;
        float currentValue = startValue;
        float deltaTime = 0.0f;

        while( deltaTime / duration < 1.0f )
        {
            yield return new WaitForSeconds(delayTime);

            deltaTime += Time.deltaTime;
            GetComponent<Light>().intensity = Mathf.Lerp(currentValue, targetValue, deltaTime / duration);
            currentValue = GetComponent<Light>().intensity;
        }

        if (destroy)
        {
            Destroy(gameObject);
        }
    }
}
