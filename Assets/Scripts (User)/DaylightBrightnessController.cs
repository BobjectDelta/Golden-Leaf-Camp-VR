using System.Collections;
using UnityEngine;

public class DaylightBrightnessController : MonoBehaviour, IWorldTimeListener
{
    [SerializeField]
    private Light _daylight;
    [SerializeField]
    private float _targetBrightness = 0;
    [SerializeField]
    private float _switchDuration = 1f;
    [SerializeField]
    private int _nightSwitchTriggerTime;
    [SerializeField]
    private int _daySwitchTriggerTime;

    private Coroutine _brightnessSwitchRoutine = null;

    private void Start()
    {
        WorldTimeManager.Instance.Register(this);
    }

    public virtual void OnTimeChanged(int time) // another example of using WorldTime listeners
    {
        if (time == _nightSwitchTriggerTime || time == _daySwitchTriggerTime)
            TrySwitchBrightness();
    }

    public void TrySwitchBrightness()
    {
        Debug.Log("Trying to switch light brightness!");
        if (_brightnessSwitchRoutine == null)
            _brightnessSwitchRoutine = StartCoroutine(SwitchBrightness());
    }

    IEnumerator SwitchBrightness()
    {
        float elapsedTime = 0;
        float startBrightness = _daylight.intensity;

        while (elapsedTime <= _switchDuration)
        {
            float t = elapsedTime / _switchDuration;
            _daylight.intensity = Mathf.Lerp(startBrightness, _targetBrightness, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        _targetBrightness = startBrightness;

        _brightnessSwitchRoutine = null;
    }

}
