using static System.Net.Mime.MediaTypeNames;

class Program
{
    public static string Key = string.Empty;
    static void Main(string[] args)
    {
        List<string> tested = new List<string>();
        var vowels = "A,E,I,O,U,Á,É,Í,Ó,Ú,À,È,Ì,Ò,Ù,Â,Ê,Î,Ô,Û,Ã,Õ,Ä,Ë,Ï,Ö,Ü";
        var vowelsList = vowels.Split(",");
        var r = new Random();

        Key = GenerateRandomKey(r, vowelsList.ToArray());        


        while (true)
        {
            vowelsList.AsParallel().ForAll(async vog =>
            {
                var randomNum = r.Next(1, 1001);
                var key = $"{(vog + "").ToLower()}{randomNum}";

                if (await SendKey(tested, key))
                    Environment.Exit(0);
            });

            vowelsList.AsParallel().ForAll(async vog =>
            {
                var randomNum = r.Next(1, 1001);

                var key = $"{randomNum}{(vog + "").ToLower()}";
                if (await SendKey(tested, key))
                    Environment.Exit(0);
            });

            vowelsList.AsParallel().ForAll(async vog =>
            {
                var randomNum = r.Next(1, 1001);

                var key = $"{(vog + "").ToUpper()}{randomNum}";
                if (await SendKey(tested, key))
                    Environment.Exit(0);
            });

            vowelsList.AsParallel().ForAll(async vog =>
            {                
                var randomNum = r.Next(1, 1001);

                var key = $"{randomNum}{(vog + "").ToUpper()}";
                if (await SendKey(tested, key))
                    Environment.Exit(0);
            });
        }
    }

    public static async Task<bool> SendKey(List<string> tested, string key)
    {
        if (tested.Contains(key))
            return await Task.FromResult(false);

        if (Key == key)
        {
            Console.WriteLine("Chave " + key);
            return await Task.FromResult(true);
        }

        tested.Add(key);
        return await Task.FromResult(false);
    }

    public static string GenerateRandomKey(Random r, string[] vowels)
    {        
        Random random = new Random();
        var randomNum = random.Next(1, 1001);        

        var selectedVowel = vowels[random.Next(vowels.Length)];
        bool addAtBeginning = random.Next(2) == 0;
        bool upperCase = random.Next(2) == 0;

        var randomKey = addAtBeginning ? 
               selectedVowel + randomNum.ToString() :
               randomNum.ToString() + selectedVowel;        

        return upperCase ?
               randomKey.ToUpper() :
               randomKey.ToLower();
    }
}