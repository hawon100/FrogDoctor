using UnityEngine;

public class SpringAnim : MonoBehaviour
{
    float time;
    private RectTransform rectTransform; // Use RectTransform instead of Transform

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>(); // Get RectTransform component
        resetAnim();
    }

    // Update is called once per frame
    void Update()
    {
        if (time < 0.4f) // Move to the origin from a specific position
        {
            rectTransform.anchoredPosition = new Vector2(0, Mathf.Lerp(820, 0, time / 0.4f)); // Adjusted values for RectTransform
        }
        else if (time < 0.5f) // Bounce
        {
            rectTransform.anchoredPosition = new Vector2(0, Mathf.Lerp(0, 400, (time - 0.4f) / 0.1f)); // Adjusted values for RectTransform
        }
        else if (time < 0.6f) // Return to the original position
        {
            rectTransform.anchoredPosition = new Vector2(0, Mathf.Lerp(400, 0, (time - 0.5f) / 0.1f)); // Adjusted values for RectTransform
        }
        else if (time < 0.7f) // Bounce
        {
            rectTransform.anchoredPosition = new Vector2(0, Mathf.Lerp(0, 200, (time - 0.6f) / 0.1f)); // Adjusted values for RectTransform
        }
        else if (time < 0.8f) // Return to the original position
        {
            rectTransform.anchoredPosition = new Vector2(0, Mathf.Lerp(200, 0, (time - 0.7f) / 0.1f)); // Adjusted values for RectTransform
        }
        else
        {
            rectTransform.anchoredPosition = Vector2.zero;
        }

        time += Time.deltaTime;
    }

    public void resetAnim()
    {
        time = 0;
    }
}
