using UnityEngine;

//Have struggles with this class. Feels better add functionality to NPCAI class
public class Subscriber : MonoBehaviour
{
    private Observer observer;

    private void Awake()
    {
        observer = FindObjectOfType<Observer>();
    }

    private void OnEnable()
    {
        observer.Subscribe(this);
    }

    private void OnDisable()
    {
        observer.UnSubscribe(this);
    }
}
