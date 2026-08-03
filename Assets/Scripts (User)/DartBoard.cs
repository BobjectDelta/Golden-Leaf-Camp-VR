using UnityEngine;
using UnityEngine.Events;

public class DartBoard : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnDartHit;

    private void OnCollisionEnter(Collision collision)
    {
        var dart = collision.collider.GetComponent<Dart>();
        //Debug.Log("Entered trigger " + gameObject.name);
        if (dart != null)
        {
            //Debug.Log("Cooking in trigger " + gameObject.name);
            dart.Stick(this.GetComponent<Collider>());
            OnDartHit?.Invoke();
        }
    }

}
