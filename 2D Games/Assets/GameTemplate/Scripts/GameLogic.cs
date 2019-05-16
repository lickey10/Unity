using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters.Binary;
using Random = UnityEngine.Random;

public class GameLogic : MonoBehaviour
{
    private const int gridSize = 9;

    [SerializeField] private UILogic _uiLogic;

    [Header("Camera settings")]
    [SerializeField] private Camera _camera;
    [SerializeField] private Vector2 _defaultResolution;

    [Header("Game settings")]
    [SerializeField] private GameObject _grid;
    [SerializeField] private GameObject _singleBlock;
    [SerializeField] private GameObject[] _blocks;
    [SerializeField] private Color[] _colors;


    private Transform[,] _gridTransforms;
    private List<GameObject> _currentBlocks; 
    private string SavePath
    {
        get { return Path.Combine(Application.persistentDataPath, "GameState.data"); }
    }

    public Transform[,] GridTransforms
    {
        get { return _gridTransforms; }
    }

    public UILogic UiLogic
    {
        get { return _uiLogic; }
    }

    private void Awake()
    {
        LoadGameState();
    }

    // Use this for initialization
    void Start () {
        _currentBlocks = new List<GameObject>();
        _gridTransforms = new Transform[gridSize, gridSize];
	    for (var i = 0; i < _gridTransforms.GetLength(0); i++)
	        for (int j = 0; j < _gridTransforms.GetLength(1); j++)
	            _gridTransforms[i,j] = _grid.transform.GetChild(i* gridSize + j).transform;

        float normalAspect = _defaultResolution.x / _defaultResolution.y;
        Debug.Log(_camera.pixelWidth + " : " + _camera.pixelHeight);
        _camera.orthographicSize = _camera.orthographicSize * normalAspect / ((float)_camera.pixelWidth / _camera.pixelHeight);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// Play game or continue new game
    /// </summary>
    public void PlayGame()
    {
        _grid.SetActive(true);
        if (GameState2.GetInstance().gameIsFinish)
            PlayNewGame();
        else
            LoadOldGame();
    }

    public void PlayNewGame()
    {
        var tempBlocks = new List<GameObject>(_blocks);
        var tempColors = new List<Color>(_colors);

        for (var i = 0; i < 3; i++)
        {
            var blockIndex = Random.Range(0, tempBlocks.Count);
            var blockController = Instantiate(tempBlocks[blockIndex], new Vector3(-3 + 3*i, -5.75f), Quaternion.identity) as GameObject;
            var colorIndex = Random.Range(0, tempColors.Count);
            blockController.GetComponent<BlockController>().ColorBlock = tempColors[colorIndex];
            blockController.GetComponent<BlockController>().Type = (BlockType)blockIndex;
            tempBlocks.RemoveAt(blockIndex);
            tempColors.RemoveAt(colorIndex);
            
            _currentBlocks.Add(blockController);
        }
    }

    public void LoadOldGame()
    {
        for (int i = 0; i < GameState2.GetInstance().blockStates.Length; i++)
        {
            var color = new Color(
                    GameState2.GetInstance().blockStates[i].red,
                    GameState2.GetInstance().blockStates[i].green,
                    GameState2.GetInstance().blockStates[i].blue);
            var position = new Vector3(GameState2.GetInstance().blockStates[i].positionX,
                    GameState2.GetInstance().blockStates[i].positionY);
            if (GameState2.GetInstance().blockStates[i].type == (int) BlockType.Single)
            {
                var blockController = Instantiate(_singleBlock, position, Quaternion.identity) as GameObject;
                
                blockController.GetComponent<SingleBlockController>().SetColor(color);
                blockController.GetComponent<SingleBlockController>().SetNewPosition(GridTransforms);
            }
            else
            {
                var blockIndex = GameState2.GetInstance().blockStates[i].type;
                var blockController = Instantiate(_blocks[blockIndex], position, Quaternion.identity) as GameObject;
                blockController.GetComponent<BlockController>().ColorBlock = color;
                blockController.GetComponent<BlockController>().Type = (BlockType)blockIndex;

                _currentBlocks.Add(blockController);
            }
        }
    }
    public void SaveGameState ()
    {
        var formatter = new BinaryFormatter();
        var saveFile = File.Create(SavePath);
        formatter.Serialize(saveFile, GameState2.GetInstance());
        saveFile.Close();
    }

    public void LoadGameState()
    {
        if (File.Exists(SavePath))
        {
            var formatter = new BinaryFormatter();
            var saveFile = File.Open(SavePath, FileMode.Open);
            var gameState = (GameState2) formatter.Deserialize(saveFile);
            GameState2.GetInstance().SetDate(gameState);
            saveFile.Close();
        }
    }

    public void CheckGrid(GameObject blockController)
    {
        var destroyBlock = blockController;
        _currentBlocks.Remove(blockController);
        Destroy(destroyBlock);
        if(_currentBlocks.Count == 0)
            PlayNewGame();

        var rows = new List<int>();
        for (var i = 0; i < GridTransforms.GetLength(0); i++)
        {
            var rowIsFull = true;
            for (var j = 0; j < GridTransforms.GetLength(1); j++)
            {
                if (GridTransforms[i, j].childCount == 0)
                    rowIsFull = false;
            }
            if(rowIsFull)
                rows.Add(i);
        }

        var columns = new List<int>();
        for (var i = 0; i < GridTransforms.GetLength(0); i++)
        {
            var columnIsFull = true;
            for (var j = 0; j < GridTransforms.GetLength(1); j++)
            {
                if (GridTransforms[j, i].childCount == 0)
                {
                    columnIsFull = false;
                }
            }
            if (columnIsFull)
                columns.Add(i);
        }

        AnimateBlocks(rows, columns);
    }

    private void AnimateBlocks(List<int> rows, List<int> columns)
    {
        for (int i = 0; i < rows.Count; i++)
        {
            for (int j = 0; j < GridTransforms.GetLength(1); j++)
            {
                var singleBlockController = GridTransforms[rows[i], j].GetChild(0).GetComponent<SingleBlockController>();
                singleBlockController.Animte();
            }
        }

        for (int i = 0; i < columns.Count; i++)
        {
            for (int j = 0; j < GridTransforms.GetLength(0); j++)
            {
                var singleBlockController = GridTransforms[j, columns[i]].GetChild(0).GetComponent<SingleBlockController>();
                singleBlockController.Animte();
            }
        }

        if (!CheckIsFinishGame())
        {
            UiLogic.AddPoints(rows.Count*gridSize + columns.Count*gridSize);
            GameState2.GetInstance().SaveSingleBlocks(GridTransforms, _currentBlocks);
            SaveGameState();
        }
        else
        {
            for (var i = 0; i < GridTransforms.GetLength(0); i++)
            {
                for (var j = 0; j < GridTransforms.GetLength(1); j++)
                {
                    if (GridTransforms[j, i].childCount > 0)
                    {
                        Destroy(GridTransforms[j, i].GetChild(0).gameObject);
                    }
                }
            }

            while (_currentBlocks.Count > 0)
            {
                var block = _currentBlocks[0];
                _currentBlocks.RemoveAt(0);
                Destroy(block);
            }

            GameState2.GetInstance().ClearArray();
            SaveGameState();
            UiLogic.GameOver();
            _grid.SetActive(false);
        }
    }

    private bool CheckIsFinishGame()
    {
        for (int k = 0; k < _currentBlocks.Count; k++)
        {
            var singleBlockPositions = _currentBlocks[k].GetComponent<BlockController>().SinglieBlocksPositions;
            for (int i = 0; i < GridTransforms.GetLength(0); i++)
            {
                for (int j = 0; j < GridTransforms.GetLength(1); j++)
                {
                    if (GridTransforms[i,j].childCount == 0 && CheckOtherPosition(i, j, singleBlockPositions))
                    {
                        return false;
                    }
                }
            }

        }
        return true;
    }

    private bool CheckOtherPosition(int i, int j, Vector2[] singleBlockPositions)
    {
        for (var k = 0; k < singleBlockPositions.Length; k++)
        {
            var row = i + (int)singleBlockPositions[k].y;
            var column = j + (int)singleBlockPositions[k].x;
            if(row < 9 && column < 9)
                Debug.Log("ROW: " + row + " column: " + column +  " Child count: " + GridTransforms[row, column].childCount);
            if (row >= gridSize || column >= gridSize || GridTransforms[row, column].childCount > 0)
            {
                return false;
            }
        }
        return true;
    }
}
