using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Player

    [SerializeField] PlayerController _playerController;
    private int _day; //TODO: use the one in playercontroller

    #endregion

    #region Zones

    private Zone[] _zones;

    #endregion

    #region BoatLists

    [SerializeField] DraggableBoatList _playerList;
    private DraggableBoatList[] _boatsList; 

    #endregion

    #region Unity

    public void Awake()
    {
        _day = 0;

        _zones = FindObjectsOfType<Zone>();
        _boatsList = FindObjectsOfType<DraggableBoatList>();
    }

    #endregion

    #region EndDay

    private void CalculateProfit()
    {

    }

    private void CalculateCost()
    {

    }

    private void DecayFromBoats()
    {

    }

    private void DecayNatural()
    {

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

        foreach(DraggableBoatList dbl in _boatsList)
        {
            dbl.UpdateView();
        }
    }

    private void PrintDebug()
    {
        Debug.Log("Day " + _day);
        foreach(Zone z in _zones)
        {
            Debug.Log("In zone " + z.Descriptor.name + " from " + z.gameObject.name);
            foreach(Boat boat in z.PlacedBoats)
            {
                Debug.Log(" - " + boat.Descriptor.ItemName);
            }
        }

        Debug.Log("Boat not placed :");
        foreach(Boat boat in _playerController.AvailableBoats)
        {
            Debug.Log(" - " + boat.Descriptor.ItemName);
        }
    }

    public void EndDay()
    {
        PrintDebug();
        
        _day++;

        CalculateProfit();
        CalculateCost();

        DecayFromBoats();
        DecayNatural();

        PutBackBoat();
        UpdateView();
    }

    #endregion
}
