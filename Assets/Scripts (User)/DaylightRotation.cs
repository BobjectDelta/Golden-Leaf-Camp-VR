using UnityEngine;
using UnityEngine.UIElements;

public class DaylightRotation : MonoBehaviour, IWorldTimeListener
{
    [SerializeField] 
    private Transform _daylightTransform;

    [SerializeField]
    private float _currentAngle = 0;
    [SerializeField]
    private float _targetAngle;
    [SerializeField]
    private float _angleDelta;
    
    private void Start()
    {
        WorldTimeManager.Instance.Register(this);    
    }

    public virtual void OnTimeChanged(int time)
    {
        _angleDelta = Mathf.Repeat(360f / WorldTimeManager.Instance.DayLength * WorldTimeManager.Instance.TimeFlowRate, 360f);
        _targetAngle += _angleDelta; 
        //Debug.Log("Changed light's angle!");
    }

    void Update()
    {
        _currentAngle = Mathf.MoveTowardsAngle(_currentAngle, _targetAngle, _angleDelta * Time.deltaTime);

        _daylightTransform.rotation = Quaternion.Euler(_currentAngle, 0f, 0f);
    }

}
