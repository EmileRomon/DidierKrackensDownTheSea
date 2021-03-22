using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    #region Events
    [System.Serializable] public class ChangePlayerBoatListEvent : UnityEvent<Boat> { }

    [SerializeField] private ChangePlayerBoatListEvent _addingBoat;
    [SerializeField] private ChangePlayerBoatListEvent _removingBoat;

    #endregion

    #region Crew
    private Dictionary<string, List<Boat>> _boats = new Dictionary<string, List<Boat>>();
    private Dictionary<string, List<CrewMember>> _crewMembers = new Dictionary<string, List<CrewMember>>();

    private List<Boat> _availableBoats = new List<Boat>();
    private List<CrewMember> _availableCrewMembers = new List<CrewMember>();

    public List<Boat> AvailableBoats => _availableBoats;
    public List<CrewMember> AvailableCrewMembers => _availableCrewMembers;

    [SerializeField] private CrewMembersIndicator _crewMembersIndicator;
    [SerializeField] private GameInfo _gameInfo;

	public Zone CurrentZone { get; set; }

    private void Awake()
    {
        if (_gameInfo != null) _gameInfo.Player = this;
    }

    private void Start()
    {
        _scoresUIManager.UpdateDaysCounter(CurrentDay);
        _scoresUIManager.UpdateMoneyCounter(MoneyAmount);
        _scoresUIManager.UpdateScoreCounter(MoneyScore);
        _crewMembersIndicator.UpdateNumber(AvailableCrewMembers.Count);
    }

    public void AssignMemberToBoat(Boat boat)
    {
        int availableCount = _availableCrewMembers.Count;
        if (availableCount <= 0) return;

        CrewMember crewMember = _availableCrewMembers[availableCount - 1];
        crewMember.CurrentBoat = boat;
        _availableCrewMembers.RemoveAt(availableCount - 1);
        boat.Crew.Add(crewMember);

        _crewMembersIndicator?.UpdateNumber(availableCount - 1);
    }

    public void RemoveMemberFromBoat(Boat boat)
    {
        int boatCrewCount = boat.Crew.Count;
        if (boatCrewCount <= 0) return;

        int availableCount = _availableCrewMembers.Count;

        CrewMember crewMember = boat.Crew[boatCrewCount - 1];
        crewMember.CurrentBoat = null;
        _availableCrewMembers.Add(crewMember);
        boat.Crew.RemoveAt(boatCrewCount - 1);

        _crewMembersIndicator.UpdateNumber(availableCount + 1);
    }

    public void KillMemberFromBoat(Boat boat)
    {
        int boatCrewCount = boat.Crew.Count;
        if (boatCrewCount <= 0) return;
        boat.Crew.RemoveAt(boatCrewCount - 1);
    }

    public void AddBoat(BoatDescriptor descriptor)
    {
        Boat boat = new Boat(descriptor);
        if (_boats.ContainsKey(descriptor.ItemName))
        {
            List<Boat> boats = _boats[descriptor.ItemName];
            boats.Add(boat);
        }
        else
        {
            List<Boat> boats = new List<Boat>();
            boats.Add(boat);
            _boats.Add(descriptor.ItemName, boats);

        }
        _availableBoats.Add(boat);
        _itemsManager.CreateBoatItem(boat);

        _addingBoat.Invoke(boat);
    }

    public void AddCrewMember(CrewMemberDescriptor descriptor)
    {
        CrewMember crewMember = new CrewMember(descriptor);
        if (_crewMembers.ContainsKey(descriptor.ItemName))
        {
            List<CrewMember> members = _crewMembers[descriptor.ItemName];
            members.Add(crewMember);
        }
        else
        {
            List<CrewMember> members = new List<CrewMember>();
            members.Add(crewMember);
            _crewMembers.Add(descriptor.ItemName, members);
        }
        _availableCrewMembers.Add(crewMember);
        _crewMembersIndicator.UpdateNumber(_availableCrewMembers.Count);
        _itemsManager.CreateCrewItem(crewMember);
    }
    #endregion Crew

    #region Management
    [SerializeField] private ItemsManager _itemsManager;
    [SerializeField] private ScoresUIManager _scoresUIManager;
    private float _moneyScore;
    private float _moneyAmount;
    private int _currentDay = 1;

    public float MoneyScore => _moneyScore;
    public float MoneyAmount => _moneyAmount = 9999999;
    public int CurrentDay { get => _currentDay; set => _currentDay = value; }

    public void PayAllBoatsMaintenance()
    {
        foreach (KeyValuePair<string, List<Boat>> pair in _boats)
        {
            foreach (Boat boat in pair.Value)
            {
                if (boat.Descriptor.MaintenancePrice <= _moneyAmount)
                {
                    boat.Maintain();
                }
                else
                {
                    return;
                }
            }
        }
    }

    public void PayAllCrewMembersMaintenance()
    {
        foreach (KeyValuePair<string, List<CrewMember>> pair in _crewMembers)
        {
            foreach (CrewMember member in pair.Value)
            {
                if (member.Descriptor.MaintenancePrice <= _moneyAmount)
                {
                    member.Maintain();
                }
                else
                {
                    return;
                }
            }
        }
    }

    public void SellItem(ManagementItem mgmtItem)
    {
        float amountToSell = mgmtItem.Item.Sell();
        Debug.Log("Sold for " + amountToSell);
        AddToMoneyAmount(amountToSell);
        Boat boat = mgmtItem.Item as Boat;
        RemoveBoat(boat);
        CrewMember crew = mgmtItem.Item as CrewMember;
        RemoveCrew(crew);
    }

    public void RemoveBoat(Boat boat)
    {
        if (boat != null)
        {
            _boats[boat.Descriptor.ItemName].Remove(boat);
            if (_availableBoats.Contains(boat))
            {
                _availableBoats.Remove(boat);
            }
            _removingBoat.Invoke(boat);
        }
    }

    public void RemoveCrew(CrewMember crew)
    {
        if(crew != null)
        {
            if(crew.CurrentBoat != null)
            {
                RemoveMemberFromBoat(crew.CurrentBoat);
            }

            _crewMembers[crew.Descriptor.ItemName].Remove(crew);
            if(_availableCrewMembers.Contains(crew))
            {
                _availableCrewMembers.Remove(crew);
                _crewMembersIndicator?.UpdateNumber(_availableCrewMembers.Count);

            }

        }
    }

    public void PurchaseItem(ShopItem shopItem)
    {
        if (shopItem.ItemDescriptor.PurchasePrice <= _moneyAmount)
        {
            AddToMoneyAmount(-shopItem.ItemDescriptor.PurchasePrice);
            Debug.Log("Spent " + shopItem.ItemDescriptor.PurchasePrice + " coins");
            BoatDescriptor boatDescriptor = shopItem.ItemDescriptor as BoatDescriptor;
            if (boatDescriptor != null)
            {
                AddBoat(boatDescriptor);
            }
            else
            {
                AddCrewMember(shopItem.ItemDescriptor as CrewMemberDescriptor);
            }
        }
    }

    public void RepairItem(ManagementBoat mgmtItem)
    {
        Boat boat = mgmtItem.Item as Boat;
        if (boat != null)
        {
            float amountToPay = boat.Repair();
            if (_moneyAmount >= amountToPay)
            {
                Debug.Log("Repaired " + amountToPay);
                AddToMoneyAmount(-amountToPay);
                mgmtItem.UpdateItem();
            }
        }
    }

    public float AddToMoneyAmount(float value)
    {
        _moneyAmount += value;
        _scoresUIManager.UpdateMoneyCounter(_moneyAmount);
        if (value > 0f)
        {
            _moneyScore += value;
            _scoresUIManager.UpdateScoreCounter(_moneyScore);
        }
        return _moneyAmount;
    }

    public int NextDay()
    {
        _currentDay++;
        _scoresUIManager.UpdateDaysCounter(_currentDay);
        return CurrentDay;
    }

    #endregion Money

    #region Debug
    public List<BoatDescriptor> _boatDescriptors;
    public List<CrewMemberDescriptor> _crewDescriptors;

    [ContextMenu("Add items")]
    public void AddItems()
    {
        foreach (BoatDescriptor boat in _boatDescriptors) AddBoat(boat);
        foreach (CrewMemberDescriptor crew in _crewDescriptors) AddCrewMember(crew);
    }

	[ContextMenu("Current Zone")]
	public void PrintCurrentZone()
	{
		if (CurrentZone == null) Debug.Log("null");
		else Debug.Log(CurrentZone.Descriptor.ZoneName);
	}
    #endregion Debug
}
