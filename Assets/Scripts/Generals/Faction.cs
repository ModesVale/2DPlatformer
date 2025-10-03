using UnityEngine;

public enum FactionType
{
    Player,
    Enemy,
    Neutral
}

public class Faction : MonoBehaviour
{
    [SerializeField] private FactionType _faction;

    public FactionType Type => _faction;
}
