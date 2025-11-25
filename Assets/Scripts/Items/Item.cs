using UnityEngine;

public class Item : MonoBehaviour
{
    public Vector3 offset;
    public Quaternion rotationOffset;

    public virtual void OnHold() { }
    public virtual void OnDrop() { }
}
