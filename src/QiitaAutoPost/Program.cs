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
            string postedArticlesFile = "posted_articles.txt";

            List<string> postedArticles = new List<string>();
            if (File.Exists(postedArticlesFile))
            {
                postedArticles = (await File.ReadAllLinesAsync(postedArticlesFile)).ToList();
                Console.WriteLine("Posted articles:");
                foreach (string article in postedArticles)
                {
                    Console.WriteLine(article);
                }
            }

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

            postedArticles.Add(articleFile.Name);
            await File.WriteAllLinesAsync(postedArticlesFile, postedArticles);

            Console.WriteLine("記事をQiitaに投稿しました。");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"エラーが発生しました: {ex.Message}");
        }
    }

    static FileInfo FindOldestUnpostedArticle(string directoryPath, List<string> postedArticles)
    {
        var directoryInfo = new DirectoryInfo(directoryPath);
        FileInfo oldestFile = null;
        DateTime oldestDate = DateTime.MaxValue;

        foreach (var subDirectory in directoryInfo.GetDirectories())
        {
            foreach (var file in subDirectory.GetFiles("*.md"))
            {
                if (!postedArticles.Contains(file.Name) && file.CreationTime < oldestDate)
                {
                    oldestFile = file;
                    oldestDate = file.CreationTime;
                }
            }
        }
        return oldestFile;
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

        var accessToken = Environment.GetEnvironmentVariable("QIITA_TOKEN");
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

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