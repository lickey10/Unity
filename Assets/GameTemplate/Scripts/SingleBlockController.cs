using UnityEngine;
using System.Collections;

public class SingleBlockController : MonoBehaviour
{
    private const float SCALE_SPEED= 10f;

    private bool _isDestroy;
    private Transform _transform;
    private SpriteRenderer _spriteRenderer;

    public bool IsDestroy
    {
        get { return _isDestroy; }
    }

    public Vector2 Position {
        get { return new Vector2(Mathf.RoundToInt(_transform.position.x), Mathf.RoundToInt(_transform.position.y));}
    }

    public Color Color
    {
        get { return _spriteRenderer.color; }
    }

    // Use this for initialization
	void Start ()
	{
	    _transform = GetComponent<Transform>();

	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (_isDestroy)
	        _transform.localScale = Vector3.Lerp(_transform.localScale, Vector3.zero, SCALE_SPEED*Time.deltaTime);

        if(_isDestroy && _transform.localScale.x < 0.05f && _transform.localScale.y < 0.05f && _transform.localScale.z < 0.05f)
            Destroy(gameObject);
	}

    public void Animte()
    {
        if (!_isDestroy)
            _isDestroy = true;
    }

    public void SetColor(Color colorBlock)
    {
        if(_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();

        _spriteRenderer.color = colorBlock;
    }

    public void MouseDown()
    {
        _spriteRenderer.sortingOrder = 2;
        _transform.localPosition *= 1.25f;
    }

    public void MouseUp()
    {

        _spriteRenderer.sortingOrder = 1;
        _transform.localPosition /= 1.25f;
    }

    public void SetNewPosition(Transform[,] gridTransforms)
    {
        if (_transform == null)
            _transform = GetComponent<Transform>();

        var rowIndex = 8 - (Mathf.RoundToInt(_transform.position.y) + 4);
        var columnIndex = Mathf.RoundToInt(_transform.position.x) + 4;
        _transform.SetParent(gridTransforms[rowIndex, columnIndex]);
        _transform.localPosition = Vector3.zero;
        _transform.localScale = Vector3.one;
        _spriteRenderer.sortingOrder = 1;
    }
}
