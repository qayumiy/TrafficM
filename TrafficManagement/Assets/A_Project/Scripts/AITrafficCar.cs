using System.Collections.Generic;
using UnityEngine;

public class AITrafficCar : MonoBehaviour
{
    public List<Transform> waypoints;
    public float speed = 8f;
    public float stopDistance;
    public float waypointThreshold = 0.5f;

    private int currentIndex = 0;
    private Rigidbody rb;
    private float detectionDistance;
    private Vector3 initialPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
    }

    void FixedUpdate()
    {
        stopDistance = Random.Range(4f, 8f);
        detectionDistance = Random.Range(4f, 8f);
        if (currentIndex >= waypoints.Count)
        {

            transform.position = initialPosition;
            currentIndex = 0;
            return;
        }
        Transform target = waypoints[currentIndex];
        Vector3 direction = (target.position - transform.position).normalized;  // direction of the vehicle

        bool stopForRed = IsRedLightAhead(target);
        bool stopForVehicle = IsVehicleInFront();

        if (stopForRed || stopForVehicle)
        {
            ApplyBrakes();
            return;
        }
        MoveForward(direction);

        if (Vector3.Distance(transform.position, target.position) < waypointThreshold)
            currentIndex++;
    }

    void MoveForward(Vector3 direction)
    {
        rb.linearVelocity = direction * speed;
    }

    void ApplyBrakes()
    {
        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, Time.fixedDeltaTime * 5f);
    }

    bool IsVehicleInFront()
    {
        Vector3 origin = transform.position + Vector3.up * 0.5f;

        // Define raycast offsets (left, center, right)
        Vector3[] offsets = new Vector3[]
        {
        transform.right * -0.6f,  // left ray
        Vector3.zero,            // center ray
        transform.right * 0.6f   // right ray
        };

        foreach (var offset in offsets)
        {
            Vector3 rayOrigin = origin + offset + transform.forward * 1.0f;

            if (Physics.Raycast(rayOrigin, transform.forward, out RaycastHit hit, detectionDistance))
            {
                if (hit.collider.CompareTag("AICar") || hit.collider.CompareTag("Vehicle"))
                {
                    Debug.DrawLine(rayOrigin, hit.point, Color.yellow);
                    return true;
                }
            }
        }

        return false;
    }

    bool IsRedLightAhead(Transform target)
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.5f, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, stopDistance))
        {
            if (hit.collider.CompareTag("TrafficLight"))
            {
                TrafficLightSignal signal = hit.collider.GetComponent<TrafficLightSignal>();
                if (signal != null && signal.currentColor == LightColor.Red)
                {
                    Debug.DrawLine(ray.origin, hit.point, Color.red);
                    //Debug.Log("Stopping for RED light at: " + hit.collider.name);
                    return true;
                }
            }
        }

        return false;
    }
}
