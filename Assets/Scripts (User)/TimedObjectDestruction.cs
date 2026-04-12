using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedObjectDestruction : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 1f;

    private float _destructionTime = 0f;

    private void Awake()
    {
        _destructionTime = Time.time + _lifeTime;
    }

    void Update()
    {
        if (_destructionTime < Time.time) 
            Destroy(gameObject);
    }

    public void SetLifeTime(float lifeTime)
    {
        _lifeTime = lifeTime;
    }
}
