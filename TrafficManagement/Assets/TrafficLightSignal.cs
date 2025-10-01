using UnityEngine;

public class TrafficLightSignal : MonoBehaviour
{
    public LightColor currentColor = LightColor.Red;

    public void SetColor(LightColor color)
    {
        currentColor = color;
    }
}
