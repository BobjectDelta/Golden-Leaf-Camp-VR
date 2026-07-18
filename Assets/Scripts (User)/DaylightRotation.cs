using UnityEngine;

public class DaylightRotation : MonoBehaviour
{
    [SerializeField] private Transform _daylightTransform;
    [SerializeField] private float _rotationSpeed = 1;


    void Update()
    {
        _daylightTransform.transform.Rotate(Vector3.right, _rotationSpeed * Time.deltaTime);
    }
}
