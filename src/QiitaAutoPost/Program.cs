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
            string unpostedDirectory = "articles/unposted";
            string postedDirectory = "articles/posted";

            var articleFile = FindOldestUnpostedArticle(unpostedDirectory);
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

            // 投稿済みのディレクトリに移動
            string newDirectoryPath = Path.Combine(postedDirectory, tag);
            if (!Directory.Exists(newDirectoryPath))
            {
                Directory.CreateDirectory(newDirectoryPath);
            }
            string newPath = Path.Combine(newDirectoryPath, articleFile.Name);
            File.Move(articleFile.FullName, newPath);

            // unpostedとpostedディレクトリを比較し、同名のファイルがあれば削除
            string unpostedFileName = Path.GetFileName(articleFile.FullName);
            string postedFileName = Path.GetFileName(newPath);

            if (postedFileName == unpostedFileName)
            {
                try
                {
                    File.Delete(articleFile.FullName);
                    Console.WriteLine($"削除成功: {articleFile.FullName}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"削除失敗: {articleFile.FullName}, エラー: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"削除対象なし: {articleFile.FullName}");
            }

            Console.WriteLine("記事をQiitaに投稿しました。");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"エラーが発生しました: {ex.Message}");
        }
    }

    static FileInfo FindOldestUnpostedArticle(string directoryPath)
    {
        var directoryInfo = new DirectoryInfo(directoryPath);
        FileInfo oldestFile = null;
        DateTime oldestDate = DateTime.MaxValue;

        foreach (var subDirectory in directoryInfo.GetDirectories())
        {
            foreach (var file in subDirectory.GetFiles("*.md"))
            {
                if (file.CreationTime < oldestDate)
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
            group_url_name = "jpt-qiita",
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
