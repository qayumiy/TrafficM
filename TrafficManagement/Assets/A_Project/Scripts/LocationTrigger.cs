using ScriptableObjectArchitecture;
using UnityEngine;

public class LocationTrigger : MonoBehaviour
{
    public GameEvent LocationEvent;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Location"))
        {
            Debug.Log("Location Triggered");
            LocationEvent.Raise();
        }
    }
}
