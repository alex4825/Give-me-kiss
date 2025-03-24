using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerManager : SingletonPersistent<PartnerManager>
{
    [SerializeField] private string _resourcesCharactersFileName;

    public List<Partner> Partners { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        InitiateCharactersFromDataFile();
    }

    private void InitiateCharactersFromDataFile()
    {
        Partners = new List<Partner>();
        Dictionary<string, PartnerData>  partnersData = FileManager.Instance.JsonToPartnerDataDictionary();

        foreach (var pair in partnersData)
        {
            PartnerData partnerData = pair.Value;
            string characterOriginName = pair.Key;

            Partners.Add(new Partner(characterOriginName, partnerData));

            DebugInformer.ShowStringFrom<Partner>(Partners);
        }
    }
}
