using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Partner : Person
{
    public Partner(PartnerData partnerData) : base(partnerData as PersonData)
    {
        Age = partnerData.Age;
        Height = partnerData.Height;
        AboutSelf = partnerData.AboutSelf;
        ShortAboutSelf = partnerData.ShortAboutSelf;
        
        Chat = new Chat(this);
    }

    public int Age { get; private set; }
    public int Height { get; private set; }
    public string ShortAboutSelf { get; private set; }
    public string AboutSelf { get; private set; }
    public Chat Chat { get; private set; }    

    public override string ToString()
    {
        return $"Имя: {Name}, Возраст: {Age}, Рост: {Height}, Кратко о себе: {ShortAboutSelf}, Описание персонажа: {AboutSelf}";
    }
}
