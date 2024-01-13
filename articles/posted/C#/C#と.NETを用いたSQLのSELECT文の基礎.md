## SELECT文とは
SELECT文は、SQLデータベースからデータを取得するための基本的な命令です。指定された条件に一致するデータを検索し、結果セットとして返します。

### 基本構文
```sql
SELECT 列名1, 列名2, ...
FROM テーブル名
WHERE 条件;
```
- `SELECT`：取得したい列を指定します。
- `FROM`：データを取得するテーブルを指定します。
- `WHERE`：取得するデータの条件を指定します（任意）。

### 例
```sql
SELECT name, age
FROM users
WHERE age > 20;
```
この例では、`users` テーブルから `age` が20より大きい全ユーザーの `name` と `age` を取得します。

## C#と.NETでのSELECT文の使い方
C#でSQLのSELECT文を扱うには、通常 `SqlConnection` や `SqlCommand` クラスを使用します。

### 基本的なステップ
1. データベースへの接続を確立する。
2. SQLコマンドを作成し、実行する。
3. 結果を処理する。

### コード例
```csharp
using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connectionString = "接続文字列";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string sql = "SELECT name, age FROM users WHERE age > 20";
            SqlCommand command = new SqlCommand(sql, connection);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["name"]} - {reader["age"]}");
                }
            }
        }
    }
}
```
このコードでは、データベースに接続し、SELECT文を実行して結果をコンソールに出力しています。