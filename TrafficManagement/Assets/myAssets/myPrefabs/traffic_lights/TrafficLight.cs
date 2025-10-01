using UnityEngine;

public enum LightColor { Red, Yellow, Green }

[System.Serializable]
public class TrafficLight
{
    public GameObject redLight;
    public GameObject yellowLight;
    public GameObject greenLight;

    public void SetState(LightColor color)
    {
        redLight.SetActive(color == LightColor.Red);
        yellowLight.SetActive(color == LightColor.Yellow);
        greenLight.SetActive(color == LightColor.Green);
    }
}
