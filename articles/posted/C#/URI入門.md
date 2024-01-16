## URIとは何か？
URI（Uniform Resource Identifier）とは、インターネット上のリソースを一意に識別するための文字列のことです。これには、Webページ、ファイル、メールアドレスなどが含まれます。URIは、リソースがどこにあるか（URL）、何であるか（URN）を表すことができます。

### URIの構造
URIは以下のような構造を持っています。
```
scheme:[//[user:password@]host[:port]][/]path[?query][#fragment]
```
- **Scheme**: リソースにアクセスするためのプロトコル（例: http、https、ftp）
- **User**: リソースにアクセスするためのユーザー名（オプション）
- **Password**: ユーザーのパスワード（オプション）
- **Host**: リソースをホストしているサーバーのドメイン名またはIPアドレス
- **Port**: サーバーのポート番号（オプション）
- **Path**: ホスト上のリソースのパス
- **Query**: クエリパラメータ（オプション）
- **Fragment**: リソース内の特定の部分（オプション）

### URLとURN
URIには、URL（Uniform Resource Locator）とURN（Uniform Resource Name）の二つのサブセットがあります。
- **URL**: リソースがどこにあるかを指定します。通常、Webページのアドレスを指すのに使用されます。
- **URN**: リソースが何であるかを一意に識別します。たとえば、ISBN番号は書籍のURNと見なすことができます。

## URIの使用例
C#でのURIの使用例を見てみましょう。以下は、URIを生成し、その構成要素にアクセスする簡単な例です。

```csharp
using System;

namespace URIDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri myUri = new Uri("https://www.example.com:80/path/to/myfile.html?user=id&password=123#section2");

            Console.WriteLine("Scheme: " + myUri.Scheme);
            Console.WriteLine("Host: " + myUri.Host);
            Console.WriteLine("Port: " + myUri.Port);
            Console.WriteLine("Path: " + myUri.AbsolutePath);
            Console.WriteLine("Query: " + myUri.Query);
            Console.WriteLine("Fragment: " + myUri.Fragment);
        }
    }
}
```