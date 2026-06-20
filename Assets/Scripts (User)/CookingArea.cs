using UnityEngine;
using UnityEngine.Events;

public class CookingArea : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnCookingStarted;
    [SerializeField] 
    private UnityEvent OnCookingFinished;

    private void OnTriggerEnter(Collider other)
    {
        var food = other.GetComponent<Food>();
        //Debug.Log("Entered trigger " + gameObject.name);
        if (food != null)
        {
            //Debug.Log("Cooking in trigger " + gameObject.name);
            food.StartCooking();
            OnCookingStarted?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var food = other.GetComponent<Food>();
        //Debug.Log("Exiting trigger " + gameObject.name);
        if (food != null)
        {
            //Debug.Log("Stopping in trigger " + gameObject.name);
            food.StopCooking();
            OnCookingFinished?.Invoke();
        }
    }
}
