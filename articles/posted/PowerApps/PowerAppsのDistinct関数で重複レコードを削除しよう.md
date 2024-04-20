## Distinct関数の構文

Distinct関数の構文は以下の通りです。

```
Distinct(テーブル, [カラム])
```

- 第1引数のテーブルには、重複を除去する対象のテーブルを指定します。
- 第2引数のカラムは省略可能で、重複チェックの対象とするカラムを1つ以上指定します。省略した場合は、全カラムの組み合わせで重複チェックが行われます。

Distinct関数の戻り値は、重複が除去された新しいテーブルです。
元のテーブルは変更されないため、変数に格納するなどして再利用することができます。

## 使用例1: 全カラムの重複を除去する

以下は、全カラムの組み合わせで重複するレコードを除去する例です。

```
ClearCollect(EmployeeList, 
    Table(
        {ID: 1, Name: "山田太郎", Department: "営業部"},
        {ID: 2, Name: "鈴木花子", Department: "総務部"},
        {ID: 3, Name: "佐藤次郎", Department: "営業部"},
        {ID: 1, Name: "山田太郎", Department: "営業部"}
    )
);

ClearCollect(UniqueEmployeeList, Distinct(EmployeeList))
```

- EmployeeListコレクションには、IDが1の山田太郎のレコードが2件含まれています。
- Distinct関数を使って重複を除去し、結果をUniqueEmployeeListコレクションに格納しています。
- 第2引数を省略しているため、ID、Name、Departmentの全カラムの組み合わせで重複チェックが行われます。

UniqueEmployeeListの中身を確認すると、重複していたIDが1のレコードが1件だけ残っていることがわかります。

## 使用例2: 特定カラムの重複を除去する

次の例は、特定のカラムのみを対象に重複を除去するものです。

```
ClearCollect(EmployeeList,
    Table(
        {ID: 1, Name: "山田太郎", Department: "営業部"},
        {ID: 2, Name: "鈴木花子", Department: "総務部"},
        {ID: 3, Name: "佐藤次郎", Department: "営業部"},
        {ID: 4, Name: "山田太郎", Department: "広報部"}
    )
);

ClearCollect(UniqueDepartmentList, Distinct(EmployeeList, Department))
```

- 第2引数でDepartmentカラムを指定しているため、部署名の重複のみがチェックされます。
- EmployeeListには同姓同名の山田太郎が2人いますが、所属部署が異なるため、両方のレコードが残ります。
- UniqueDepartmentListには、営業部、総務部、広報部の3レコードが格納されます。

このように、特定のカラムを指定することで、部分的な重複除去が可能になります。
データ分析の要件に応じて、適切なカラムを選択するようにしましょう。

## 使用例3: 関数呼び出しと組み合わせる

Distinct関数は、他の関数と組み合わせて使用することもできます。
以下は、Filter関数で抽出したレコードから重複を除去する例です。

```
ClearCollect(EmployeeList,
    Table(
        {ID: 1, Name: "山田太郎", Department: "営業部", Gender: "男性"},
        {ID: 2, Name: "鈴木花子", Department: "総務部", Gender: "女性"},
        {ID: 3, Name: "佐藤次郎", Department: "営業部", Gender: "男性"},
        {ID: 4, Name: "山田花子", Department: "営業部", Gender: "女性"}
    )
);

ClearCollect(
    UniqueFemaleSalesEmployeeList, 
    Distinct(
        Filter(EmployeeList, Department = "営業部", Gender = "女性"),
        Name
    )
)
```

- Filter関数を使って、営業部の女性社員だけを抽出しています。
- Distinct関数に渡すテーブルとして、Filter関数の結果を指定しています。
- 第2引数でNameカラムを指定しているため、氏名の重複がチェックされます。

結果として、営業部の女性社員である山田花子だけがUniqueFemaleSalesEmployeeListに格納されます。
このように、他の関数と連携させることで、より柔軟なデータ処理が可能になります。

## 使用例4: 集計関数と組み合わせる

Distinct関数は、集計関数とも相性が良いです。
以下は、売上テーブルから重複を除去し、商品別の売上金額を集計する例です。

```
ClearCollect(SalesTable,
    Table(
        {ID: 1, Product: "A", Amount: 1000},
        {ID: 2, Product: "B", Amount: 2000},
        {ID: 3, Product: "A", Amount: 500}, 
        {ID: 4, Product: "C", Amount: 1500},
        {ID: 5, Product: "B", Amount: 1000}        
    )
);

ClearCollect(
    ProductSalesSum,
    AddColumns(
        Distinct(SalesTable, Product),
        "TotalAmount", Sum(Filter(SalesTable, Product = Product), Amount)
    )
)
```

- Distinct関数で商品名の重複を除去し、ユニークな商品レコードを作成しています。
- AddColumns関数を使って、商品レコードにTotalAmountカラムを追加しています。
- Sum関数とFilter関数を組み合わせて、商品名が一致するレコードの売上金額を合計しています。

ProductSalesSumには、以下のようなレコードが格納されます。

| Product | TotalAmount |
|---------|-------------|
| A       | 1500        |
| B       | 3000        |
| C       | 1500        |

レコード数が少ない例ですが、実際のデータではかなりの数のレコードを処理することになるでしょう。
Distinct関数を活用することで、パフォーマンスの高い集計処理を実現できます。

## 注意点

Distinct関数の使用に際しては、以下の点に注意が必要です。

- 完全一致でないと重複と見なされない
  - 数値の場合は完全一致しますが、テキストの場合は大文字/小文字や全角/半角の違いがあると、別のデータとして扱われます。
  - 必要に応じて、Upper関数やLower関数などで文字列を正規化してからDistinct関数に渡しましょう。

- レコードの順序が保証されない
  - Distinct関数によってレコードの順序が変わることがあります。
  - レコードの順序を維持したい場合は、追加でSort関数を使う必要があります。

- 重複除去だけでは不十分なケースがある
  - Distinct関数は単純に重複を削除するだけです。
  - 実際のデータ処理では、重複レコードの集約が必要なケースもあります。
  - 例えば、売上テーブルの例では、商品IDごとに売上金額を合計するなどの処理が考えられます。