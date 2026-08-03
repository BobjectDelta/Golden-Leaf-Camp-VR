using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Dart : MonoBehaviour
{
    [SerializeField]
    private Collider _boardCollider = null;

    public void Stick(Collider collider)
    {
        if (!this.GetComponent<XRGrabInteractable>().isSelected)
        {
            Debug.Log("Stuck...");
            _boardCollider = collider;
            this.GetComponent<Rigidbody>().Sleep();
            //this.transform.SetParent(collider.transform, true);
        }
    }

    public void Unstick()
    {
        Debug.Log("Unstuck!");
        this.GetComponent<Rigidbody>().WakeUp();
        _boardCollider = null;
        //this.transform.SetParent(null, true);
    }
}
