データのシリアライズとは、オブジェクトやデータ構造を一連のビットに変換して、ファイルやネットワーク越しに保存や送信できる形式にすることを指します。この記事では、C#を使用してデータをJSON形式にシリアライズする方法について解説します。

## なぜJSONシリアライズが必要なのか？

JSON（JavaScript Object Notation）は、データをテキスト形式で表現するための軽量なデータ交換フォーマットです。その読みやすさとシンプルさから、Web APIや設定ファイルなど、多くの場面でデータ交換の標準フォーマットとして採用されています。

C#でJSONシリアライズを行うことで、オブジェクトの状態を簡単に保存したり、Webサービスにデータを送信したりすることができます。

## C#でのJSONシリアライズの基本

C#では、`System.Text.Json`名前空間に含まれる`JsonSerializer`クラスを使用してJSONシリアライズを行います。以下に基本的な使用方法を示します。

### シリアライズするクラスの定義

まず、シリアライズするためのクラスを定義します。例として、`Person`クラスを以下のように定義します。

```csharp
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
}
```

### オブジェクトのシリアライズ

次に、`Person`オブジェクトをJSONにシリアライズする方法を見てみましょう。

```csharp
using System.Text.Json;

var person = new Person
{
    Name = "John Doe",
    Age = 30,
    Email = "john.doe@example.com"
};

string jsonString = JsonSerializer.Serialize(person);
Console.WriteLine(jsonString);
```

このコードを実行すると、`Person`オブジェクトが以下のようなJSON文字列に変換されます。

```json
{"Name":"John Doe","Age":30,"Email":"john.doe@example.com"}
```

### シリアライズオプションのカスタマイズ

`JsonSerializer`では、シリアライズの挙動をカスタマイズするためのオプションを提供しています。例えば、プロパティ名をキャメルケースで出力したい場合は、`JsonSerializerOptions`を使用して設定を変更できます。

```csharp
var options = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
};

string jsonString = JsonSerializer.Serialize(person, options);
Console.WriteLine(jsonString);
```

これにより、出力されるJSON文字列のプロパティ名がキャメルケースになります。

```json
{"name":"John Doe","age":30,"email":"john.doe@example.com"}
```