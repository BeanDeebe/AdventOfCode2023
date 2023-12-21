using System.Collections;
using System.Text;

class Program
{
    private static int ProcessTextFile(string filePath)
    {

        int result = 0;
        string line;
        List<string> processedLines = [];

        Hashtable wordToNum = new Hashtable();

        wordToNum.Add("one", 1);
        wordToNum.Add("two", 2);
        wordToNum.Add("three", 3);
        wordToNum.Add("four", 4);
        wordToNum.Add("five", 5);
        wordToNum.Add("six", 6);
        wordToNum.Add("seven", 7);
        wordToNum.Add("eight", 8);
        wordToNum.Add("nine", 9);

        string[] names = [
            "one", "two", "three", "four", "five",
            "six","seven", "eight", "nine"];

        /// checking if file exists.
        if (!File.Exists(filePath))
        {
            Console.WriteLine("No file found");
            return -1;
        }
        else
        {


            using StreamReader reader = File.OpenText(filePath);

            while ((line = reader.ReadLine()) != null)
            {
                foreach (var name in names)
                {
                    if (line.Contains(name))
                    {
                        while (line.Contains(name))
                        {
                            line = line.Replace(name, name.First() + wordToNum[name].ToString() + name.Last());
                            if (!line.Contains(name))
                            {
                                break;
                            }

                        }

                    }
                }

                /// number logic
                char[] lineArr = line.ToCharArray();
                List<int> numsArr = [];
                int number;

                /// add nums to their own array.
                for (int i = 0; i < lineArr.Length; i++)
                {
                    if (int.TryParse(lineArr[i].ToString(), out number))
                    {
                        numsArr.Add(number);
                    }
                }

                StringBuilder sb = new StringBuilder();
                sb.Append(numsArr.First());
                sb.Append(numsArr.Last());

                int addToRes;
                int.TryParse(sb.ToString(), out addToRes);
                result += addToRes;

            }
            return result;
        }
    }
    static void Main()
    {
        int result = ProcessTextFile("./input.txt");
        Console.WriteLine(result.ToString());
    }
}


