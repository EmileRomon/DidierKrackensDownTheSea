using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableBoatList : MonoBehaviour
{
    [SerializeField] protected GameObject _prefabListItem;

    [SerializeField] protected PlayerController _pc;
	public PlayerController Player => _pc;

    protected void Start()
    {
        if (_pc != null)
        {
            foreach (Boat b in _pc.AvailableBoats)
            {
                GameObject go = Instantiate(_prefabListItem, transform);
                BoatListItem bli = go.GetComponent<BoatListItem>();
                Debug.Assert(bli != null);
                bli.SetBoat(b);
            }
        }
    }

    /// <summary>
    /// Adding a new boat (used when purchasing a boat)
    /// </summary>
    /// <param name="boat"></param>
    public void AddNewBoat(Boat boat)
    {
        GameObject go = Instantiate(_prefabListItem, transform);
        BoatListItem bli = go.GetComponent<BoatListItem>();
        Debug.Assert(bli != null);
        bli.SetBoat(boat);
    }

    public void AddBoat(Boat boat)
    {
        AddNewBoat(boat);
        if (boat.CurrentZone == null && _pc != null) //if no zone, go back in player list
        {
            _pc.AvailableBoats.Add(boat);
        }
    }

    public void RemoveBoat(Boat boat)
    {
        _pc.AvailableBoats.Remove(boat);
        UpdateView();
    }

    [ContextMenu("Update")]
    public virtual void UpdateView()
    {
        for (int i = 0; i < transform.childCount;++i)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        if (_pc != null)
        {
            foreach (Boat b in _pc.AvailableBoats)
            {
                GameObject go = Instantiate(_prefabListItem, transform);
                BoatListItem bli = go.GetComponent<BoatListItem>();
                Debug.Assert(bli != null);
                bli.SetBoat(b);
            }
        }
    }
}
