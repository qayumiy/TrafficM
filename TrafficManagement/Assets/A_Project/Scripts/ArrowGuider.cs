using ScriptableObjectArchitecture;
using UnityEngine;

public class ArrowGuider : MonoBehaviour
{
    public Transform[] Locations;
    public Transform self;
    public GameEvent FinishEvent;

    public float zOffset = 0f;

    public Transform target;
    private RectTransform rect;
    public int index = 0;

    private void Start()
    {
        target = Locations[index];
        rect = GetComponent<RectTransform>();
    }


    private void LateUpdate()
    {
        if (!self || !target) return;

        // directions flattened onto the ground plane (ignore pitch/roll)
        Vector3 fwd = Vector3.ProjectOnPlane(self.forward, Vector3.up);
        Vector3 to = Vector3.ProjectOnPlane(target.position - self.position, Vector3.up);
        if (to.sqrMagnitude < 1e-6f) return;

        // -180..180 (negative = left, positive = right)
        float signed = Vector3.SignedAngle(fwd, to, Vector3.up);

        // UI rotates around Z. Negate to match screen-space CCW/clockwise,
        // and add sprite offset if your arrow texture points right, etc.
        rect.localRotation = Quaternion.Euler(0f, 0f, -signed + zOffset);

    }

    public void SetNextLocation()
    {
        if (index.Equals(4))
        {

            FinishEvent.Raise();

        }
        else if (index.Equals(3))
        {
            Locations[index + 1].gameObject.SetActive(true);
            index++;
            target = Locations[index];
        }
        else
        {
            index++;
            target = Locations[index];
        }
    }
}
