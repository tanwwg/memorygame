using UnityEngine;

public class ApplyForce : MonoBehaviour
{
    public Rigidbody target;

    public Vector3 force;
    public Vector3 torque;
    
    public void Invoke()
    {
        target.constraints = RigidbodyConstraints.None;
        target.AddForce(force, ForceMode.Impulse);
        target.AddTorque(torque, ForceMode.Impulse);
    }
}
