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

            // 投稿済みの記事のリストを保持するテキストファイルのパス
            string postedArticlesFile = "../../posted_articles.txt";
            Console.WriteLine(Directory.GetCurrentDirectory());

            // 投稿済みの記事のリストを読み込む
            List<string> postedArticles = new List<string>();
            if (File.Exists(postedArticlesFile))
            {
                postedArticles = (await File.ReadAllLinesAsync(postedArticlesFile)).ToList();

                // ログにリストの内容を出力
                Console.WriteLine("Posted articles:");
                foreach (string article in postedArticles)
                {
                    Console.WriteLine(article);
                }
            }

            // 未投稿の記事を探す
            var articleFile = FindOldestUnpostedArticle(articlesDirectory, postedArticles);
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

            // 投稿済みの記事のリストに追加し、テキストファイルに保存
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
                // ファイル名が投稿済みの記事のリストに含まれていないか確認
                if (!postedArticles.Contains(file.Name) && file.CreationTime < oldestDate)
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