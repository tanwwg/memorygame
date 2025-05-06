using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterDelay : MonoBehaviour
{
    public float delay;

    public UnityEvent onInvoke;

    public void Invoke()
    {
        this.Invoke(nameof(InvokeImmediately), delay);
    }

    public void InvokeImmediately()
    {
        this.onInvoke.Invoke();
    }
}
