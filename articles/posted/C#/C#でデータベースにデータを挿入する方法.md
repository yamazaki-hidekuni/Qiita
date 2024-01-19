# C#でデータベースにデータを挿入する方法
## 1. プロジェクトのセットアップ
まず、Visual Studioで新しいプロジェクトを作成します。ここでは、C#のコンソールアプリケーションを例として使用します。新しいプロジェクトを作成したら、必要なNuGetパッケージをインストールします。データベース操作には、Entity FrameworkやSystem.Data.SqlClientなどのライブラリが便利です。

## 2. データベース接続の準備
次に、データベースへの接続を設定します。App.configまたはWeb.configファイルに接続文字列を定義します。例えば、SQL Serverを使用する場合、接続文字列は以下のようになります：

```xml
<connectionStrings>
    <add name="MyDbContext" 
         connectionString="Server=サーバー名; Database=データベース名; Trusted_Connection=True;" 
         providerName="System.Data.SqlClient" />
</connectionStrings>
```

## 3. データの挿入
データベースにデータを挿入するには、まずデータベースとの接続を確立する必要があります。C#でこれを行うためには、以下の手順に従います：

1. **データモデルの作成**：挿入したいデータに対応するクラス（例：`Product`）を作成します。
2. **DbContextクラスの定義**：Entity Frameworkを使用してデータベース操作を容易にします。
3. **データの追加**：DbContextを使用して、新しいデータ（例：`new Product()`）をデータベースに追加します。
4. **変更の保存**：DbContextの`SaveChanges()`メソッドを呼び出して、データベースに変更を永続化します。

以下はこのプロセスの基本的な例です：

```csharp
using System;
using System.Data.Entity;

public class MyDbContext : DbContext {
    public DbSet<Product> Products { get; set; }
}

public class Product {
    public int Id { get; set; }
    public string Name { get; set; }
    // その他のプロパティ
}

class Program {
    static void Main() {
        using (var db = new MyDbContext()) {
            var product = new Product { Name = "新製品" };
            db.Products.Add(product);
            db.SaveChanges();

            Console.WriteLine("データが挿入されました。");
        }
    }
}
```

## 4. コードの実行と確認
上記のコードをVisual Studioで実行します。プログラムが正常に動作すれば、コンソールに「データが挿入されました。」と表示されます。データベースを確認することで、新しいデータが挿入されていることを確認できます。