
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public static List<Zombie> zombies = new List<Zombie>();

    private void Update()
    {
        foreach (var zombie in zombies)
        {
            zombie.CustomUpdate();
        }
    }
}
