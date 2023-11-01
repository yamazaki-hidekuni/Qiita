using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

class Program
{
    private static readonly HttpClient httpClient = new HttpClient();

    static async Task Main(string[] args)
    {
        try
        {
            string articlesDirectory = "articles";

            var accessToken = Environment.GetEnvironmentVariable("QIITA_TOKEN");
            if (string.IsNullOrEmpty(accessToken))
            {
                Console.WriteLine("Qiitaのアクセストークンが設定されていません。");
                return;
            }
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var postedArticles = await GetPostedArticles("your_qiita_username");

            var articleFile = FindOldestUnpostedArticle(articlesDirectory, postedArticles);
            if (articleFile == null)
            {
                Console.WriteLine("未投稿の記事が見つかりませんでした。");
                return;
            }

            string articleContent = await File.ReadAllTextAsync(articleFile.FullName);
            string title = Path.GetFileNameWithoutExtension(articleFile.Name);
            string tag = articleFile.Directory.Name;

            bool isPosted = await PostArticleToQiita(title, tag, articleContent);
            if (!isPosted)
            {
                Console.WriteLine("記事の投稿に失敗しました。");
                return;
            }

            Console.WriteLine("記事をQiitaに投稿しました。");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"エラーが発生しました: {ex.Message}");
        }
    }

    static FileInfo FindOldestUnpostedArticle(string directoryPath, HashSet<string> postedArticles)
    {
        var directoryInfo = new DirectoryInfo(directoryPath);
        FileInfo oldestFile = null;
        DateTime oldestDate = DateTime.MaxValue;

        foreach (var subDirectory in directoryInfo.GetDirectories())
        {
            foreach (var file in subDirectory.GetFiles("*.md"))
            {
                if (!postedArticles.Contains(Path.GetFileNameWithoutExtension(file.Name)) && file.CreationTime < oldestDate)
                {
                    oldestFile = file;
                    oldestDate = file.CreationTime;
                }
            }
        }
        return oldestFile;
    }

    static async Task<HashSet<string>> GetPostedArticles(string userId)
    {
        var requestUri = $"https://qiita.com/api/v2/users/{userId}/items";
        var postedArticles = new HashSet<string>();

        while (true)
        {
            var response = await httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Qiitaからの投稿の取得に失敗しました。ステータスコード: {response.StatusCode}");
                break;
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var responseJson = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(responseBody);

            postedArticles.UnionWith(responseJson.Select(item => item["title"].ToString()));

            if (!response.Headers.TryGetValues("Link", out var linkHeader) || !linkHeader.Any(link => link.Contains("rel=\"next\"")))
            {
                break;
            }
        }

        return postedArticles;
    }

    static async Task<bool> PostArticleToQiita(string title, string tag, string content)
    {
        var requestUri = "https://qiita.com/api/v2/items";

        var postData = new
        {
            body = content,
            coediting = false,
            group_url_name = (string)null,
            @private = false,
            tags = new[] { new { name = tag } },
            title = title,
            tweet = false,
        };

        var jsonContent = JsonSerializer.Serialize(postData);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(requestUri, httpContent);

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Qiitaへの投稿に失敗しました。ステータスコード: {response.StatusCode}");
            return false;
        }

        var responseBody = await response.Content.ReadAsStringAsync();
        var responseJson = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);
        if (responseJson.ContainsKey("error"))
        {
            Console.WriteLine($"Qiitaへの投稿に失敗しました。エラーメッセージ: {responseJson["error"]}");
            return false;
        }

        return true;
    }
}