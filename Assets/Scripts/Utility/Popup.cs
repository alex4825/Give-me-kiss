using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField] private float _disappearDistance = 100;
    [SerializeField] private float _disappearDuration = 3.5f;

    private void OnEnable()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOMoveY(transform.position.y + _disappearDistance, _disappearDuration));

        var graphics = GetComponentsInChildren<Graphic>();

        foreach (var graphic in graphics)
        {
            sequence.Join(graphic.DOFade(0f, _disappearDuration));
        }

        sequence.SetEase(Ease.InSine);
        sequence.Play();
        sequence.OnComplete(() => { Destroy(gameObject); });
    }
}
