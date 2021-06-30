using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    private readonly List<Subscriber> subscribers = new List<Subscriber>();

    public float minDistance;
    private float squaredDistance;

    private void Awake()
    {
        squaredDistance = Mathf.Sqrt(squaredDistance);
    }

    private void Update()
    {
        if (subscribers.Count < 2) return;

        CheckDistance();
    }

    public void Subscribe(Subscriber sub)
    {
        subscribers.Add(sub);
    }

    public void UnSubscribe(Subscriber sub)
    {
        subscribers.Remove(sub);
    }

    private void CheckDistance()
    {
        for (int i = 0; i < subscribers.Count; i++)
        {
            for (int j = i + 1; j < subscribers.Count; j++)
            {
                Subscriber go1 = subscribers[i];
                Subscriber go2 = subscribers[j];

                Vector3 toOtherObject = go1.transform.position - go2.transform.position;

                if (toOtherObject.sqrMagnitude < squaredDistance)
                {

                    TryToDestroy(go1, go2);
                }
            }
        }

    }

    private void TryToDestroy(Subscriber go1, Subscriber go2)
    {
        NPCAI npc1 = go1.GetComponent<NPCAI>();
        NPCAI npc2 = go2.GetComponent<NPCAI>();

        float lucksum = npc1.Luck + npc2.Luck;

        float randomNumber = Random.Range(0, lucksum);

        if (randomNumber >= npc1.Luck)
        {
            npc1.Die();
        }
        else
        {
            npc2.Die();
        }
    }

    //Test purposes
    private void OnValidate()
    {
        squaredDistance = Mathf.Sqrt(minDistance);
    }
}
