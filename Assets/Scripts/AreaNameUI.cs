using System.Collections;
using UnityEngine;
using TMPro;


public class AreaNameUI : MonoBehaviour
{
    public TextMeshProUGUI areaNameText; // The UI text element
    public float fadeInDuration = 1.5f; // Duration for fade-in
    public float visibleDuration = 2f; // Duration for the text to stay visible
    public float fadeOutDuration = 1.5f; // Duration for fade-out

    private float timer = 0f;
    private bool isFading = true;

    void Start()
    {
        if (areaNameText == null)
        {
            Debug.LogError("AreaNameUI: No TextMeshProUGUI assigned!");
            return;
        }

        // Initialize text alpha to 0
        areaNameText.alpha = 0;
        StartCoroutine(FadeInOut());
    }

    private IEnumerator FadeInOut()
    {
        // Fade In
        while (timer < fadeInDuration)
        {
            timer += Time.deltaTime;
            areaNameText.alpha = Mathf.Lerp(0, 1, timer / fadeInDuration);
            yield return null;
        }

        areaNameText.alpha = 1;
        yield return new WaitForSeconds(visibleDuration);

        // Fade Out
        timer = 0;
        while (timer < fadeOutDuration)
        {
            timer += Time.deltaTime;
            areaNameText.alpha = Mathf.Lerp(1, 0, timer / fadeOutDuration);
            yield return null;
        }

        areaNameText.alpha = 0;
        isFading = false; // End fading process
    }
}
