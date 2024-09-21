using DG.Tweening;
using UnityEngine;

public class MagicExplosion : MonoBehaviour
{
    private void OnEnable()
    {
        transform.SetParent(GameManager.Instance.projectTileTrf);
        transform.localScale = 10 * Vector3.one;
        transform.DOScale(Vector3.zero, 1f).OnComplete(() =>
        {
            PoolingManager.Despawn(gameObject);
        });
    }
}
