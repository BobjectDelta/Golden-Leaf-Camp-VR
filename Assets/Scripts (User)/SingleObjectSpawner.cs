using UnityEngine;
using UnityEngine.Events;

public class SingleObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform _spawnPoint;
    [SerializeField]
    private GameObject _objectPrefab;

    private GameObject _currentObject;

    [SerializeField]
    private UnityEvent OnSpawned;

    private void Start()
    {
        SpawnObject();                    
    }

    private void OnTriggerExit(Collider other)
    {
        var gameObject = other.gameObject;
        if (gameObject == _currentObject)
        {
            _currentObject = null;
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        if (_currentObject == null)
        {
            _currentObject = GameObject.Instantiate(_objectPrefab, _spawnPoint.position, Quaternion.identity);
            OnSpawned?.Invoke();
        }
    }
}
