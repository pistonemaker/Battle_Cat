using UnityEngine;

[CreateAssetMenu(fileName = "Projectile Data", menuName = "Data/Projectile")]
public class ProjectileData : ScriptableObject
{
    public Vector2 velocity;
    public float activeTime;
    public int damage;
    public string damageTag;
    public float damageradius;
}
