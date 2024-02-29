# Entity Framework Coreで初期データをデータベースに反映する方法

Entity Framework Core (EF Core) を使用する際、アプリケーション開発の初期段階やバージョンアップ時に、特定のデータをデータベースにあらかじめ挿入しておくことがよくあります。このような初期データをデータベースに反映するプロセスは、EF Coreのマイグレーション機能を通じて行われます。
この記事では、.NET Core CLIを使用して初期データをデータベースに反映する方法を解説します。

## 初期データ反映の基本ステップ

### ステップ 1: データモデルの定義

最初に、アプリケーションで使用するデータの構造を定義します。これはエンティティクラスを作成することで行い、データベースのテーブルと列にマッピングされます。

```csharp
public class Blog
{
    public int BlogId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
}
```

### ステップ 2: 初期データの設定

次に、`DbContext`クラスの`OnModelCreating`メソッドをオーバーライドして、初期データを設定します。`HasData`メソッドを使用して、挿入したい初期データを指定します。

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Blog>().HasData(
        new Blog { BlogId = 1, Name = "My First Blog", Url = "https://myfirstblog.com" },
        new Blog { BlogId = 2, Name = "Tech Blog", Url = "https://techblog.com" }
    );
}
```

### ステップ 3: マイグレーションの作成

初期データを含めた状態で、データモデルの変更をデータベースに反映するためのマイグレーションを作成します。.NET Core CLIを使用して以下のコマンドを実行します。

```bash
dotnet ef migrations add AddInitialData
```

このコマンドは、データモデルの変更を含む新しいマイグレーションファイルを生成し、指定した初期データの挿入コードも含めます。

### ステップ 4: マイグレーションの適用

作成したマイグレーションをデータベースに適用して、初期データをデータベースに挿入します。

```bash
dotnet ef database update
```

このコマンドは、最新のマイグレーションをデータベースに適用し、`HasData`メソッドで定義されたデータをデータベースに挿入します。

## 注意点

- 初期データを挿入する際は、エンティティの主キーを明示的に指定する必要があります。
- データモデルに変更がない場合でも、初期データの追加や変更のためだけに新しいマイグレーションを作成することができます。