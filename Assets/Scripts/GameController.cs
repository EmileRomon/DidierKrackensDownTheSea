using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Player

    [SerializeField] PlayerController _playerController;
    public PlayerController Player => _playerController;
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

    [SerializeField] private MiniGameController _prefabGame;


    #endregion

    #region Zones

    private List<Zone> _zones;
    private List<Zone> _zonesRemoved = new List<Zone>();
    public List<Zone> Zones => _zones;
    public List<Zone> ZonesRemoved => _zonesRemoved;

    private void RemoveDeadZones()
    {
        _zonesRemoved.Clear();
        for (int i = _zones.Count - 1; i >= 0; i--)
        {
            if (_zones[i].CurrentHealth <= 0f)
            {
                _zones[i].gameObject.SetActive(false);
                _zonesRemoved.Add(_zones[i]);
                _zones.RemoveAt(i);
            }
        }
    }

    #endregion

    #region BoatLists

    [SerializeField] DraggableBoatList _playerList;
	[SerializeField] DraggablePlayerBoatContainer _playerBoatContainer;

	#endregion

	#region Unity

	[SerializeField]  private CanvasGroup _canvasGroup;

    public void Awake()
    {
        //_day = 0;

        _zones = new List<Zone>(FindObjectsOfType<Zone>());

        Debug.Assert(_price != 0, "Price of fish no set");
        Debug.Assert(_dailyDebt != 0, "Daily debt not set");

        _eventUIManager.OnDisplayEnd.AddListener(LateEndDay);
        StartDay();
    }

    #endregion

    #region EndDay

    [SerializeField] private GameOverManager _gameOver;
    [SerializeField] private EndDayRecap _endDayRecap;


    //the numeber of fish fished
    private int _minigameScore;
    public int MiniGameScore { get { return _minigameScore; } set { _minigameScore = value; } }

    private void CalculateProfit()
    {
        _profit = 0;
        foreach (Zone z in _zones)
        {
            _profit += z.GetMoney(_price);
        }
    }

    private void CalculateCost()
    {
        _cost = 0;
        foreach (KeyValuePair<string, List<CrewMember>> pair in _playerController.CrewMembers)
        {
            foreach (CrewMember member in pair.Value)
            {
                Debug.LogWarning(member + " " + member.Descriptor.MaintenancePrice);
                _cost += member.Descriptor.MaintenancePrice;
            }
        }

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
                boat.CurrentZone = null;
                _playerController.AvailableBoats.Add(boat);
                while (boat.Crew.Count > 0)
                {
                    _playerController.RemoveMemberFromBoat(boat);
                }
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
        if (_playerController.CurrentZone != null)
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
            MiniGameController mgc = Instantiate(_prefabGame);
            //prefab will call EndDay at the end of minigame

            mgc.SetWeather(MiniGameController.MiniGameWeather.SUNNY); //todo
            mgc.SetTime(90); //1:30
        }
        else EndDay();
    }

    public void StartDay()
    {
        _endDayRecap.StartDay();
    }

    private void DamageBoats()
    {
        foreach(Zone z in _zones)
        {
            foreach(Boat b in z.PlacedBoats)
            {
                b.CurrentHealth -= z.Descriptor.DangerFactor * 25;
            }
        }
    }

    public void EndDay()
    {
        _canvasGroup.alpha=1;
        _canvasGroup.blocksRaycasts = true;

        _playerController.NextDay();

        CalculateProfit();
        CalculateCost();

        DamageBoats();

        DecayFromBoats();
        DecayNatural();
        PrintDebug();

        _playerController.AddToMoneyAmount(_profit);
        _playerController.AddToMoneyAmount(_cost *= -1);

        if (_playerController.MoneyAmount < 0f)
        {
            DisplayGameOver(GameOverType.NoMoneyLeft);
        }

        LaunchEvents();
		_playerController.CurrentZone = null;
		_playerBoatContainer.UpdateView();
    }

    private void LateEndDay()
    {
        PutBackBoat();

        UpdateView();
        RemoveDeadZones();
        if (_zones.Count <= 0)
        {
            DisplayGameOver(GameOverType.NoZoneleft);
        }

        _endDayRecap.EndDay();
        _endDayRecap.StartDay();
    }

    private void DisplayGameOver(GameOverType type)
    {
        _gameOver.gameObject.SetActive(true);
        _gameOver.DisplayGameOver(_playerController.MoneyScore, _playerController.CurrentDay, type);

    }

    #region Event
    [SerializeField] private EventUIManager _eventUIManager;

    private void LaunchEvents()
    {
        foreach (Zone z in _zones)
        {
            List<Event> events = new List<Event>(z.Descriptor.Events);
            foreach (Boat boat in z.PlacedBoats)
            {
                if (Random.value > z.Descriptor.NoEventProbability)
                {
                    Event e = _eventUIManager.LoadRandomEvent(events, boat, false);
                    events.Remove(e);
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
