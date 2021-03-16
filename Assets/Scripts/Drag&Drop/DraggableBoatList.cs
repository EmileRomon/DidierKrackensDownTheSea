using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableBoatList : MonoBehaviour
{
    [SerializeField] private BoatDescriptor DEBUG_BOAT;

    [SerializeField] private GameObject _prefabListItem;

    //tmp, shoud be the list from zone or the list from the player
    private List<Boat> _list;

    public void Awake()
    {
        // populate
        _list = new List<Boat>();
    }

    public void AddBoat(Boat boat)
    {
        Debug.Log("Adding boat to " + gameObject.name);
        _list.Add(boat);

        GameObject go = Instantiate(_prefabListItem, transform);
        BoatListItem bli = go.GetComponent<BoatListItem>();
        Debug.Assert(bli != null);
        bli.SetBoat(boat);
    }

    public void RemoveBoat(Boat boat)
    {
        Debug.Log("Removing boat from " + gameObject.name);
        _list.Remove(boat);
    }

    [ContextMenu("Populate")]
    public void Populate()
    {
        for(int i=0;i<5;++i)
        {
            Boat boat = new Boat(Instantiate(DEBUG_BOAT));
            AddBoat(boat);
        }
    }

}
