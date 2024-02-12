## System.Text.Jsonとは？
`System.Text.Json`は.NET Core 3.0に導入された、JSON処理ライブラリです。従来の`Newtonsoft.Json`（別名Json.NET）よりもパフォーマンスが優れており、メモリ消費量も少ないのが特徴です。

### 特徴と利点
- **高速なパフォーマンス**: 特に読み書きの処理速度が速い。
- **低メモリ消費**: リソース使用が最適化されている。
- **ネイティブサポート**: .NET Core 3.0以降で標準で利用可能。
- **安全性**: 安全なコード記述を促進。

## JSONのシリアライズ
JSONのシリアライズとは、C#のオブジェクトをJSON文字列に変換するプロセスです。

```csharp
using System.Text.Json;

public class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}

var product = new Product { Name = "Pen", Price = 1.99M };
string jsonString = JsonSerializer.Serialize(product);
```

この例では`Product`オブジェクトがJSON文字列に変換されます。

### オプションのカスタマイズ
`JsonSerializerOptions`を使用して、シリアライズの挙動をカスタマイズできます。例えば、プロパティ名をキャメルケースにするには以下のようにします。

```csharp
var options = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
};
string jsonString = JsonSerializer.Serialize(product, options);
```

## JSONのデシリアライズ
JSONのデシリアライズは、JSON文字列をC#のオブジェクトに変換するプロセスです。

```csharp
string jsonString = "{\"Name\":\"Pen\",\"Price\":1.99}";
var product = JsonSerializer.Deserialize<Product>(jsonString);
```

このコードはJSON文字列を`Product`オブジェクトに変換します。

### エラーハンドリング
デシリアライズ中にエラーが発生した場合、`JsonException`がスローされます。適切なエラーハンドリングを行うことで、アプリケーションの堅牢性を高めることができます。