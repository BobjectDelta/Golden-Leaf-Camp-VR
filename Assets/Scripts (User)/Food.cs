using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Food : MonoBehaviour
{
    [SerializeField]
    private float _timeToCook = 5f;

    enum CookState {Raw, Cooked, Burned};
    private CookState _cookState = CookState.Raw;

    [SerializeField] 
    float _cookingTime = 0;
    private float _timeToBurn;

    private Coroutine _cookRoutine;

    public UnityEvent OnCooked;
    public UnityEvent OnBurned;

    private void Start()
    {
        _timeToBurn = _timeToCook * 2;
    }

    public void StartCooking()
    {
        if (_cookRoutine == null)
        {
            _cookRoutine = StartCoroutine(Cook());
            Debug.Log("Starting cooking");
        }
        //Debug.Log("Cooking routine after Start: " + _cookRoutine.ToString());
    }

    public void StopCooking()
    {
        Debug.Log("Cooking routine on Stop: " + (_cookRoutine != null));
        if (_cookRoutine != null)
        {
            Debug.Log("Stopping cooking");
            StopCoroutine(_cookRoutine);
            _cookRoutine = null;
        }
    }

    IEnumerator Cook()
    {
        while (_cookState != CookState.Burned)
        {
            if (_cookState == CookState.Raw && _cookingTime >= _timeToCook)
            {
                _cookState = CookState.Cooked;
                OnCooked?.Invoke();
            }
            else if (_cookState == CookState.Cooked && _cookingTime >= _timeToBurn)
            {
                _cookState = CookState.Burned;
                OnBurned?.Invoke();
            }

            yield return new WaitForSeconds(0.1f);
            _cookingTime += 0.1f;
        }

        _cookRoutine = null;
    }
}
