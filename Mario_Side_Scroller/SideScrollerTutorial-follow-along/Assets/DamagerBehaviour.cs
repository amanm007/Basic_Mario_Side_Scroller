using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagerBehaviour : MonoBehaviour
{
    public float damage = 20;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerBehaviour collidedPlayer = collision.gameObject.GetComponent<PlayerBehaviour>();
        if (collidedPlayer != null) collidedPlayer.Damage(damage);
    }
}
