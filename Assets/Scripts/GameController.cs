using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Player

    [SerializeField] PlayerController _playerController;
    //private int _day; //TODO: use the one in playercontroller

    /// <summary>
    /// Money gain today with the boats (and the event ?)
    /// </summary>
    private float _profit;
    public float Profit => _profit;

    /// <summary>
    /// Cost of the day (salary, debt,(event?) ect...)
    /// </summary>
    private float _cost;
    public float Cost => _cost;

    [Header("Price of a basic fish, impact every zone")]
    [SerializeField] private float _price;

    [Header("Debt, price to pay everyday")]
    [SerializeField] private float _dailyDebt;

    [SerializeField] private GameObject _prefabGame;
    [SerializeField] private bool _miniGame;



    #endregion

    #region Zones

    private List<Zone> _zones;

    private void RemoveDeadZones()
    {
        for (int i = _zones.Count - 1; i >= 0; i--)
        {
            if (_zones[i].CurrentHealth <= 0f)
            {
                _zones[i].gameObject.SetActive(false);
                _zones.RemoveAt(i);
            }
        }
    }

    #endregion

    #region BoatLists

    [SerializeField] DraggableBoatList _playerList;

    #endregion

    #region Unity

    public void Awake()
    {
        //_day = 0;

        _zones = new List<Zone>(FindObjectsOfType<Zone>());

        Debug.Assert(_price != 0, "Price of fish no set");
        Debug.Assert(_dailyDebt != 0, "Daily debt not set");
    }

    #endregion

    #region EndDay

    [SerializeField] private GameOverManager _gameOver;

    private void CalculateProfit()
    {
        _profit = 0;
        foreach (Zone z in _zones)
        {
            _profit += z.GetMoney() * _price;
        }
    }

    private void CalculateCost()
    {
        _cost = 0;
        //TODO: crew salary

        _cost += _dailyDebt;
    }

    private void DecayFromBoats()
    {
        foreach (Zone zone in _zones)
        {
            zone.DecayFromBoats();
        }
    }

    private void DecayNatural()
    {
        foreach (Zone zone in _zones)
        {
            zone.DecayNatural();
        }
    }

    private void PutBackBoat()
    {
        foreach (Zone z in _zones)
        {
            foreach (Boat boat in z.PlacedBoats)
            {
                _playerController.AvailableBoats.Add(boat);
            }
            z.PlacedBoats.Clear();
        }
    }

    private void UpdateView()
    {
        _playerList.UpdateView();
    }

    private void PrintDebug()
    {
        //Debug.Log("Day " + _day);
        Debug.Log("Day " + _playerController.CurrentDay);
        foreach (Zone z in _zones)
        {
            Debug.Log("In zone " + z.Descriptor.name + " from " + z.gameObject.name);
            foreach (Boat boat in z.PlacedBoats)
            {
                Debug.Log(" - " + boat.Descriptor.ItemName);
            }
        }

        Debug.Log("Boat not placed :");
        foreach (Boat boat in _playerController.AvailableBoats)
        {
            Debug.Log(" - " + boat.Descriptor.ItemName);
        }

        Debug.Log("Profit: " + _profit);
        Debug.Log("Cost : " + _cost);
    }

    public void ManageEndDay()
    {
        if (_miniGame)
        {
            GameObject _go = Instantiate(_prefabGame);
            //prefab will call EndDay at the end of minigame
        }
        else EndDay();
    }


    public void EndDay()
    {
        PrintDebug();

        //if _playerCOntrooler.Money<0 then gameover

        LaunchEvents();

        //_day++;
        _playerController.NextDay();

        CalculateProfit();
        CalculateCost();

        //todo:damage boats

        DecayFromBoats();
        DecayNatural();

        //todo:display le resume

        _playerController.AddToMoneyAmount(_profit);
        _playerController.AddToMoneyAmount(_cost *= -1);

        //todo: if money < 0 ?

        if (_playerController.MoneyAmount < 0f)
        {
            _gameOver.gameObject.SetActive(true);
            _gameOver.DisplayGameOver(_playerController.MoneyScore, _playerController.CurrentDay);
        }

        PutBackBoat();
        UpdateView();
        RemoveDeadZones();
    }

    #region Event
    [SerializeField] private EventUIManager _eventUIManager;

    private void LaunchEvents()
    {
        foreach (Zone z in _zones)
        {
            foreach (Boat boat in z.PlacedBoats)
            {
                if (Random.value > z.Descriptor.NoEventProbability)
                {
                    _eventUIManager.LoadRandomEvent(z.Descriptor.Events, boat, false);
                }
            }
        }
        _eventUIManager.DisplayAllEvents();
    }

    public void ApplyOutcomeEffect(EventOutcome outcome, Boat target)
    {
        switch (outcome.TargetResource)
        {
            case ResourceType.Boat:
                if (outcome.AffectOtherInZone)
                {
                    foreach (Boat b in target.CurrentZone.PlacedBoats)
                    {
                        _playerController.RemoveBoat(b);
                    }
                }
                else
                {
                    _playerController.RemoveBoat(target);
                }
                break;
            case ResourceType.Crew:
                if (outcome.AffectOtherInZone)
                {
                    foreach (Boat b in target.CurrentZone.PlacedBoats)
                    {
                        _playerController.KillMemberFromBoat(b);
                    }
                }
                else
                {
                    _playerController.KillMemberFromBoat(target);
                }
                break;
            case ResourceType.Health:
                if (outcome.AffectOtherInZone)
                {
                    foreach (Boat b in target.CurrentZone.PlacedBoats)
                    {
                        b.CurrentHealth += outcome.Value;
                    }
                }
                else
                {
                    target.CurrentHealth += outcome.Value;
                }
                break;
            case ResourceType.Money:
                float moneyGain = outcome.Value;
                if (outcome.AffectOtherInZone)
                {
                    moneyGain *= target.CurrentZone.PlacedBoats.Count;
                }
                _playerController.AddToMoneyAmount(moneyGain);
                break;
            case ResourceType.ZoneHealth:
                target.CurrentZone.CurrentHealth -= outcome.Value;
                break;
            default:
                break;
        }
    }

    #endregion Event
    #endregion
}
