using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Person
{
    public string OriginName { get; protected set; }
    public string Name { get; protected set; }
    public Sprite FaceSprite { get; protected set; }
    public Sprite AppearanceSprite { get; protected set; }

}
