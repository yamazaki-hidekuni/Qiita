## Surrogate Key（代理キー）とは
Surrogate Keyは、データベース内のレコードを一意に識別するために使用される、データベース独自のキーです。これは通常、自動生成される一意の番号（例えば、整数の連番やGUIDなど）で構成されます。

### 代理キーのメリット
- **一意性**: 代理キーはレコードごとに一意であるため、データの整合性を保つのに役立ちます。
- **パフォーマンス**: 整数型のキーは、文字列や複合キーに比べて検索が高速です。
- **柔軟性**: ビジネスロジックとは無関係なため、ビジネス要件の変更に柔軟に対応できます。

### デメリット
- **直感性の欠如**: 代理キーはビジネスロジックと直接関係がないため、その値自体からは情報を読み取ることができません。
- **追加のスペース要件**: データベースに追加の列が必要となります。

## C#におけるSurrogate Keyの実装
C#とSQL Server Management Studio (SSMS)を使用して、Surrogate Keyをどのように実装するかを見ていきましょう。

### SSMSでの設定
SQL Serverでテーブルを作成する際に、代理キーとして`IDENTITY`属性を持つ列を定義します。これにより、SQL Serverは新しいレコードが追加されるたびに自動的に値を生成します。

```sql
CREATE TABLE Employees (
    EmployeeID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50),
    ...
);
```

### C#でのデータ操作
C#のEntity Frameworkを使用して、データベース操作を行う場合、代理キーは自動的に処理されます。例えば、新しいレコードを追加する際、`EmployeeID`は指定する必要はありません。

```csharp
using (var context = new MyDbContext()) {
    var employee = new Employee { Name = "Alice" };
    context.Employees.Add(employee);
    context.SaveChanges();
}
```