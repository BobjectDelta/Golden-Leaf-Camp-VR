using UnityEngine;
using UnityEngine.Events;

public class LaserSight : MonoBehaviour
{
    [SerializeField]
    private Material _laserMaterial;
    [SerializeField]
    private Color _laserColor = Color.red;
    [SerializeField]
    private LayerMask _layerMask;

    [SerializeField]
    private Transform _laserOrigin;
    [SerializeField]
    private float _maxDistance = 10f;

    private bool _laserEnabled = true;
    private bool _laserAllowed = false;
    private RaycastHit _raycastHit;

    LineRenderer _lineRenderer;

    public UnityEvent OnLaserToggled;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        if (_lineRenderer == null)
            _lineRenderer = gameObject.AddComponent<LineRenderer>();

        _lineRenderer.material = _laserMaterial;
        _lineRenderer.startColor = _laserColor;
        _lineRenderer.endColor = _laserColor;

        _lineRenderer.startWidth = 0.01f;
        _lineRenderer.endWidth = 0.01f;

        _lineRenderer.positionCount = 2;
        //_lineRenderer.SetPosition(0, _laserOrigin.position);
    }

    void Update()
    {
        _lineRenderer.enabled = _laserEnabled;

        if (!_laserEnabled)
            return;

        Vector3 start = _laserOrigin.position;
        Vector3 end;

        if (Physics.Raycast(start, _laserOrigin.forward, out RaycastHit hit, _maxDistance, _layerMask))
        {
            end = hit.point;
        }
        else
        {
            end = start + _laserOrigin.forward * _maxDistance;
        }

        _lineRenderer.SetPosition(0, start);
        _lineRenderer.SetPosition(1, end);   
        /*if (_laserEnabled)
        {
            if (Physics.Raycast(_laserOrigin.position, _laserOrigin.forward, out _raycastHit, _maxDistance, _layerMask))
            {
                _lineRenderer.SetPosition(0, _raycastHit.point);
            }
        }*/
    }

    public void TryToggleLaser()
    {
        if (_laserAllowed)
        {
            _laserEnabled = !_laserEnabled;
            OnLaserToggled?.Invoke();
        }
    }

    public void ToggleLaserActivation(bool isAllowed)
    {
        _laserAllowed = isAllowed;
    }
}
