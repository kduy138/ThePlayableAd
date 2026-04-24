using UnityEngine;

public class BaseSpawner : MonoBehaviour, ISpawner
{
    public virtual void SpawnObject()
    {
        Debug.Log("If you are seeing this, there is a bug!");
    }
}
