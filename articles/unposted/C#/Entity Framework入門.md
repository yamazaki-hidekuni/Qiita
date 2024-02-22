## Entity Frameworkとは？
Entity Frameworkは、Microsoftが提供するオープンソースのオブジェクト関連マッピング (ORM) ツールです。ORMツールとは、データベースのテーブルをプログラムのクラスにマッピングし、データベース操作をより直感的かつ簡単に行うためのツールです。

## Entity Frameworkの主要機能
Entity Frameworkには以下のような機能があります。

- **コードファースト**: データモデルをC#のコードで直接記述し、それを基にデータベースを生成・操作します。
- **データベースファースト**: 既存のデータベースからエンティティクラスを生成し、それを使用してデータベースを操作します。
- **LINQサポート**: LINQを用いてデータベースのクエリを書くことができます。

## 簡単な例
ここでは、Entity Frameworkを使って簡単なデータベース操作を行う例を示します。まず、データモデルのクラスを定義します。

```csharp
public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }
}
```

次に、データベースコンテキストを定義します。

```csharp
public class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
}
```

そして、以下のようにデータベースにアクセスし、データを操作します。

```csharp
using (var db = new BloggingContext())
{
    var blog = new Blog { Url = "http://sample.com" };
    db.Blogs.Add(blog);
    db.SaveChanges();

    var query = from b in db.Blogs
                orderby b.Url
                select b;

    foreach (var item in query)
    {
        Console.WriteLine(item.Url);
    }
}
```

この例では、まず新しいブログをデータベースに追加し、その後で全てのブログをURLで並べ替えて表示しています。