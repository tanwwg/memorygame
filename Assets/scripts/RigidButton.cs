using UnityEngine;
using UnityEngine.Events;

public class RigidButton : MonoBehaviour
{
    // private bool isPressed = true;
    public UnityEvent mouseDown;
    public UnityEvent mouseUp;
    
    void FixedUpdate()
    {
        // if (isPressed && depth < maxPressDepth)
        // {
        //     rb.AddForce(-localForward * pressForce, ForceMode.Force);
        // }
    }

    void OnMouseDown()
    {
        
        mouseDown.Invoke();
    }

    void OnMouseUp()
    {
        mouseUp.Invoke();
    }
}
