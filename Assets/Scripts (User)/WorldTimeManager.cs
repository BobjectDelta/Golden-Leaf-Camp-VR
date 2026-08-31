using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldTimeManager : MonoBehaviour
{
    [SerializeField]
    private int _currentTime = 0;
    [SerializeField, Min(1)]
    private int _dayLength = 300;
    [SerializeField]
    private int _timeFlowRate = 1;
    [SerializeField]
    private int _sunriseTime = 0;
    [SerializeField]
    private int _sunsetTime;

    public int TimeFlowRate => _timeFlowRate;
    public int DayLength => _dayLength;

    [SerializeField]
    private bool _isTimePaused = false;

    public static WorldTimeManager Instance { get; private set; }

    private List<IWorldTimeListener> _worldTimeListeners = new List<IWorldTimeListener>();
    private Coroutine _timeFlowRoutine;

    enum TimeOfDay {Day, Night};
    private TimeOfDay _timeOfDay = TimeOfDay.Night;

    public UnityEvent OnSunrise;
    public UnityEvent OnSunset;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void Start()
    {
        StartTimeFlow();
    }

    private void OnValidate()
    {
        _sunriseTime = Mathf.Clamp(_sunriseTime, 0, _dayLength);
        _sunsetTime = Mathf.Clamp(_sunsetTime, 0, _dayLength);
    }

    public void ToggleTimePause()
    {
        if (_isTimePaused)
            StartTimeFlow();
        else
            StopTimeFlow();
    }

    public void StartTimeFlow()
    {
        Debug.Log("Trying to start time!");
        if (_timeFlowRoutine == null)
            _timeFlowRoutine = StartCoroutine(ProgressTime());
    }

    public void StopTimeFlow()
    {
        Debug.Log("Trying to stop time!");
        if (_timeFlowRoutine != null)
        {
            StopCoroutine(_timeFlowRoutine);
            _timeFlowRoutine = null;
        }
    }

    IEnumerator ProgressTime()
    {
        while (!_isTimePaused)
        {
            _currentTime += _timeFlowRate;

            if (_currentTime >= DayLength)
                _currentTime = 0;

            Debug.Log(_timeOfDay);

            if (_currentTime >= _sunriseTime && _currentTime <= _sunsetTime && _timeOfDay == TimeOfDay.Night)
            {
                OnSunrise?.Invoke();
                _timeOfDay = TimeOfDay.Day;
            }
            else if ((_currentTime >= _sunsetTime || _currentTime < _sunriseTime) &&  _timeOfDay == TimeOfDay.Day)
            {
                OnSunset?.Invoke();
                _timeOfDay = TimeOfDay.Night;
            }

            foreach (IWorldTimeListener listener in _worldTimeListeners)
                listener.OnTimeChanged(_currentTime);

            //Debug.Log("Current time: " + _currentTime);

            yield return new WaitForSeconds(1);
        }

        _timeFlowRoutine = null;
    }

    public void Register(IWorldTimeListener listener)
    {
        _worldTimeListeners.Add(listener);
    }

    public void Unregister(IWorldTimeListener listener)
    {
        _worldTimeListeners.Remove(listener);
    }

}
