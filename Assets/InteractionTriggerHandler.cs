using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTriggerHandler : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		SendMessageUpwards("InteractionEnter", other);
	}
	void OnTriggerExit(Collider other)
	{
		SendMessageUpwards("InteractionExit", other);
	}
}
