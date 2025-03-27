using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAdaptor : MonoBehaviour
{
    [SerializeField] private List<Transform> _canvasElements;
    [SerializeField] private float _minScreenProportion = 0.7f;
    [SerializeField] private float _minScaleCoef = 0.7f;
    [SerializeField] private bool _shouldConsiderBothDimensions;

    private Vector2Int _defaultScreenResolution = new(1920, 1080);
    private Vector2 _minScreenResolution;
    bool _isScaled;

    private void Awake()
    {
        _minScreenResolution = new(_defaultScreenResolution.x * _minScreenProportion, _defaultScreenResolution.y * _minScreenProportion);
    }

    private void Update()
    {
        if (_shouldConsiderBothDimensions)
            ScaleAfterChangeDimentions(Screen.width < _minScreenResolution.x && Screen.height < _minScreenResolution.y);
        else
            ScaleAfterChangeDimentions(Screen.width < _minScreenResolution.x || Screen.height < _minScreenResolution.y);
    }

    private void ScaleAfterChangeDimentions(bool isDimentionsChange)
    {
        if (isDimentionsChange)
        {
            if (_isScaled == false)
            {
                foreach (var elementTransform in _canvasElements)
                {
                    elementTransform.localScale *= _minScaleCoef;
                }

                _isScaled = true;
            }
        }
        else
        {
            if (_isScaled == true)
            {
                foreach (var elementTransform in _canvasElements)
                {
                    elementTransform.localScale /= _minScaleCoef;
                }

                _isScaled = false;
            }
        }
    }    
}
