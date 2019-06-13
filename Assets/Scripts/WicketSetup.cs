using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WicketSetup : MonoBehaviour
{
    public List<Rigidbody> wickets, bails;

    private void OnTriggerEnter(Collider other)
    {
        foreach(Rigidbody bail in bails)
        {
            bail.isKinematic = false;
        }
    }
}
