using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishGroupController : MonoBehaviour
{
    [SerializeField] private Vector2 _pos;
    [SerializeField] private float _timeComeBack;

    private float _timer;

    private FishController[] _fishes;

    private bool _dispo;
    public bool Dispo => _dispo;

    // Start is called before the first frame update
    void Start()
    {
        _fishes = GetComponentsInChildren<FishController>();
        _dispo = true;
        _timer = 0;
    }

    public bool IsIn(Vector2 position)
    {
        Debug.Log("Test " + position + " " + _pos);
        return position.x < _pos.y && position.x > _pos.x;
    }

    [ContextMenu("Fuite")]
    public void EscapeAll()
    {
        _dispo = false;
        _timer = 0;
        foreach(FishController fish in _fishes)
        {
            fish.Escape();
        }
    }

    [ContextMenu("ComeBack")]
    public void ComeBackAll()
    {
        _dispo = true;
        foreach(FishController fish in _fishes)
        {
            fish.ComeBack();
        }
    }

    private void Update()
    {
        if(!_dispo)
        {
            if((_timer+=Time.deltaTime)>=_timeComeBack)
            {
                ComeBackAll();
            }
        }
    }


#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(_pos.x, -10, 0), new Vector3(_pos.x,10,0));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(_pos.y, -10, 0), new Vector3(_pos.y, 10, 0));
    }

#endif
}
