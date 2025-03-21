using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : SingletonPersistent<CharacterManager>
{
    [SerializeField] private string _resourcesCharactersFileName;

    public List<Character> Characters { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        InitiateCharactersFromDataFile();
        DebugInformer.ShowStringFrom<Character>(Characters);
    }

    private void InitiateCharactersFromDataFile()
    {
        Characters = new List<Character>();
        Dictionary<string, CharacterData>  charactersData = JsonConverter.ToDictionary<CharacterData>(_resourcesCharactersFileName);

        foreach (var pair in charactersData)
        {
            CharacterData characterData = pair.Value;
            string characterOriginName = pair.Key;

            Characters.Add(new Character(characterOriginName, characterData));
        }
    }
}
