using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class RaycastShooting : MonoBehaviour
{
    [SerializeField] private Transform shotSpawnPoint;

    [SerializeField] private LayerMask targetLayerMask;

    [SerializeField] private GameObject visualFX;

    [SerializeField] private float fireRateDelay = 0.1f;

    private float nextFireTime = 0f;

    [Header("Events")]
    public UnityEvent OnShotFired;

    public void OnActivate()
    {
        TryShoot();
    }

    public void TryShoot()
    {
        if (Time.time < nextFireTime)
            return;

        Shoot();
        nextFireTime = Time.time + fireRateDelay;
        OnShotFired?.Invoke();
    }

    private void Shoot()
    {
        Debug.Log("XR Pew!");
        GameObject.Instantiate(visualFX, shotSpawnPoint);

        RaycastHit raycastHit;
        if (Physics.Raycast(shotSpawnPoint.position, shotSpawnPoint.forward, out raycastHit, Mathf.Infinity, targetLayerMask))
        {
            DecalPainter decalPainter = GetComponent<DecalPainter>();
            if (decalPainter != null)
                decalPainter.PaintDecal(raycastHit.point, raycastHit.normal);
            else
                Debug.LogError("DecalPainter is null!");
        }

        Debug.DrawRay(transform.position, shotSpawnPoint.forward * 10, Color.yellow);

    }
}
