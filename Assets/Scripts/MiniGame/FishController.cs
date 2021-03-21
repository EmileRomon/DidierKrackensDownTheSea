using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    private Vector3 _position;
    private Quaternion _rotation;
    private Animator _animator;

    private float _saveAnimSpeed;

    [SerializeField] Vector3 _offsetEscapePosition;
    [SerializeField] Vector3 _escapeRotation;
    private Quaternion _escapeQuaternion;

    private bool _escape;

    [SerializeField] float _speed;


    // Start is called before the first frame update
    void Start()
    {
        _position = transform.position;
        _rotation = transform.rotation;
        _animator = GetComponent<Animator>();

        _animator.speed = Random.Range(0.75f, 1.25f);
        _saveAnimSpeed = _animator.speed;
    }

    [ContextMenu("Fuite")]
    public void Escape()
    {
        _escape = true;
        transform.rotation = _escapeQuaternion;
        _animator.speed = 2;
    }

    public void ComeBack()
    {
        _escape = false;
        transform.rotation = _rotation;
        _animator.speed = _saveAnimSpeed;
    }

#if UNITY_EDITOR

    private void OnValidate()
    {
        _escapeQuaternion = Quaternion.Euler(_escapeRotation);
    }

#endif

    // Update is called once per frame
    void Update()
    {
        if (_escape) transform.position = Vector3.MoveTowards(transform.position, _position+ _offsetEscapePosition, Time.deltaTime * _speed);
        else transform.position = Vector3.MoveTowards(transform.position, _position, Time.deltaTime * _speed);
    }
}
