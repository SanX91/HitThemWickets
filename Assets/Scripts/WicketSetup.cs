using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WicketSetup : MonoBehaviour
{
    public new BoxCollider collider;
    public List<Rigidbody> wickets, bails;

    List<Vector3> initWktPositions, initBailPositions;

    private void Awake()
    {
        initBailPositions = new List<Vector3>();
        initWktPositions = new List<Vector3>();

        foreach (Rigidbody bail in bails)
        {
            initBailPositions.Add(bail.transform.position);
        }

        foreach (Rigidbody wicket in wickets)
        {
            initWktPositions.Add(wicket.transform.position);
        }
    }

    private void Start()
    {
        Initialize();
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach(Rigidbody bail in bails)
        {
            bail.isKinematic = false;
        }

        collider.enabled = false;
        EventManager.Instance.TriggerEvent(new EndBallEvent(true));
    }

    public void Initialize()
    {
        for(int i=0; i<bails.Count; i++)
        {
            bails[i].isKinematic = true;
            bails[i].transform.position = initBailPositions[i];
            bails[i].transform.rotation = Quaternion.identity;
        }

        for (int i = 0; i < wickets.Count; i++)
        {
            wickets[i].velocity = wickets[i].angularVelocity = Vector3.zero;
            wickets[i].transform.position = initWktPositions[i];
            wickets[i].transform.rotation = Quaternion.identity;
        }

        collider.enabled = true;
    }
}
