using UnityEngine;

public class Arrow : Projectile
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(data.damageTag))
        {
            var damagable = other.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(data.damage);
            }
            PoolingManager.Despawn(gameObject);
        }
    }
}
