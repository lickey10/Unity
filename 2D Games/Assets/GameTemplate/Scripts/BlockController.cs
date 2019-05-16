using UnityEngine;
using System.Collections;

public class BlockController : MonoBehaviour
{
    [SerializeField] private Vector2[] _singlieBlocksPositions;

    private GameLogic _gameLogic;
    private Transform _transform;
    private Vector3 _position;
    private bool _isClicked;
    private Camera _camera;
    private Color _color;
    private BlockType type;
    private SingleBlockController[] _singleBlockControllers;

	private GameObject audioObject;
    public Color ColorBlock

    {
        get { return _color; }
        set { _color = value; }
    }

    public BlockType Type
    {
        get { return type; }
        set { type = value; }
    }

    public Vector2 Position
    {
        get { return transform.position; }
    }

    public Vector2[] SinglieBlocksPositions
    {
        get { return _singlieBlocksPositions; }
    }

    // Use this for initialization
	void Start ()
	{
		audioObject = GameObject.Find("Main Camera");
	    _gameLogic = FindObjectOfType<GameLogic>();
	    _transform = GetComponent<Transform>();
        _transform.localScale = Vector3.one * 0.6f;
        _camera = Camera.main;

        _singleBlockControllers = new SingleBlockController[_transform.childCount];
	    for (var i = 0; i < _transform.childCount; i++)
	    {
	        _singleBlockControllers[i] = _transform.GetChild(i).GetComponent<SingleBlockController>();
	        _singleBlockControllers[i].SetColor(ColorBlock);
	    }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnMouseDown()
    {
        _position = _transform.localPosition;
        _transform.localScale = Vector3.one * 0.8f;
        for (var i = 0; i < _singleBlockControllers.Length; i++)
            _singleBlockControllers[i].MouseDown();
        _isClicked = true;
    }

    private void OnMouseDrag()
    {
        if (_isClicked)
        {
            var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            gameObject.transform.position = new Vector2(mousePosition.x, mousePosition.y + 1.5f);

        }
    }

    private void OnMouseUp()
    {
		if(PlayerPrefs.GetInt("SoundBoolean") == 0){
			audioObject.GetComponent<AudioSource>().Play ();
		}
        if (PositionIsGood())
        {
            _gameLogic.UiLogic.AddPoints(_singleBlockControllers.Length);
            for (int i = 0; i < _singleBlockControllers.Length; i++)
            {
                _singleBlockControllers[i].SetNewPosition(_gameLogic.GridTransforms);
            }
            //while (_transform.childCount > 0)
            //{
            //    var child = _transform.GetChild(0);
            //    var rowIndex = 8-(Mathf.RoundToInt(child.position.y) + 4);
            //    var columnIndex = Mathf.RoundToInt(child.position.x) + 4;
            //    child.SetParent(_gameLogic.GridTransforms[rowIndex, columnIndex]);
            //    child.localPosition = Vector3.zero;
            //    child.localScale = Vector3.one;
            //}

            _gameLogic.CheckGrid(gameObject);

        }
        else
        {
            _transform.localPosition = _position;
            _transform.localScale = Vector3.one * 0.6f;
            for (var i = 0; i < _singleBlockControllers.Length; i++)
                _singleBlockControllers[i].MouseUp();

        }
        
        _isClicked = false;

    }

    private bool PositionIsGood()
    {

        bool isGoodPosition = true;
        for (var i = 0; i < _transform.childCount; i++)
        {
            var rowIndex = 8 - (Mathf.RoundToInt(_transform.GetChild(i).position.y) + 4);
            var columnIndex = Mathf.RoundToInt(_transform.GetChild(i).position.x) + 4;
            if (Mathf.Abs(_transform.GetChild(i).position.x) >= 4.5f ||
                Mathf.Abs(_transform.GetChild(i).position.y) >= 4.5f ||
                _gameLogic.GridTransforms[rowIndex, columnIndex].childCount > 0)
                isGoodPosition = false;
        }
        return isGoodPosition;
    }
}
