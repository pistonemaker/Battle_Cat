using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected ProjectileData data;
    [SerializeField] protected Rigidbody2D rb;
    private float activeTime;
    
    private void OnEnable()
    {
        transform.SetParent(GameManager.Instance.projectTileTrf);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = data.velocity;
        activeTime = 0f;
    }

    private void Update()
    {
        activeTime += Time.deltaTime;
        if (activeTime > data.activeTime)
        {
            PoolingManager.Despawn(gameObject);
        }
    }
}
