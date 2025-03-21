using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SwipeController : MonoBehaviour
{
    [SerializeField] private RectTransform _cardContainer;
    [SerializeField] private HorizontalLayoutGroup _horizontalLayoutGroup;
    [SerializeField] private RectTransform _card;
    [SerializeField] private float _swipeTime;
    [SerializeField] private Ease _easeType;

    private float _swipeLenght;
    [SerializeField] private float _swipeTimer;

    private void Awake()
    {
        _swipeLenght = _card.rect.width + _horizontalLayoutGroup.spacing;
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

        if (toRight)
            targetPositionX = _cardContainer.position.x - _swipeLenght;
        else
            targetPositionX = _cardContainer.position.x + _swipeLenght;

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
