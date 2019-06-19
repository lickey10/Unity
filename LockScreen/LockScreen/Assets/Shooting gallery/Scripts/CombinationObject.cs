using UnityEngine;
using System.Collections;

[System.Serializable]
public class CombinationObject {
	public int Row = -1;
	public int RowCounter = -1;

	public CombinationObject()
	{

	}

	public CombinationObject(int row, int rowCounter)
	{
		Row = row;
		RowCounter = rowCounter;
	}
}
