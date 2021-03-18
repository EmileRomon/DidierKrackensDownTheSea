using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Player

    [SerializeField] PlayerController _playerController;
    private int _day; //TODO: use the one in playercontroller

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



    #endregion

    #region Zones

    private Zone[] _zones;

    #endregion

    #region BoatLists

    [SerializeField] DraggableBoatList _playerList;

    #endregion

    #region Unity

    public void Awake()
    {
        _day = 0;

        _zones = FindObjectsOfType<Zone>();

        Debug.Assert(_price != 0, "Price of fish no set");
        Debug.Assert(_dailyDebt != 0, "Daily debt not set");
    }

    #endregion

    #region EndDay

    private void CalculateProfit()
    {
        _profit = 0;
        foreach(Zone z in _zones)
        {
            _profit += z.GetMoney()*_price;
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
        foreach(Zone zone in _zones)
        {
            zone.DecayFromBoats();
        }
    }

    private void DecayNatural()
    {
        foreach(Zone zone in _zones)
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
        Debug.Log("Day " + _day);
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

    public void EndDay()
    {
        PrintDebug();

        //if _playerCOntrooler.Money<0 then gameover

        //LaunchEvents();

        _day++;

        CalculateProfit();
        CalculateCost();

        //todo:damage boats

        DecayFromBoats();
        DecayNatural();

        //todo:display le resume

        _playerController.AddToMoneyAmount(_profit);
        _playerController.AddToMoneyAmount(_cost *= -1);

        //todo: if money < 0 ?

        PutBackBoat();
        UpdateView();
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
        //TODO
        switch (outcome.TargetResource)
        {
            case ResourceType.Boat:
                break;
            case ResourceType.Crew:
                break;
            case ResourceType.Health:
                if (outcome.AffectOtherInZone)
                {
                    foreach (Boat b in target.CurrentZone.PlacedBoats)
                    {
                        b.CurrentHealth -= outcome.Value;
                    }
                }
                else
                {
                    target.CurrentHealth -= outcome.Value;
                }
                break;
            case ResourceType.Money:
                break;
            default:
                break;
        }
    }

    #endregion Event
    #endregion
}
