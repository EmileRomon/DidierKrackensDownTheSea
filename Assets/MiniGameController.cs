using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameController : MonoBehaviour
{
    public enum MiniGameWeather { SUNNY, RAINY };

    [SerializeField] private Transform[] _weathers;
   
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
}
