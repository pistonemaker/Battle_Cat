using System.Collections;
using Spine;
using Spine.Unity;
using TMPro;
using UnityEngine;
using Event = Spine.Event;

public class Entity : MonoBehaviour, IDamagable, IKnockbackable
{
    public Data_Entity dataEntity;
    public Projectile projectile;
    public Transform attackTrf;
    [SerializeField] private Transform groundCheck;

    public FSM stateMachine;
    public SkeletonAnimation anim;
    public Rigidbody2D rb;
    public TextMeshPro stateText;
    public Transform curTarget;

    protected MoveState moveState;
    protected AttackState attackState;
    protected HurtState hurtState;

    [SerializeField] protected Base myBase;
    [SerializeField] protected Base opponentBase;

    public Vector2 knockbackAngle;
    [SerializeField] private float curHP;
    private float maxHP;
    public int faceDirection;
    public int maxKnockbackCount = 3;
    public int knockbackCount;
    public int power;

    public bool isDead;
    public bool isHitLaze;

    public MoveState MoveState => moveState;

    public AttackState AttackState => attackState;

    public HurtState HurtState => hurtState;

    public virtual void Awake()
    {
        stateMachine = new FSM();
    }

    private void OnEnable()
    {
        transform.SetParent(GameManager.Instance.entityTrf);
        curHP = maxHP = dataEntity.maxHP;
        knockbackCount = 0;
        power = CountPower();
        isDead = false;
        isHitLaze = false;
        SetObjectData();
        anim = transform.GetChild(0).GetComponent<SkeletonAnimation>();
        rb = GetComponent<Rigidbody2D>();
        stateText = GetComponentInChildren<TextMeshPro>();
        anim.initialSkinName = "skin_default";
        anim.AnimationState.Event += HandleEvent;
    }

    public virtual void Update()
    {
        stateMachine.CurState.LogicUpdate();
        SetStateText();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.CurState.PhysicsUpdate();
    }

    private void SetStateText()
    {
        stateText.text = stateMachine.CurState.ToString();
    }

    public void SetBases(Base ourBase, Base enermyBase)
    {
        myBase = ourBase;
        opponentBase = enermyBase;
    }

    private void SetObjectData()
    {
        knockbackAngle = new Vector2(-1, 1);

        if (transform.rotation.y != 0f)
        {
            faceDirection = -1;
            myBase = GameManager.Instance.ourBase;
            opponentBase = GameManager.Instance.enermyBase;
        }
        else
        {
            faceDirection = 1;
            myBase = GameManager.Instance.enermyBase;
            opponentBase = GameManager.Instance.ourBase;
        }
    }

    public void SetVelocityX(float xVelo)
    {
        rb.velocity = new Vector2(xVelo * faceDirection, rb.velocity.y);
    }

    public void SetVelocityY(float yVelo)
    {
        rb.velocity = new Vector2(rb.velocity.x, yVelo);
    }

    public void SetVelocity(float velocityScale, Vector2 angle, int direction)
    {
        angle.Normalize();
        rb.velocity = new Vector2(angle.x * velocityScale * direction, angle.y * velocityScale);
    }

    public bool CheckIfNearestTarGetInRange()
    {
        curTarget = myBase.GetNearestTarget();

        if (Vector2.Distance(transform.position, curTarget.position) < dataEntity.attackRange)
        {
            return true;
        }

        return false;
    }

    public void ResetAttackTarget()
    {
        curTarget = myBase.GetNearestTarget();
    }

    public bool CheckIfAttackTargetInRange(Transform target)
    {
        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.position) < dataEntity.attackRange)
            {
                return true;
            }
        }

        return false;
    }

    // Quái sẽ knockback khi chết hoặc máu lần đầu bị giảm xuống dưới 2/3 và 1/3 
    public bool CheckIfKnockback()
    {
        // Debug.LogError(name + " " + curHP);
        if (curHP <= 0)
        {
            return true;
        }

        if (knockbackCount == maxKnockbackCount)
        {
            return false;
        }

        if (curHP / maxHP < 1f / maxKnockbackCount && knockbackCount == 1)
        {
            return true;
        }

        if (curHP / maxHP < 2f / maxKnockbackCount && knockbackCount == 0)
        {
            return true;
        }

        return false;
    }

    public bool CheckIfGrounded()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down,
            0.3f, dataEntity.groundLayer);
    }

    public void TakeDamage(float amount)
    {
        curHP -= amount;

        if (curHP <= 0)
        {
            curHP = 0;
            isDead = true;
        }

        if (CheckIfKnockback())
        {
            stateMachine.ChangeState(HurtState);
        }
    }

    public void Knockback(float strength, Vector2 angle, int direction)
    {
        curTarget = null;
        SetVelocity(strength, angle, direction);
    }

    public void KnockbackDefault()
    {
        knockbackCount++;
        Knockback(dataEntity.knockbackStrength, knockbackAngle, faceDirection);
    }

    public IEnumerator Dead()
    {
        yield return new WaitForSeconds(0.1f);
        myBase.entities.Remove(this);
        yield return new WaitForSeconds(0.5f);
        anim.AnimationState.ClearTrack(0);
        PoolingManager.Despawn(gameObject);
    }

    public void SpawnProjectile()
    {
        PoolingManager.Spawn(projectile, attackTrf.position, Quaternion.identity);
    }

    public IEnumerator HitLaze()
    {
        isHitLaze = true;
        yield return new WaitForSeconds(1f);
        isHitLaze = false;
    }

    private void HandleEvent(TrackEntry trackEntry, Event e)
    {
        if (e.Data.Name == "fire")
        {
            attackState.TriggerAttack();
        }
    }

    public int CountPower()
    {
        return (int)(maxHP + dataEntity.speed + dataEntity.attackRange * dataEntity.damage + dataEntity.attackRadius);
    }
}