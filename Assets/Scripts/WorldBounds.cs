using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldBounds : MonoBehaviour
{
    [SerializeField] Collider2D worldBounds;
    private void Update()
    {
        if (Hero.Instanse.gameObject.GetComponent<Collider2D>().IsTouching(worldBounds))
        {
            Hero.Instanse.GetDamage(Hero.Instanse.herolives);
        }
    }
}
