using UnityEngine;

public class Graph : MonoBehaviour
{
    #region --Fields / Properties--
    
    [SerializeField, Tooltip("Optional coefficient for the selected GraphType.")]
    private float _coefficient;

    [SerializeField, Tooltip("Optional Y-axis intercept for the selected GraphType.")]
    private float _yIntercept;
    
    [SerializeField, Tooltip("Desired GraphType to be drawn.")]
    private GraphType _currentGraphType;
    
    [SerializeField, Tooltip("How fast the graph should be drawn.")]
    private float _graphSpeed = 1;

    [SerializeField, Range(2, 20), Tooltip("Desired input value range for the selected GraphType.")]
    private int _inputRange = 2;
    
    /// <summary>
    /// Cached Transform component.
    /// </summary>
    private Transform _transform;

    /// <summary>
    /// Displays the path of the selected GraphType.
    /// </summary>
    private LineRenderer _lineRenderer;
    
    /// <summary>
    /// Tracks current number of positions used by the line renderer.
    /// </summary>
    private int _index;
    
    /// <summary>
    /// Controls graph output between _maxInputValue and _minInputValue.
    /// </summary>
    private bool _changeDirection;
    
    /// <summary>
    /// Current position of the graphing node (this game object).
    /// </summary>
    private Vector3 _movePosition;
    
    /// <summary>
    /// Keeps track of the previously selected GraphType to allow for runtime changes between types.
    /// </summary>
    private GraphType _previousGraphType;
    
    /// <summary>
    /// The largest input value for the selected GraphType.
    /// </summary>
    private int _maxInputValue;

    /// <summary>
    /// The smallest input value for the selected GraphType.
    /// </summary>
    private float _minInputValue;
    
    /// <summary>
    /// The current input value passed into the selected GraphType.
    /// </summary>
    private float _inputValue;

    /// <summary>
    /// Tracks when a GraphType switch has been detected.
    /// </summary>
    private bool _isChangingGraphType;
    
    /// <summary>
    /// Pauses graphing for the specified time when a GraphType switch has been detected.
    /// </summary>
    private float _resetDelayTime = 1f;
    
    /// <summary>
    /// Tracks the amount of time passed when a GraphType switch has been detected.
    /// </summary>
    private float _resetDelayTimer;
    
    /// <summary>
    /// Enables/disables visibility of the graphing node (this game object).
    /// </summary>
    private MeshRenderer _meshRenderer;
    
    #endregion
    
    #region --Unity Specific Methods---

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        CheckGraph();
        UpdateInput();
        DrawGraph();
    }
    
    #endregion
    
    #region --Custom Methods--
    
    /// <summary>
    /// Initializes variables and caches components.
    /// </summary>
    private void Init()
    {
        _transform = transform;
        _lineRenderer = GetComponent<LineRenderer>();
        _meshRenderer = GetComponent<MeshRenderer>();
        SwitchGraphType();
    }

    /// <summary>
    /// Checks the current graph behavior for any changes.
    /// </summary>
    private void CheckGraph()
    {
        if (_isChangingGraphType)
        {
            _resetDelayTimer += Time.deltaTime;
        }

        if (_resetDelayTimer > _resetDelayTime)
        {
            _isChangingGraphType = false;
            _resetDelayTimer = 0;
        }
        
        if (_previousGraphType != _currentGraphType)
        {
            ResetGraph();
            SwitchGraphType();
        }
    }

    /// <summary>
    /// Switches to a different GraphType.
    /// </summary>
    private void SwitchGraphType()
    {
        _previousGraphType = _currentGraphType;
        switch (_currentGraphType)
        {
            case GraphType.Line:
                _maxInputValue = _inputRange;
                _minInputValue = -_inputRange;
                break;

            case GraphType.Squared:
                _maxInputValue = _inputRange;
                _minInputValue = -_inputRange;
                break;

            case GraphType.Cubed:
                _maxInputValue = _inputRange;
                _minInputValue = -_inputRange;
                break;

            case GraphType.SquareRoot:
                _maxInputValue = _inputRange;
                _minInputValue = 0.1f;
                break;

            case GraphType.Sine:
                _maxInputValue = _inputRange;
                _minInputValue = -_inputRange;
                break;

            case GraphType.Cosine:
                _maxInputValue = _inputRange;
                _minInputValue = -_inputRange;
                break;
        }
    }

    /// <summary>
    /// Prepares a new graph for the change in GraphType.
    /// </summary>
    private void ResetGraph()
    {
        _isChangingGraphType = true;
        _changeDirection = false;
        _meshRenderer.enabled = false;
        _transform.position = Vector3.zero;
        _index = 0;
        _lineRenderer.positionCount = 0;
        _movePosition = UpdateGraphFunction();
    }

    /// <summary>
    /// Updates the mathematical function to be used for the selected GraphType.
    /// </summary>
    private Vector3 UpdateGraphFunction()
    {
        Vector3 _currentPosition = _transform.position;
        GraphFunction _function = GraphFunctionLibrary.GetGraphFunction(_currentGraphType);
        return _function(_currentPosition.x, _coefficient, _yIntercept);
    }

    /// <summary>
    /// Increments the input value passed into the selected GraphType.
    /// </summary>
    private void UpdateInput()
    {
        if (_isChangingGraphType) return;
        
        if (_transform.position.x > _maxInputValue)
        {
            _changeDirection = true;
        }
        else if (_transform.position.x < _minInputValue)
        {
            _changeDirection = false;
        }

        if (!_changeDirection)
        {
            _inputValue = _graphSpeed * Time.deltaTime;
        }
        else
        {
            _inputValue = -_graphSpeed * Time.deltaTime;
        }
        
        _movePosition = new Vector3(_movePosition.x + _inputValue, _movePosition.y, 0);
        _transform.position = _movePosition;
    }
    
    /// <summary>
    /// Updates the line renderer component to draw the selected GraphType.
    /// </summary>
    private void DrawGraph()
    {
        if (_isChangingGraphType) return;

        _meshRenderer.enabled = true;
        
        if (_lineRenderer.positionCount <= _index)
        {
            _lineRenderer.positionCount = _index + 1;
        }

        _movePosition = UpdateGraphFunction();
        _lineRenderer.SetPosition(_index, _movePosition);
        _index++;
    }
    
    #endregion
    
}
