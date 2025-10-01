using UnityEngine;

public enum PedLightColor { Red, Green }

[System.Serializable]
public class PedestrianLight
{
    public GameObject redLight;
    public GameObject greenLight;

    public void SetState(PedLightColor color)
    {
        redLight.SetActive(color == PedLightColor.Red);
        greenLight.SetActive(color == PedLightColor.Green);
    }
}
