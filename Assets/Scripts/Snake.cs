using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Snake : Entity
{
    public UnityAction food—onsumed;

    [SerializeField] private Transform _apple;
    [SerializeField] private float _speedMove;
    [SerializeField] private float _gravity;
    [SerializeField] private VirtualJoystick _virtualJoystick;
    [SerializeField] private GameObject _segmentTailPref;

    [SerializeField] private float _offsetSegment;
    [SerializeField] private float _distanceSegmentTail;

    [SerializeField] private List<Transform> _segmentTail;

    private Rigidbody _rb;
    private Vector3 _gravityVector;

    #region UnityEvent
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        _gravityVector = collision.GetContact(0).normal;
    }

    private void OnCollisionStay(Collision collision)
    {
        _gravityVector = collision.GetContact(0).normal;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.TryGetComponent(out Eat eat))
        { 
             Destroy(eat.gameObject);
            var lastSegment = _segmentTail.Last();
            GameObject segmentTail = Instantiate(_segmentTailPref, new Vector3(lastSegment.position.x, lastSegment.transform.position.y, lastSegment.transform.position.z + _offsetSegment),Quaternion.identity);
            _segmentTail.Add(segmentTail.transform);
            food—onsumed?.Invoke();
        }
    }

    protected override void FixedUpdate()
    {
        MoveHead();

        MoveTail();
    }
    #endregion

    #region Metods
    private void MoveHead()
    {
        Quaternion rotation = Quaternion.FromToRotation(transform.up, _gravityVector) * transform.rotation;

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.fixedDeltaTime);


        _rb.AddForce(_gravityVector * _gravity);

        if (_virtualJoystick.Value.x > 0)
        {
            transform.rotation = MoveTurn(transform.right);
        }
        if (_virtualJoystick.Value.x < 0)
        {
            transform.rotation = MoveTurn(-transform.right);
        }

        _rb.MovePosition(_rb.position + transform.forward * _speedMove * Time.fixedDeltaTime);
    }

    private Quaternion MoveTurn(Vector3 toDirection)
    {
        Quaternion rotation = Quaternion.FromToRotation(transform.forward, toDirection) * transform.rotation;
        Quaternion directionTurn = Quaternion.Lerp(transform.rotation, rotation, Time.fixedDeltaTime);
        return directionTurn; 
    }
    
    private void MoveTail()
    {
        float distance = Mathf.Sqrt(_distanceSegmentTail);

        for (int i = 1; i < _segmentTail.Count; i++)
        {
            Vector3 previousPos = _segmentTail[i - 1].position;
            if ((_segmentTail[i].position - previousPos).sqrMagnitude > distance)
            {
                _segmentTail[i].position = Vector3.MoveTowards(_segmentTail[i].position, _segmentTail[i - 1].position,
                    _speedMove * Time.fixedDeltaTime);

                _segmentTail[i].rotation = Quaternion.Lerp(_segmentTail[i].rotation, _segmentTail[i - 1].rotation, 
                    2 * Time.fixedDeltaTime);
            }                
        }
    }
    #endregion
}
