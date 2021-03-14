using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop", menuName = "Managers/Shop")]
public class ShopManager : ScriptableObject
{
    [SerializeField] private List<BoatDescriptor> _boatDescriptors;
	[SerializeField] private List<CrewMemberDescriptor> _crewMembersDescriptors;

	public List<BoatDescriptor> Boats => _boatDescriptors;
	public List<CrewMemberDescriptor> CrewMembers => _crewMembersDescriptors;
}
