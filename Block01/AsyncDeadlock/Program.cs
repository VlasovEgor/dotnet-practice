class Program
{

    private static async Task Main(string[] args)
    {
        string value = await GetData();
        Console.WriteLine(value);
    }
    
    private static async Task<string> GetData()
    {
        return await FetchAsync();
    }
    
    private static async Task<string> FetchAsync()
    {
        await Task.Delay(100);
        return "ok";
    }
}


