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
        ballTable.Add("redMax", 0);
        ballTable.Add("greenMax", 0);
        ballTable.Add("blueMax", 0);

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
        ballTable["redMax"] = 0;
        ballTable["greenMax"] = 0;
        ballTable["blueMax"] = 0;

    }

    /// <summary>
    /// "Scores" the game by deducting the appropriate ball color.
    /// </summary>
    /// <param name="game">each round of the game, broken up into array values</param>
    /// <param name="gameTable">the hashtable to keep track of ball counts</param>
    /// <returns>Returns whether the game was valid or not</returns>
    private static void UpdateBallCount(string[] game, Hashtable gameTable)
    {
        int deductVal;
        int currColorVal;
        int currentMaxVal;

        foreach (var ball in game)
        {
            ball.Trim();
            string[] newBall = ball.Split(" ");
            int.TryParse(newBall[1].ToString(), out deductVal);
            if (ball.Contains("red"))
            {
                int.TryParse(gameTable["redMax"].ToString(), out currentMaxVal);
                if (currentMaxVal < deductVal)
                {
                    gameTable["redMax"] = deductVal;
                }
                int.TryParse(gameTable["red"].ToString(), out currColorVal);
                gameTable["red"] = currColorVal - deductVal;
            }
            else if (ball.Contains("blue"))
            {
                int.TryParse(gameTable["blueMax"].ToString(), out currentMaxVal);
                if (currentMaxVal < deductVal)
                {
                    gameTable["blueMax"] = deductVal;
                }
                int.TryParse(gameTable["blue"].ToString(), out currColorVal);
                gameTable["blue"] = currColorVal - deductVal;

            }
            else if (ball.Contains("green"))
            {
                int.TryParse(gameTable["greenMax"].ToString(), out currentMaxVal);
                if (currentMaxVal < deductVal)
                {
                    gameTable["greenMax"] = deductVal;
                }
                int.TryParse(gameTable["green"].ToString(), out currColorVal);
                gameTable["green"] = currColorVal - deductVal;
            }

        }
    }

    static void Main()
    {
        Hashtable gameTable = InitBallTable();
        int counter = 1;
        string line;
        List<int> powers = [];

        // Reads text file as input, and adds up the value of valid game rounds.
        using StreamReader reader = File.OpenText("./input2.txt");
        while ((line = reader.ReadLine()) != null)
        {
            List<string> resultGame = SanitizeString(line);
            foreach (var result in resultGame)
            {
                string[] game = result.Split(",");
                UpdateBallCount(game, gameTable);


            }
            int redMax = int.Parse(gameTable["redMax"].ToString());
            int greenMax = int.Parse(gameTable["greenMax"].ToString());
            int blueMax = int.Parse(gameTable["blueMax"].ToString());

            powers.Add(redMax * greenMax * blueMax);

            ResetBallCount(gameTable);
        }

        int finalSum = 0;

        foreach (var answers in powers)
        {
            finalSum += answers;
        }

        Console.WriteLine("Final sum: {0}", finalSum);

    }
}