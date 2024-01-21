## Dictionaryとは何か？
C#における`Dictionary`は、キーと値のペアを格納するためのコレクションです。これは、キーを使用して迅速に値を検索できるように設計されています。`Dictionary<TKey, TValue>`は、`System.Collections.Generic`名前空間に属し、ジェネリックコレクションの一部です。

### 主な特徴
- **高速な検索**: キーを使用して値を迅速に検索できます。
- **一意のキー**: 各キーは`Dictionary`内で一意でなければなりません。
- **ジェネリック**: 異なるタイプのキーと値を格納することができます。

## 基本的な使用方法
`Dictionary`の使用を開始するには、まず`Dictionary`オブジェクトを作成し、その後、キーと値のペアを追加する必要があります。

```csharp
Dictionary<string, int> ageOfFriends = new Dictionary<string, int>();
ageOfFriends.Add("John", 30);
ageOfFriends.Add("Mary", 28);
ageOfFriends.Add("Mike", 35);
```

### 値の取得
キーを指定して、対応する値を取得できます。

```csharp
int ageOfJohn = ageOfFriends["John"];
```

### 要素の更新と削除
既存のキーの値を更新したり、キーと値のペアを削除することもできます。

```csharp
// 値の更新
ageOfFriends["John"] = 31;

// キーと値のペアの削除
ageOfFriends.Remove("Mary");
```

## Dictionaryの活用
### キーの存在確認
`ContainsKey`メソッドを使用して、特定のキーが`Dictionary`に存在するかどうかを確認できます。

```csharp
if (ageOfFriends.ContainsKey("Mike")) {
    Console.WriteLine("Mikeの年齢は: " + ageOfFriends["Mike"]);
}
```

### foreachループを使用した反復処理
`Dictionary`を`foreach`ループで繰り返し処理することができます。

```csharp
foreach (KeyValuePair<string, int> kvp in ageOfFriends) {
    Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
}
```