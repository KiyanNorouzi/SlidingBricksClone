using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class MyBlock : Block
{
    public Block bellowBlock;
    public float bellowBlockLocalY;
    private MyBlock_Move _move => GetComponent<MyBlock_Move>();

    private void Start()
    {
        //Randomize Start position
        Vector3 pos = transform.position;
        pos.x = Random.Range(0, 5);
        transform.position = pos;

        //Color
        SetColor(GameplayManager.Instance.colors[Random.Range(0, GameplayManager.Instance.colors.Count)]);
        InvokeRepeating(nameof(UpdateColor), 0, 5);

        //Check for Collect
        GameplayManager.Instance.OnPlay += CheckForCollect;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        bellowBlock = other.GetComponent<Block>();
    }

    private void UpdateColor()
    {
        if (GameplayManager.Instance.lose) return;

        SetColor(GameplayManager.Instance.colors[Random.Range(0, GameplayManager.Instance.colors.Count)], .1f);

        Invoke(nameof(CheckForCollect), .2f);
    }

    public void CheckForCollect()
    {
        if (!GameplayManager.Instance.playing) return;
        if (GameplayManager.Instance.lose) return;

        if (bellowBlock.GetColor() == GetColor())
        {
            GameplayManager.Instance.AddScore();

            bellowBlockLocalY = bellowBlock.transform.localPosition.y;
            bellowBlock.manager.HideBlocks();
            bellowBlock = null;

            _move.MoveY();
        }
    }
}