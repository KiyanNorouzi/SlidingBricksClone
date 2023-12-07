using System;
using DG.Tweening;
using UnityEngine;

public class MyBlock_Move : MonoBehaviour
{
    private MyBlock _block => GetComponent<MyBlock>();
    private Camera _camera;

    private float x, y;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (!GameplayManager.Instance.playing) return;
        if (GameplayManager.Instance.lose) return;

        Vector3 pos = transform.position;

        #region Mouse

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            float x = mousePosition.x;
            if (x < .5f) pos.x = 0;
            else if (x is > .5f and < 1.5f) pos.x = 1;
            else if (x is > 1.5f and < 2.5f) pos.x = 2;
            else if (x is > 2.5f and < 3.5f) pos.x = 3;
            else if (x is > 3.5f and < 4.5f) pos.x = 4;
            else if (x > 4.5) pos.x = 4;
        }

        #endregion

        #region ArrowKeys

        if (Input.GetKeyDown(KeyCode.LeftArrow)) pos.x -= 1;
        if (Input.GetKeyDown(KeyCode.RightArrow)) pos.x += 1;

        //Limitations
        if (pos.x < 0) pos.x = 0;
        if (pos.x > 4) pos.x = 4;

        #endregion

        //Move
        if (Math.Abs(transform.position.x - pos.x) > 0)
            MoveX(pos);

        //SpeedUp Role
        if (transform.position.y < -2)
            StartCoroutine(GameplayManager.Instance.SpeedUpOnBottomOfScreen());

        //Lose
        if (transform.position.y >= 4.5f)
        {
            GameplayManager.Instance.Lose();
        }
    }

    public void MoveY()
    {
        transform.DOKill();
        transform.DOLocalMoveY(transform.localPosition.y - 1, .15f).SetEase(Ease.Linear).OnComplete(() =>
        {
            _block.CheckForCollect();
        });
    }

    public void MoveX(Vector3 pos)
    {
        transform.DOKill();
        transform.DOMoveX(pos.x, .2f).SetEase(Ease.OutCirc).OnComplete(() => { _block.CheckForCollect(); });

        //x = pos.x;
    }
}