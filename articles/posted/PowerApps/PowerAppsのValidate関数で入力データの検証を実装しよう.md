## Validate関数の構文

Validate関数の構文は以下の通りです。

```
Validate(検証ルール, 検証エラー時に返す値, 検証成功時に返す値)
```

- 第1引数の検証ルールには、true/falseを返す検証条件式を指定します。
- 第2引数には、検証ルールがfalseだった場合に返す値や式を指定します。省略した場合は空文字列が返ります。
- 第3引数には、検証ルールがtrueだった場合に返す値や式を指定します。省略可能で、既定値は空文字列です。

Validate関数の戻り値は、検証ルールの評価結果に応じて、第2引数または第3引数で指定した値になります。

## Validate関数の使用例

Validate関数の具体的な使い方をいくつか見てみましょう。

### 例1. 必須入力チェック

以下は、テキスト入力が必須であることをチェックする例です。

```
Validate(Not(IsBlank(TextInput1.Text)), "必ず入力してください", TextInput1.Text)
```

- 検証ルールのNot(IsBlank(TextInput1.Text))は、テキストボックスが空欄でない場合にtrueを返します。
- 空欄の場合は、"必ず入力してください"というエラーメッセージが返されます。
- 入力がある場合は、テキストボックスの入力値がそのまま返されます。

### 例2. 入力文字種チェック

次の例は、半角英数字以外の文字が含まれていないかをチェックします。

```
Validate(IsMatch(TextInput1.Text, "^[a-zA-Z0-9]*$"), "半角英数字で入力してください", TextInput1.Text)
```

- 検証ルールのIsMatch関数は、指定された正規表現パターンにマッチする場合にtrueを返します。
- 半角英数字以外の文字が含まれている場合は、"半角英数字で入力してください"というエラーメッセージが返されます。
- 半角英数字のみで構成されている場合は、テキストボックスの入力値がそのまま返されます。

### 例3. 複数の検証ルールを組み合わせる

以下は、Validate関数を入れ子にして、複数の検証ルールを組み合わせる例です。

```
Validate(
    Not(IsBlank(TextInput1.Text)),
    "必ず入力してください",
    Validate(
        IsMatch(TextInput1.Text, "^[a-zA-Z0-9]*$"),
        "半角英数字で入力してください",
        TextInput1.Text
    )
)
```

- 外側のValidate関数で必須入力チェックを行い、内側のValidate関数で入力文字種チェックを行っています。
- 最初の検証ルールを満たさない場合は"必ず入力してください"が返され、2番目の検証ルールを満たさない場合は"半角英数字で入力してください"が返されます。
- 両方の検証ルールを満たす場合は、テキストボックスの入力値がそのまま返されます。

## エラーメッセージの表示方法

Validate関数の戻り値をテキストプロパティに設定することで、検証エラーメッセージを画面に表示できます。
以下は、Validate関数とラベルコントロールを使ってエラーメッセージを表示する例です。

```
ErrorMessage.Text = Validate(Not(IsBlank(TextInput1.Text)), "必ず入力してください")
ErrorMessage.Visible = Not(IsBlank(ErrorMessage.Text))
```

- Validate関数の結果をErrorMessageラベルのTextプロパティに設定しています。
- ErrorMessage.Visibleプロパティは、エラーメッセージが空でないときにtrueになるよう設定しています。
- これにより、検証エラーが発生した場合にのみエラーメッセージが表示されます。

## より実用的な例

次の例は、Validate関数とパネルコントロールを使って、入力フォームを実装したものです。

```
FormPanel.Valid =
    And(
        Not(IsBlank(TextInput1.Text)),
        IsMatch(TextInput1.Text, "^[a-zA-Z0-9]*$"),
        IsMatch(TextInput2.Text, "^[0-9]{3}-[0-9]{4}$")
    )

SubmitButton.DisplayMode = If(FormPanel.Valid, DisplayMode.Edit, DisplayMode.Disabled)

TextInput1.HintText = Validate(Not(IsBlank(TextInput1.Text)), "必須項目です")
TextInput1.Format = Validate(IsMatch(TextInput1.Text, "^[a-zA-Z0-9]*$"), "半角英数字で入力してください", TextInput1.Text)

TextInput2.HintText = Validate(Not(IsBlank(TextInput2.Text)), "必須項目です")
TextInput2.Format = Validate(IsMatch(TextInput2.Text, "^[0-9]{3}-[0-9]{4}$"), "郵便番号の形式で入力してください", TextInput2.Text)
```

- FormPanel.Validプロパティで、フォーム全体の検証結果を判定しています。
- SubmitButton.DisplayModeプロパティは、フォームが有効な場合のみ編集可能になるよう設定しています。
- TextInput1とTextInput2には、それぞれHintTextプロパティとFormatプロパティにValidate関数を設定し、検証エラーメッセージとフォーマットを指定しています。

このように、Validate関数を活用することで、入力フォームの検証ロジックを宣言的に記述できます。
フォームの各項目に対して必要な検証ルールを設定し、フォーム全体の検証結果に応じて送信ボタンの活性/非活性を切り替えるのが一般的な実装パターンです。