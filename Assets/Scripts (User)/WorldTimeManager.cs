using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class WorldTimeManager : MonoBehaviour
{
    [SerializeField]
    private float _currentTime = 0;
    [SerializeField]
    private float _timeFlowRate = 0.005f;
    [SerializeField, Range(0, 1)]
    private float _sunriseTime = 0f;
    [SerializeField, Range(0, 1)]
    private float _sunsetTime = 0.5f;

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
            _currentTime = Mathf.Repeat(_currentTime + _timeFlowRate, 1f);
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

            Debug.Log("Current time: " + _currentTime);

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
