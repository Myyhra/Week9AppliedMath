using UnityEngine;

public static class EaseOutCirc 
{
    public static float easeOutCirc(float a, float b,float t)
    {
        var time =  Mathf.Sqrt(1 - Mathf.Pow(t - 1, 2));

        return  Mathf.Lerp( a,  b, time);
    } 
}
