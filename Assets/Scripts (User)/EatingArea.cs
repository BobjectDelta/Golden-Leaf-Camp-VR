using UnityEngine;
using UnityEngine.Events;

public class EatingArea : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnFoodDetected;

    private void OnTriggerEnter(Collider other)
    {
        var food = other.GetComponent<Food>();
        //Debug.Log("Entered trigger " + gameObject.name);
        if (food != null)
        {
            Debug.Log("Eating food in trigger " + gameObject.name);
            OnFoodDetected?.Invoke();
            GameObject.Destroy(other.gameObject);
        }
        else
            Debug.Log("Not food in trigger " + gameObject.name);
        
    }
}
