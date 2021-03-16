using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoatListItem : MonoBehaviour
{
    protected Boat _boat;

    virtual public void SetBoat(Boat boat)
    {
        _boat = boat;
    }
}
