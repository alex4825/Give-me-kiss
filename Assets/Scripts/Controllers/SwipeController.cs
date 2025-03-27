using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SwipeController : MonoBehaviour
{
    [SerializeField] private RectTransform _scroller;
    [SerializeField] private RectTransform _cardContainer;
    [SerializeField] private HorizontalLayoutGroup _horizontalLayoutGroup;
    [SerializeField] private float _swipeTime;
    [SerializeField] private Ease _easeType;

    [SerializeField] private float _swipeTimer;

    private void Awake()
    {
        _swipeTimer = _swipeTime;
    }

    public void Next()
    {
        if (_swipeTimer == _swipeTime)
        {
            Move(true);
            StartCoroutine(WaitSwipeTime());
        }
    }

    public void Previous()
    {
        if (_swipeTimer == _swipeTime)
        {
            Move(false);
            StartCoroutine(WaitSwipeTime());
        }
    }

    private void Move(bool toRight)
    {
        float targetPositionX;

        float swipeLenght = _cardContainer.rect.height * _scroller.localScale.y;

        if (toRight)
            targetPositionX = _cardContainer.position.x - swipeLenght;
        else
            targetPositionX = _cardContainer.position.x + swipeLenght;

        /*if (targetPositionX % _swipeLenght != 0)
        {
            int roundedValue = (int)(targetPositionX / _swipeLenght);
            targetPositionX = roundedValue * _swipeLenght;
        }*/

        _cardContainer.DOMoveX(targetPositionX, _swipeTime).SetEase(_easeType);
    }

    private IEnumerator WaitSwipeTime()
    {
        while (_swipeTimer > 0)
        {
            _swipeTimer -= Time.deltaTime;
            yield return null;
        }
        _swipeTimer = _swipeTime;
    }
}
