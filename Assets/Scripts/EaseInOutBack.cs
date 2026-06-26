using UnityEngine;
using UnityEngine.UI;

public static class EaseInOutBack
{
    public static float easeInOutBack(float a, float b, float t) 
    {
        float c1 = 1.70158f;
        float c2 = c1 * 1.525f;

        var time = t < 0.5f 
            ? (Mathf.Pow(2f * t, 2f) * ((c2 + 1f) * 2f * t - c2)) / 2f 
            : (Mathf.Pow(2f * t - 2f, 2f) * ((c2 + 1f) * (t * 2f - 2f) + c2) + 2f) / 2f;
        return Mathf.Lerp(a,b,t);
            
    }

    public static float easeOutBack(float t)
    {
        float c1 = 1.70158f;
        float c3 = c1 + 1;
        return 1 + c3 * Mathf.Pow(t - 1, 3) + c1 * Mathf.Pow(t - 1, 2);
    }

    public static Vector2 easeInOutBackLerp(Vector2 a, Vector2 b, float t)
    {
        float c1 = 1.70158f;
        float c2 = c1 * 1.525f;
        float time = t < 0.5f 
            ? (Mathf.Pow(2f * t, 2f) * ((c2 + 1f) * 2f * t - c2)) / 2f 
            : (Mathf.Pow(2f * t - 2f, 2f) * ((c2 + 1f) * (t * 2f - 2f) + c2) + 2f) / 2f;
        return Vector2.Lerp(a,b,time);
    }
}