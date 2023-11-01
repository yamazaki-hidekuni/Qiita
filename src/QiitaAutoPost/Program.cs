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
    static async Task Main(string[] args)
    {
        try
        {
            // 記事のディレクトリパス
            string articlesDirectory = "articles";

            // 未投稿の記事を探す
            var articleFile = FindOldestUnpostedArticle(articlesDirectory);
            if (articleFile == null)
            {
                Console.WriteLine("未投稿の記事が見つかりませんでした。");
                return;
            }

            // 記事の内容を読み込む
            string articleContent = await File.ReadAllTextAsync(articleFile.FullName);

            // タイトルとタグを取得
            string title = Path.GetFileNameWithoutExtension(articleFile.Name);
            string tag = articleFile.Directory.Name;

            // Qiitaに投稿
            await PostArticleToQiita(title, tag, articleContent);

            // ファイル名に "posted" を追加
            string newFileName = $"{articleFile.FullName}_posted";
            File.Move(articleFile.FullName, newFileName);

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
                // ファイル名に "posted" などのマーカーがないか確認
                if (!file.Name.Contains("posted") && file.CreationTime < oldestDate)
                {
                    oldestFile = file;
                    oldestDate = file.CreationTime;
                }
            }
        }
        return oldestFile;
    }

    static async Task PostArticleToQiita(string title, string tag, string content)
    {
        var httpClient = new HttpClient();
        var requestUri = "https://qiita.com/api/v2/items"; // QiitaのAPIエンドポイント

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

        // ここでQiitaのアクセストークンを設定
        // この例では、環境変数からアクセストークンを読み込んでいます
        var accessToken = Environment.GetEnvironmentVariable("QIITA_TOKEN");
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        var response = await httpClient.PostAsync(requestUri, httpContent);

        if (!response.IsSuccessStatusCode)
        {
            // 例外をスローする代わりに、エラーメッセージをログに記録します
            Console.WriteLine($"Qiitaへの投稿に失敗しました。ステータスコード: {response.StatusCode}");
            return;
        }

        // レスポンスボディにエラーが含まれているかどうかを確認します
        var responseBody = await response.Content.ReadAsStringAsync();
        var responseJson = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);
        if (responseJson.ContainsKey("error"))
        {
            Console.WriteLine($"Qiitaへの投稿に失敗しました。エラーメッセージ: {responseJson["error"]}");
        }
    }
}