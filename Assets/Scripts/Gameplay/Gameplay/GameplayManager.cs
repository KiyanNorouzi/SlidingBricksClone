using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;
    public bool playing = false;
    public bool lose = false;
    [Range(.2f, 2)] public float blocksSpeed = 1;
    public List<Color> colors = new List<Color>();
    public int lastBlock = -9;
    private bool speedUpMode = false;

    [Header("Blocks")] public Transform blocks;

    [Header("Score")] 
    public int score = 0;
    public int highScore = 0;
    [SerializeField] private TextMeshProUGUI scoreText;

    public Action OnPlay;
    public Action OnLose;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        score = 0;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        
        //SpeedUp every 3 sec
        InvokeRepeating(nameof(SpeedUp), 1, .5f);
    }

    public IEnumerator SpeedUpOnBottomOfScreen()
    {
        if (speedUpMode) yield break;

        speedUpMode = true;

        float normalSpeed = blocksSpeed;

        blocksSpeed = 6f;

        yield return new WaitForSeconds(.6f);

        blocksSpeed = normalSpeed;

        speedUpMode = false;
    }

    private void Update()
    {
        //Play
        if (Input.GetKeyDown(KeyCode.Mouse0) ||
            Input.GetKeyDown(KeyCode.LeftArrow) ||
            Input.GetKeyDown(KeyCode.RightArrow))
            if (!lose && !playing)
            {
                playing = true;

                OnPlay?.Invoke();
            }

        if (playing && !lose)
            blocks.Translate(0, blocksSpeed * Time.deltaTime, 0, Space.World);
    }

    #region Score

    public void AddScore()
    {
        score += 1;
        scoreText.text = score.ToString();
    }

    #endregion

    public List<Color> GetRandomColors()
    {
        return Utils.ShuffleList(colors);
    }

    private void SpeedUp()
    {
        if (!playing) return;

        blocksSpeed += .01f;

        if (lose)
            blocksSpeed = 0f;
    }

    public void Lose()
    {
        lose = true;
        playing = false;
        
        OnLose?.Invoke();

        CancelInvoke(nameof(SpeedUp));
    }
}