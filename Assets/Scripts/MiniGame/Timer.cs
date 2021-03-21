using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshPro _text;

    private bool _timerRunning;
    private float _time;

    private UnityEngine.Events.UnityAction _timerCallback;

    public void Awake()
    {
        _timerRunning = false;
    }

    public void SetTime(float sec)
    {
        _time = sec;
        _timerRunning = true;
    }

    public void AddCallback(UnityEngine.Events.UnityAction callback)
    {
        _timerCallback += callback;
    }

    private void UpdateView()
    {
        _text.text = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(_time / 60), Mathf.FloorToInt(_time % 60));
    }

    // Update is called once per frame
    void Update()
    {
        if(_timerRunning)
        {
            if(_time<=0)
            {
                _time = 0;
                _timerCallback.Invoke();
                _timerRunning = false;
            }
            else
            {
                _time -= Time.deltaTime;
            }

            UpdateView();
        }
    }
}
