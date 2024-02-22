### IDENTITYプロパティとは

IDENTITYプロパティは、SQL Serverで主に使用される機能で、一意の連続する数値を自動的に生成します。これは、たとえば顧客IDや注文番号など、各レコードに固有の識別子を割り当てる際に便利です。

---

### IDENTITYプロパティの設定方法

IDENTITYプロパティは、テーブル作成時またはALTER TABLEステートメントを使用して列に追加できます。基本的な構文は次のとおりです。

```sql
CREATE TABLE [テーブル名] (
    [列名] [データ型] IDENTITY([開始値], [増分]),
    ...
);
```

ここで、`[開始値]`はIDENTITY列の初期値、`[増分]`は各レコードが追加されるたびに増加する値です。

---

### IDENTITYプロパティの利点

IDENTITYプロパティの主な利点は、一意性が保証されることと、データの挿入時に自動で値が割り当てられるため、アプリケーション側での管理が容易になることです。

---

### C#でのIDENTITYプロパティの使用

C#アプリケーションでSQL Serverと連携する場合、IDENTITYプロパティはADO.NETを使用して簡単に扱うことができます。データベースからIDENTITY列の値を取得する方法は以下の通りです。

```csharp
string connectionString = "[接続文字列]";
using (SqlConnection connection = new SqlConnection(connectionString))
{
    connection.Open();
    SqlCommand command = new SqlCommand("INSERT INTO [テーブル名] OUTPUT INSERTED.[IDENTITY列] VALUES ([その他の値])", connection);
    int identityValue = (int)command.ExecuteScalar();
}
```

ここでは、`OUTPUT INSERTED.[IDENTITY列]`句を使用して、挿入された行のIDENTITY列の値を取得しています。

---

### 注意点とトラブルシューティング

IDENTITYプロパティは非常に便利ですが、いくつか注意すべき点があります。たとえば、IDENTITY列の値は手動で設定することができません（`SET IDENTITY_INSERT`ステートメントを使用することで一時的に設定は可能）。また、削除や挿入が行われた場合、IDENTITY列の値にギャップが生じる可能性があります。