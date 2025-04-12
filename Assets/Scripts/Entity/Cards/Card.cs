using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] public Image PersonImage;
    [SerializeField] protected Image BackgroundImage;
    [SerializeField] protected ProgressBar ProgressBar;
    [SerializeField] private Transform _emotionLocation;

    protected Person Person;

    public Transform PopupLocation => _emotionLocation;

    private void OnEnable()
    {
        if (Person != null)
        {
            ProgressBar.SetFillAmount(Person.ProgressNormalized);
        }
    }

    public void Initiate(Person person)
    {
        Person = person;
        PersonImage.sprite = Person.FaceSprite;
        BackgroundImage.color = person.BasicColor;
        ProgressBar.SetFillAmount(person.ProgressNormalized);

        Person.OnProgressChanged += Person_OnProgressChanged;

        PersonImage.sprite = Person.FaceSprite;

        if (Person is Partner && (Person as Partner).IsConquered)
        {
            PersonImage.sprite = (Person as Partner).KissSprite;
        }
    }

    public void UpdateProgress(int value)
    {
        ProgressBar.SetFillAmount(value);
    }

    protected void Person_OnProgressChanged(float progressNotmalized)
    {
        ProgressBar.SetFillAmount(progressNotmalized);
    }

    /*private void OnDisable()
    {
        Person.OnProgressNormalizedChanged -= Person_OnProgressChanged;
    }*/
}
