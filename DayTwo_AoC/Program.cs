using System;
using System.Collections;
using System.Diagnostics.Metrics;
using System.Xml.XPath;

class Solution
{

    /// <summary>
    /// Takes the game string and splits it into each 'round' of the game.
    /// </summary>
    /// <param name="gameRound">game string</param>
    /// <returns></returns>
    private static List<string> SanitizeString(string gameRound)
    {
        gameRound = gameRound.Remove(0, 7);
        string[] first = gameRound.Split(";");
        List<string> second = [];

        foreach (var round in first)
        {
            round.Split(",");
            second.Add(round);
        }

        return second;

    }

    /// <summary>
    /// creates the Hashtable to keep track of the balls / gameSum count.
    /// </summary>
    /// <returns></returns>
    private static Hashtable InitBallTable()
    {
        Hashtable ballTable = new Hashtable();
        ballTable.Add("red", 12);
        ballTable.Add("green", 13);
        ballTable.Add("blue", 14);
        ballTable.Add("gameSum", 0);

        return ballTable;
    }

    /// <summary>
    /// Resets ball count to original values
    /// </summary>
    /// <param name="ballTable">Hashtable with ball counts and gameSum</param>
    private static void ResetBallCount(Hashtable ballTable)
    {
        ballTable["red"] = 12;
        ballTable["green"] = 13;
        ballTable["blue"] = 14;

    }

    /// <summary>
    /// "Scores" the game by deducting the appropriate ball color.
    /// </summary>
    /// <param name="game">each round of the game, broken up into array values</param>
    /// <param name="gameTable">the hashtable to keep track of ball counts</param>
    /// <returns>Returns whether the game was valid or not</returns>
    private static bool UpdateBallCount(string[] game, Hashtable gameTable, int counter)
    {
        int deductVal;
        int currColorVal;

        foreach (var ball in game)
        {
            ball.Trim();
            string[] newBall = ball.Split(" ");
            int.TryParse(newBall[1].ToString(), out deductVal);

            if (ball.Contains("red"))
            {
                int.TryParse(gameTable["red"].ToString(), out currColorVal);
                gameTable["red"] = currColorVal - deductVal;
            }
            else if (ball.Contains("blue"))
            {
                int.TryParse(gameTable["blue"].ToString(), out currColorVal);
                gameTable["blue"] = currColorVal - deductVal;

            }
            else if (ball.Contains("green"))
            {
                int.TryParse(gameTable["green"].ToString(), out currColorVal);
                gameTable["green"] = currColorVal - deductVal;
            }

        }
        return CheckBallCount(gameTable);
    }

    /// <summary>
    /// Checks the Hashtable to see if the ball counts are positive or negative, then resets the ball count to the original values.
    /// </summary>
    /// <param name="gameTable">Hashtable for keeping track of ball counts</param>
    /// <returns>true if count is >= 0, false otherwise.</returns>
    private static bool CheckBallCount(Hashtable gameTable)
    {
        if
        (
            int.Parse(gameTable["red"].ToString()) < 0
            || int.Parse(gameTable["blue"].ToString()) < 0
            || int.Parse(gameTable["green"].ToString()) < 0
        )
        {
            ResetBallCount(gameTable);
            return false;
        }
        else
        {
            ResetBallCount(gameTable);
            return true;
        }
    }
    static void Main()
    {
        Hashtable gameTable = InitBallTable();
        int counter = 1;
        string line;

        // Reads text file as input, and adds up the value of valid game rounds.
        using StreamReader reader = File.OpenText("./input2.txt");
        while ((line = reader.ReadLine()) != null)
        {
            List<string> resultGame = SanitizeString(line);
            bool stillValid = true;
            foreach (var result in resultGame)
            {
                string[] game = result.Split(",");
                stillValid = UpdateBallCount(game, gameTable, counter);
                if (!stillValid)
                {
                    break;
                }
            }
            if (stillValid)
            {
                int updatedGameRoundValue;
                int.TryParse(gameTable["gameSum"].ToString(), out updatedGameRoundValue);
                gameTable["gameSum"] = updatedGameRoundValue + counter;
            }

            counter++;
        }

        Console.WriteLine("gameSum = {0}", gameTable["gameSum"].ToString());

    }
}