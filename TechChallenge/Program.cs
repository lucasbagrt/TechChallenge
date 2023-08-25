using System.Diagnostics;

class Program
{
    public static string Key = string.Empty;
    static void Main(string[] args)
    {
        List<string> tested = new List<string>();
        var vowels = "A,E,I,O,U,Á,É,Í,Ó,Ú,À,È,Ì,Ò,Ù,Â,Ê,Î,Ô,Û,Ã,Õ,Ä,Ë,Ï,Ö,Ü";
        var vowelsList = vowels.Split(",");
        var r = new Random();
        var stopwatch = new Stopwatch();

        Key = GenerateRandomKey(r, vowelsList.ToArray());        
        stopwatch.Start();

        while (true)
        {
            vowelsList.AsParallel().ForAll(async vog =>
            {
                var randomNum = r.Next(1, 1001);
                var key = $"{(vog + "").ToLower()}{randomNum}";

                Console.WriteLine(key);
                if (await SendKey(tested, key, stopwatch))
                    Environment.Exit(0);
            });

            vowelsList.AsParallel().ForAll(async vog =>
            {
                var randomNum = r.Next(1, 1001);
                var key = $"{randomNum}{(vog + "").ToLower()}";

                Console.WriteLine(key);
                if (await SendKey(tested, key, stopwatch))
                    Environment.Exit(0);
            });

            vowelsList.AsParallel().ForAll(async vog =>
            {
                var randomNum = r.Next(1, 1001);
                var key = $"{(vog + "").ToUpper()}{randomNum}";

                Console.WriteLine(key);
                if (await SendKey(tested, key, stopwatch))
                    Environment.Exit(0);
            });

            vowelsList.AsParallel().ForAll(async vog =>
            {
                var randomNum = r.Next(1, 1001);
                var key = $"{randomNum}{(vog + "").ToUpper()}";

                Console.WriteLine(key);
                if (await SendKey(tested, key, stopwatch))
                    Environment.Exit(0);
            });
        }
    }

    public static async Task<bool> SendKey(List<string> tested, string key, Stopwatch stopwatch)
    {
        if (tested.Contains(key))
            return await Task.FromResult(false);

        if (Key == key)
        {
            Console.WriteLine("Chave " + key);
            Console.WriteLine("Tentativas " + tested.Count);
            Console.WriteLine("Tempo " + (stopwatch.ElapsedMilliseconds / 1000) + " seg");
            return await Task.FromResult(true);
        }

        tested.Add(key);
        return await Task.FromResult(false);
    }

    public static string GenerateRandomKey(Random r, string[] vowels)
    {                
        var randomNum = r.Next(1, 1001);        

        var selectedVowel = vowels[r.Next(vowels.Length)];
        bool addAtBeginning = r.Next(2) == 0;
        bool upperCase = r.Next(2) == 0;

        var randomKey = addAtBeginning ? 
               selectedVowel + randomNum.ToString() :
               randomNum.ToString() + selectedVowel;        

        return upperCase ?
               randomKey.ToUpper() :
               randomKey.ToLower();
    }
}