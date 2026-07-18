using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxManager : MonoBehaviour, IWorldTimeListener
{
    [SerializeField]
    private Material _daySkybox;
    [SerializeField]
    private Material _nightSkybox;
    [SerializeField]
    private Material _currentSkybox;
    [SerializeField]
    private int _currentSkyboxIndex = 0;

    [SerializeField]
    private List<Material> _skyboxMaterials;
    [SerializeField]
    private float _skyboxBlendDuration = 1.0f;

    [SerializeField]
    private float _eveningTriggerTime = 0.7f;

    private Coroutine _skyboxSwitchingRoutine = null;

    private void Awake()
    {
        WorldTimeManager.Instance.Register(this);   
    }

    private void Start()
    {
        _currentSkybox = new Material(_skyboxMaterials[0]);
        RenderSettings.skybox = _currentSkybox;
    }

    public virtual void OnTimeChanged(float time) // example of using WorldTime listeners
    {
        if (time == _eveningTriggerTime)
        {
            Debug.Log("Triggering evening change!");
            SwitchToNextSkybox();
        }
    }

    public void SwitchToNextSkybox()
    {
        Debug.Log("Trying to change Skybox...");

        if (_skyboxSwitchingRoutine == null)
            _skyboxSwitchingRoutine = StartCoroutine(SwitchSkybox());

        Debug.Log("Skybox changed!");
    }

    IEnumerator SwitchSkybox()
    {
        Material startSkybox = new Material(_currentSkybox);
        Material targetSkybox = new Material(_skyboxMaterials[(_currentSkyboxIndex + 1) % _skyboxMaterials.Count]);

        float timeElapsed = 0;

        while (timeElapsed < _skyboxBlendDuration)
        {
            float t = timeElapsed / _skyboxBlendDuration;

            _currentSkybox.Lerp(startSkybox, targetSkybox, t);
            RenderSettings.skybox = _currentSkybox;
            
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        _currentSkyboxIndex++;
        _currentSkyboxIndex = _currentSkyboxIndex % (_skyboxMaterials.Count);

        RenderSettings.skybox = targetSkybox;
        _currentSkybox = new Material(targetSkybox);


        _skyboxSwitchingRoutine = null;
    }
}
