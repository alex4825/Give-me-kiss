using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAdaptor : MonoBehaviour
{
    [SerializeField] private List<Transform> _canvasElements;
    [SerializeField] private float _minScaleCoef = 0.7f;

    private Vector2Int _defaultScreenResolution = new(1920, 1080);
    private Vector2 _minScreenResolution;
    bool _isScaled;

    private void Awake()
    {
        _minScreenResolution = new(_defaultScreenResolution.x * _minScaleCoef, _defaultScreenResolution.y * _minScaleCoef);
    }

    private void Update()
    {
        if (Screen.width < _minScreenResolution.x || Screen.height < _minScreenResolution.y)
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
            if(_isScaled == true)
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
