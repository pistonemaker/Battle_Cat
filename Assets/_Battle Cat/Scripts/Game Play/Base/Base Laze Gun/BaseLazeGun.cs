using System.Collections;
using UnityEngine;

public class BaseLazeGun : MonoBehaviour
{
    public LazeGunButton lazeGunButton;
    public LazeGunData data;

    public Transform lazeStartPos;
    public Transform lazeFirstEndPos;
    public Transform lazeLastEndPos;

    public float knockbackStrength;
    public Vector2 knockbackAngle;
    public int direction;

    private int lvDamage;
    private int lvRange;
    private int damage;
    private float range;
    private LineRenderer lineRenderer;
    private bool isLazeGunActive = false;

    public void Init()
    {
        lvDamage = 1;
        lvRange = 1;
        damage = data.damages[lvDamage - 1];
        range = data.ranges[lvRange - 1];
        SetObjectData();
        SetUpLaze();
        lazeGunButton.Init();
    }

    private void SetObjectData()
    {
        knockbackStrength = 10f;
        knockbackAngle = new Vector2(-1, 1);
        
        if (transform.parent.position.x > 0f)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
    }

    private void SetUpLaze()
    {
        lazeLastEndPos.position = new Vector3(lazeFirstEndPos.position.x - range,
            lazeLastEndPos.position.y, lazeStartPos.position.z);
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.3f;
    }

    private void OnEnable()
    {
        EventDispatcher.Instance.RegisterListener(EventID.Use_Laze_Gun, UseLazeGun);
    }
    
    private void OnDisable()
    {
        EventDispatcher.Instance.RemoveListener(EventID.Use_Laze_Gun, UseLazeGun);
    }

    private void UseLazeGun(object param)
    {
        if (!isLazeGunActive)
        {
            StartCoroutine(MoveLaze());
        }
    }

    private IEnumerator MoveLaze()
    {
        lineRenderer.enabled = true;
        isLazeGunActive = true;
        var lazeTime = 0f;
        var duration = 0.75f;
        var startPos = lazeFirstEndPos.position;
        var endPos = lazeLastEndPos.position;

        while (lazeTime < duration)
        {
            lazeTime += Time.deltaTime;
            var currentEndPos = Vector3.Lerp(startPos, endPos, lazeTime / duration);
            lineRenderer.SetPosition(0, lazeStartPos.position);
            lineRenderer.SetPosition(1, currentEndPos);

            // Raycast từ lazeStartPos tới currentEndPos
            var hits = Physics2D.RaycastAll(lazeStartPos.position,
                currentEndPos - lazeStartPos.position,
                (currentEndPos - lazeStartPos.position).magnitude);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.collider.CompareTag("Monster"))
                {
                    var entity = hit.collider.GetComponent<Entity>();
                    var damagable = hit.collider.GetComponent<IDamagable>();
                    var knockbackable = hit.collider.GetComponent<IKnockbackable>();

                    if (entity != null && !entity.isHitLaze)
                    {
                        StartCoroutine(entity.HitLaze());
                        if (damagable != null)
                        {
                            damagable.TakeDamage(damage);
                        }

                        if (knockbackable != null)
                        {
                            knockbackable.Knockback(knockbackStrength, knockbackAngle, direction);
                        }
                    }
                }
            }

            yield return null;
        }

        lineRenderer.SetPosition(1, endPos);
        lineRenderer.enabled = false;
        isLazeGunActive = false;
    }
}