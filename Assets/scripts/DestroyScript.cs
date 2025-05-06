using UnityEngine;

public class DestroyScript : MonoBehaviour
{
    public GameObject target;
    
    public void DestroyNow()
    {
        Destroy(target);
    }
}
