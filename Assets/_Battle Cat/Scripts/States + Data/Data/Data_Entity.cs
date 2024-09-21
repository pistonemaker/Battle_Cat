using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "Data/Entity Data")]
public class Data_Entity : ScriptableObject
{
    public float maxHP;
    public float speed;
    public float attackRange;
    public float attackRadius;
    public float damage;
    public float knockbackStrength;

    public LayerMask groundLayer;
    public LayerMask opponentLayer;
}
