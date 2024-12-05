using UnityEngine;

public class FightingManager : MonoBehaviour
{
    private static Color currentPlayer; // Tracks the player initiating the fight
    private static Color fightWinner; // Stores the winner of the fight

    public static void SetFighters(Color playerColor)
    {
        currentPlayer = playerColor;
    }

    public static void SetWinner(Color winner)
    {
        fightWinner = winner;
    }

    public static Color GetWinner()
    {
        return fightWinner;
    }
}
