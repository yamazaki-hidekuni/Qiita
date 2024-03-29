#### 現在時間の取得

PowerAppsで現在の時間を取得するには、主に`Now()`関数を使用します。この関数は、現在の日付と時間を返します。時間はユーザーのローカルタイムゾーンに基づいています。

```plaintext
Now()
```

#### 使用例

例えば、テキストラベルのテキストプロパティを使用して、現在の日時を表示したい場合、以下の式を使用します。

```plaintext
Text(Now(), "yyyy/mm/dd hh:mm:ss")
```

この式は、現在の日付と時間を「年/月/日 時:分:秒」の形式で表示します。`Text`関数は、日時を特定の形式のテキストに変換するために使用されます。

#### 日付と時間の書式設定

`Text`関数を使用すると、日付と時間をさまざまな形式で表示することができます。以下にいくつかの例を示します。

- **年月日のみ表示:** `Text(Now(), "yyyy/mm/dd")`
- **時間のみ表示 (24時間表記):** `Text(Now(), "HH:mm")`
- **AM/PMを含む時間:** `Text(Now(), "hh:mm tt")`

#### 注意点

- `Now()`関数は、アプリケーションが開かれている間、定期的に更新されます。これにより、アプリ内の日時が常に現在のものに保たれます。
- ユーザーによっては異なるタイムゾーンが適用されるため、グローバルに使用するアプリの場合、表示される時間には注意が必要です。
- 特定の時刻に基づいてアクションをトリガする場合、サーバー側の時間とのズレに注意しましょう。
