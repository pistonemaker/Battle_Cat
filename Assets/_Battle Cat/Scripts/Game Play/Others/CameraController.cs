using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float x1;
    private float x2;
    private float duration = 0.75f;
    
    private void OnEnable()
    {
        x1 = GameManager.Instance.ourBase.transform.position.x - 2;
        x2 = GameManager.Instance.enermyBase.transform.position.x + 2;
        this.RegisterListener(EventID.Base_Destroyed, param => OnBaseDestroyed((Base)param));
    }
    private void OnDisable()
    {
        this.RemoveListener(EventID.Base_Destroyed, param => OnBaseDestroyed((Base)param));
    }

    private void OnBaseDestroyed(Base destroyedBase)
    {
        if (destroyedBase == GameManager.Instance.ourBase)
        {
            transform.DOMoveX(x1, duration).OnComplete(() =>
            {
                EventDispatcher.Instance.PostEvent(EventID.On_Player_Lose);
            });
        }
        else
        {
            transform.DOMoveX(x2, duration).OnComplete(() =>
            {
                EventDispatcher.Instance.PostEvent(EventID.On_Player_Win);
            });
        }
    }
}
