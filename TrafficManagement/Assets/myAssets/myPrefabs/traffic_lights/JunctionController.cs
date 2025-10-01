using UnityEngine;
using System.Collections;

public class JunctionController : MonoBehaviour
{
    public TrafficLight northLight, southLight, eastLight, westLight;
    public PedestrianLight northPed, southPed, eastPed, westPed;

    // Signal zones for AI cars
    public TrafficLightSignal signal_North;
    public TrafficLightSignal signal_South;
    public TrafficLightSignal signal_East;
    public TrafficLightSignal signal_West;

    public float greenTime = 8f;
    public float yellowTime = 2f;
    public float allRedTime = 1f;

    void Start()
    {
        StartCoroutine(TrafficCycle());
    }

    IEnumerator TrafficCycle()
    {
        while (true)
        {
            // Phase 1: NS Green
            SetNS(LightColor.Green);
            SetEW(LightColor.Red);
            SetSignalNS(LightColor.Green);
            SetSignalEW(LightColor.Red);
            yield return new WaitForSeconds(greenTime);

            // Phase 2: NS Yellow
            SetNS(LightColor.Yellow);
            SetSignalNS(LightColor.Yellow);
            yield return new WaitForSeconds(yellowTime);

            // Phase 3: All Red
            SetAll(LightColor.Red);
            SetAllSignals(LightColor.Red);
            yield return new WaitForSeconds(allRedTime);

            // Phase 4: EW Green
            SetEW(LightColor.Green);
            SetNS(LightColor.Red);
            SetSignalEW(LightColor.Green);
            SetSignalNS(LightColor.Red);
            yield return new WaitForSeconds(greenTime);

            // Phase 5: EW Yellow
            SetEW(LightColor.Yellow);
            SetSignalEW(LightColor.Yellow);
            yield return new WaitForSeconds(yellowTime);

            // Phase 6: All Red
            SetAll(LightColor.Red);
            SetAllSignals(LightColor.Red);
            yield return new WaitForSeconds(allRedTime);
        }
    }

    void SetNS(LightColor color)
    {
        northLight.SetState(color);
        southLight.SetState(color);
    }

    void SetEW(LightColor color)
    {
        eastLight.SetState(color);
        westLight.SetState(color);
    }

    void SetAll(LightColor color)
    {
        SetNS(color);
        SetEW(color);
    }

    void SetSignalNS(LightColor color)
    {
        signal_North.SetColor(color);
        signal_South.SetColor(color);
    }

    void SetSignalEW(LightColor color)
    {
        signal_East.SetColor(color);
        signal_West.SetColor(color);
    }

    void SetAllSignals(LightColor color)
    {
        SetSignalNS(color);
        SetSignalEW(color);
    }
}
