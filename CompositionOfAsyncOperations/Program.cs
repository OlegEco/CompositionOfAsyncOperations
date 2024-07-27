
string[] urls =
{
    "https://jsonplaceholder.typicode.com/posts",
    "https://jsonplaceholder.typicode.com/comments",
    "https://jsonplaceholder.typicode.com/albums"
};

Task<string>[] fetchTasks = new Task<string>[urls.Length];

for (int i = 0; i < urls.Length; i++)
{
    fetchTasks[i] = FetchDataAsync(urls[i]);
}

string[] resuls = await Task.WhenAll(fetchTasks); //waiting completed all Tasks

foreach (string resul in resuls)
{
    Console.WriteLine(resul.Substring(0, 100)); // first 100 symbols every result
}

static async Task<string> FetchDataAsync(string url)
{
    using (HttpClient client = new HttpClient()) 
    {
        HttpResponseMessage response = await client.GetAsync(url); 
        response.EnsureSuccessStatusCode(); 
        string responseData = await response.Content.ReadAsStringAsync();
        return responseData; 
    }
}
