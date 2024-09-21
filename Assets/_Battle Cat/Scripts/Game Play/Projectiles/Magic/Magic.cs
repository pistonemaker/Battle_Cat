using Unity.Mathematics;
using UnityEngine;

public class Magic : Projectile
{
    public MagicExplosion explosion;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(data.damageTag))
        {
            Vector2 collisionPoint = other.ClosestPoint(transform.position);
            PoolingManager.Spawn(explosion, collisionPoint, quaternion.identity);
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(collisionPoint, 1.5f);

            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.CompareTag(data.damageTag))
                {
                    var damagable = enemy.GetComponent<IDamagable>();
                    if (damagable != null)
                    {
                        damagable.TakeDamage(data.damage);
                    }
                }
            }
            
            PoolingManager.Despawn(gameObject);
        }
    }
}
