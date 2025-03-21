using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CardManager : MonoBehaviour
{
    [SerializeField] private Transform _cardContainer;
    [SerializeField] private GameObject _cardPrefab;

    private List<Character> _characters;

    public List<Card> Cards { get; private set; }

    public static CardManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _characters = CharacterManager.Instance.Characters;
        CreateCards();
    }

    private void CreateCards()
    {
        Cards = new List<Card>(_characters.Count);

        foreach (Character character in _characters)
        {
            GameObject cardGameObject = Instantiate(_cardPrefab, _cardContainer);
            cardGameObject.GetComponent<Card>().Initiate(character);
        }
    }
}
