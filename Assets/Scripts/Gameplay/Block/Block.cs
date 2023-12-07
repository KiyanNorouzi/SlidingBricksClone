using DG.Tweening;
using UnityEngine;

public class Block : MonoBehaviour
{
    public BlocksManager manager;
    private Collider2D _Collider => GetComponent<Collider2D>();
    private SpriteRenderer _sprite => GetComponent<SpriteRenderer>();

    public void SetColor(Color c, float duration = 0)
    {
        _sprite.DOKill();
        _sprite.DOColor(c, duration);
    }

    public Color GetColor()
    {
        return _sprite.color;
    }

    public void EnableCollider()
    {
        _Collider.enabled = true;
    }
    public void DisableCollider()
    {
        _Collider.enabled = false;
    }

    public void Hide()
    {
        DisableCollider();
        
        transform.DOKill();
        transform.DOScale(0, .1f);
    }
    
    public void Show()
    {
        transform.DOKill();
        transform.DOScale(3, 0).OnComplete(EnableCollider);
    }
}