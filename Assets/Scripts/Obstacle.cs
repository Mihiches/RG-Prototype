using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] int damage = 1;
    private void OnCollisionEnter2D (Collision2D collision)
    {
      if (collision.gameObject == Hero.Instanse.gameObject)
            Hero.Instanse.GetDamage(damage);
    }
}
