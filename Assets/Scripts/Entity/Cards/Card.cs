using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] protected Image PersonImage;
    [SerializeField] protected Image BackgroundImage;
    [SerializeField] protected ProgressBar ProgressBar;

    protected Person Person;

    private void OnEnable()
    {
        if (Person != null)
        {
            ProgressBar.SetFillAmount(Person.ProgressNormalized);
        }
    }

    public virtual void Initiate(Person person)
    {
        Person = person;
        PersonImage.sprite = Person.FaceSprite;
        BackgroundImage.color = person.BasicColor;
        ProgressBar.SetFillAmount(person.ProgressNormalized);

        Person.OnProgressNormalizedChanged += Person_OnProgressChanged;
    }

    protected void Person_OnProgressChanged(float progressNotmalized)
    {
        ProgressBar.SetFillAmount(progressNotmalized);
    }
}
