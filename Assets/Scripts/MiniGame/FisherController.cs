using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FisherController : MonoBehaviour
{
    [SerializeField] private float _speed;

    [SerializeField] private GameObject _foundIndicator;
    [SerializeField] private GameObject _notFoundInicator;

    [SerializeField] private TMPro.TextMeshPro _textScore;

    [SerializeField] private float _minWait;
    [SerializeField] private float _maxWait;
    [SerializeField] private float _timeWindow;


    private int _score;
    public int Score => _score;

    private Animator _animator;
    private Vector3 _movement;

    private bool _fishing;
    private int _group;
    private float _waitingTime;
    private float _timer;

    private FishGroupController[] _fishes;
   

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _fishes = Object.FindObjectsOfType<FishGroupController>();

        _score = 0;

        _fishing = false;
        _group = -1;
    }

    private void BeginFishing()
    {
        _fishing = true;
        _animator.SetBool("Fishing", true);

        _group = -1;
        for(int i=0;i<_fishes.Length;++i)
        {
            if(_fishes[i].IsIn(transform.parent.position))
            {
                _group = i;
            }
        }

        _waitingTime = Random.Range(_minWait, _maxWait);
        _timer = 0;
    }

    private void GetFish()
    {
        _score++;
        _textScore.text = ""+_score;
        _fishes[_group].EscapeAll();
    }

    private void DontGetFish()
    {
        if(_group!=-1)
            _fishes[_group].EscapeAll();
    }

    private void EndFishing()
    {
        _fishing = false;
        _animator.SetBool("Fishing", false);

        if (_group!=-1 && _timer > _waitingTime && _timer < (_waitingTime + _timeWindow))
        {
            GetFish();
        }
        else DontGetFish();

        _foundIndicator.SetActive(false);
        _notFoundInicator.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        _movement = Vector3.zero;
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            _movement.x -= _speed*Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            _movement.x += _speed * Time.deltaTime;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            BeginFishing();
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            EndFishing();
        }

        if (_movement.x < 0 && transform.position.x <= -8)
            _movement.x = 0;
        else if (_movement.x > 0 && transform.position.x >= 8)
            _movement.x = 0;

        if (_fishing)
        {
            if((_timer+=Time.deltaTime)>=_waitingTime)
            {
                if (_group != -1 && _fishes[_group].Dispo) _foundIndicator.SetActive(true);
                else _notFoundInicator.SetActive(true);
            }
        }

        transform.parent.position += _movement;

        _animator.SetBool("Moving", _movement != Vector3.zero);
    }
}
