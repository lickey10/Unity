using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public enum BlockType
{
    TypeOne,
    TypeTwo,
    TypeThree,
    TypeFour,
    TypeFive,
    TypeSix,
    TypeSeven,
    TypeEight,
    TypeNine,
    TypeTen,
    TypeEleven,
    Single
}

[Serializable]
public class GameState2
{
    private static GameState2 _instance;

    public bool soundIsOn = true;
    public bool gameIsFinish = true;
    public int recordPoints = 0;
    public int currentPoints = 0;
    public BlockState[] blockStates = new BlockState[0];


    public static GameState2 GetInstance()
    {
        if (_instance == null)
            _instance = new GameState2();
        return _instance;
    }

    public void SetDate(GameState2 gameState)
    {
        soundIsOn = gameState.soundIsOn;
        gameIsFinish = gameState.gameIsFinish;
        recordPoints = gameState.recordPoints;
        currentPoints = gameState.currentPoints;
        Array.Resize(ref blockStates, gameState.blockStates.Length);
        Array.Copy(gameState.blockStates, blockStates, gameState.blockStates.Length);
    }

    [Serializable]
    public class BlockState
    {
        public int type;
        public float positionX;
        public float positionY;
        public float red;
        public float green;
        public float blue;

        public BlockState(BlockType blockType, Vector2 position, Color color)
        {
            type = (int) blockType;
            positionX = position.x;
            positionY = position.y;
            red = color.r;
            green = color.g;
            blue = color.b;
        }
    }

    public void SaveSingleBlocks(Transform[,] gridTransforms, List<GameObject> curreentBlocks)
    {
        var list = new List<SingleBlockController>();
        for (var i = 0; i < gridTransforms.GetLength(0); i++)
        {
            for (var j = 0; j < gridTransforms.GetLength(1); j++)
            {
                if (gridTransforms[i, j].childCount > 0)
                {
                    var singleBlockController = gridTransforms[i, j].GetChild(0).GetComponent<SingleBlockController>();
                    if (!singleBlockController.IsDestroy)
                    {
                        list.Add(singleBlockController);
                    }
                } 
            }
        }

        Array.Resize(ref blockStates, list.Count + curreentBlocks.Count);
        for (int i = 0; i < list.Count; i++)
        {
            blockStates[i] = new BlockState(BlockType.Single,  list[i].Position, list[i].Color);
        }
        var k = 0;
        for (int i = list.Count; i < blockStates.Length; i++)
        {
            var blockController = curreentBlocks[k].GetComponent<BlockController>();
            blockStates[i] = new BlockState(blockController.Type, blockController.Position, blockController.ColorBlock);
            k++;
        }

        gameIsFinish = false;
    }

    public void ClearArray()
    {
        gameIsFinish = true;
        Array.Clear(blockStates, 0, blockStates.Length);
    }
}
