using UnityEngine;

public class SnakeCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [Header("Offset")]
    [SerializeField] private float _height;

    [Header("Damping")]
    [SerializeField] private float _damping;

    private void FixedUpdate()
    {

        //Lerp
        var hight = _target.position  + _height * _target.up;
        var currentHight = Vector3.Lerp (transform.position, hight, _damping * Time.fixedDeltaTime);

        transform.position = currentHight;

        //Rotation
        transform.LookAt(_target, _target.forward);
    }
}
