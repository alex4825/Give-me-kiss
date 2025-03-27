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
        List<PartnerData>  partnersData = FileManager.Instance.JsonToPartnerDataList();

        foreach (var partnerData in partnersData)
        {
            Partners.Add(new Partner(partnerData));
        }

        DebugInformer.ShowStringFrom<Partner>(Partners);
    }
}
