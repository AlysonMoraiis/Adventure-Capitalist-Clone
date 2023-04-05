using UnityEngine;

[CreateAssetMenu(fileName = "Business")]
public class Business : ScriptableObject
{
    public Sprite Sprite;

    public int InitialLevel;
    public int Level;
    public int LevelToGoal;

    public float InitialIncome;
    public float Income;
    public float InitialReturnTime;
    public float ReturnTime;
    public float InitialUpgradeCost;
    public float UpgradeCost;

    public bool HasPurchasedManager;
}
