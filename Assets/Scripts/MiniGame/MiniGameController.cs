using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameController : MonoBehaviour
{
    public enum MiniGameWeather { SUNNY, RAINY };

    [SerializeField] private Transform[] _weathers;
    [SerializeField] private FisherController _fisherController;

    private Timer _timer;

    private void Awake()
    {
        _timer = GetComponent<Timer>();
        _timer.AddCallback(StopGame);
    }

    public void SetWeather(MiniGameWeather weather)
    {
        for(int i=0;i<_weathers.Length;++i)
        {
            if ((MiniGameWeather)i == weather)
            {
                _weathers[i].gameObject.SetActive(true);
            }
            else _weathers[i].gameObject.SetActive(false);
        }
    }

    public void SetTime(float sec)
    {
        _timer.SetTime(sec);
    }

    private void StopGame()
    {
        GameController gc = FindObjectOfType<GameController>();
        gc.MiniGameScore = _fisherController.Score;
        gc.EndDay();

        Destroy(gameObject);
    }

    public void Update()
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _timer.SetTime(2);
        }
#endif
    }
}
