using System.Text;

class Program
{
    private static int ProcessTextFile(string filePath)
    {

        int result = 0;
        string line;
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


