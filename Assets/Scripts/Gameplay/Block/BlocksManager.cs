using System.Collections.Generic;
using UnityEngine;

public class BlocksManager : MonoBehaviour
{
    public List<Block> blocks = new List<Block>();
    public List<Color> colors = new List<Color>();

    private void Start()
    {
        RandomizeBlocks();
    }

    private void RandomizeBlocks()
    {
        //Update Colors
        colors = GameplayManager.Instance.GetRandomColors();

        //Set Colors
        for (int i = 0; i < blocks.Count; i++)
            blocks[i].SetColor(colors[i]);
    }

    public void HideBlocks()
    {
        foreach (Block block in blocks)
        {
            block.Hide();
        }

        //Move Blocks Down
        Invoke(nameof(MoveBlocksDown), .2f);
    }

    private void MoveBlocksDown()
    {
        GameplayManager.Instance.lastBlock -= 1;

        Vector3 pos = transform.localPosition;
        pos.y = GameplayManager.Instance.lastBlock;
        transform.localPosition = pos;
        foreach (Block block in blocks)
        {
            block.Show();
        }
    }
}