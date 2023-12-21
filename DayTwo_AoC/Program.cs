using System;
using System.Collections;
using System.Diagnostics.Metrics;
using System.Xml.XPath;

class Solution
{

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

    private static Hashtable InitBallTable()
    {
        Hashtable ballTable = new Hashtable();
        ballTable.Add("red", 12);
        ballTable.Add("green", 13);
        ballTable.Add("blue", 14);
        ballTable.Add("gameSum", 0);

        return ballTable;
    }

    private static void ResetBallCount(Hashtable ballTable)
    {
        ballTable["red"] = 12;
        ballTable["green"] = 13;
        ballTable["blue"] = 14;

    }

    private static bool updateBallCount(string[] game, Hashtable gameTable, int counter)
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
        return CheckBallCount(gameTable, counter);
    }

    private static bool CheckBallCount(Hashtable gameTable, int counter)
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

        using StreamReader reader = File.OpenText("./input2.txt");
        while ((line = reader.ReadLine()) != null)
        {
            List<string> resultGame = SanitizeString(line);
            bool stillValid = true;
            foreach (var result in resultGame)
            {
                string[] game = result.Split(",");
                stillValid = updateBallCount(game, gameTable, counter);
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