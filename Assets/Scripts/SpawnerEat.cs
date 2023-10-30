using UnityEngine;

public class SpawnerEat : MonoBehaviour
{

    [SerializeField] private Snake _snake;

    [SerializeField] private Entity _entityPrefab;

    [SerializeField] private int _numSpawns;

    [SerializeField] private MeshFilter _filtrMeshApple;

    private Mesh _meshApple;

    private Vector3[] _vectorPoints;

    #region UnityEvents
    private void Start()
    {
       _meshApple = _filtrMeshApple.GetComponent<MeshFilter>().mesh;
       _vectorPoints = _meshApple.vertices;

        _snake.food—onsumed += OnFoodConsumed;

        SpawnEntities();
    }

    private void OnDestroy()
    {
        _snake.food—onsumed -= OnFoodConsumed;
    }

    #endregion

    #region Metods
    private void OnFoodConsumed()
    {
        SpawnProperty();
    }

    private void SpawnEntities()
    {
        for (int i = 0; i < _numSpawns; i++)
        {
            SpawnProperty();
        }
    }

    private void SpawnProperty()
    {
        GameObject e = Instantiate(_entityPrefab.gameObject);

        e.transform.position = GetRandomPoint();
    }

    private Vector3 GetRandomPoint()
    {
        var asd = _vectorPoints[Random.Range(0, _vectorPoints.Length)];
        return asd;
    }
    #endregion
}
