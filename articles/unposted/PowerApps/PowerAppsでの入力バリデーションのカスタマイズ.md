## MaxLength プロパティ

MaxLength プロパティは、テキスト入力コントロールに入力可能な最大文字数を指定します。
以下は、最大20文字までの入力を受け付ける例です。

```
TextInput1.MaxLength = 20
```

## Required プロパティ  

Required プロパティは、テキスト入力コントロールが必須入力項目かどうかを指定します。
以下は、Required プロパティを true に設定して必須入力としている例です。

```
TextInput1.Required = true
```

## HintText プロパティ

HintText プロパティは、テキスト入力コントロールの背景に表示するヒントテキストを指定します。
以下は、メールアドレスの入力を促すヒントテキストを表示する例です。

```
TextInput1.HintText = "メールアドレスを入力してください"  
```

## DisabledBorderColor プロパティ

DisabledBorderColor プロパティは、テキスト入力コントロールが無効状態のときの枠線色を指定します。
以下は、コントロールが無効のときに枠線を灰色にする例です。

```
TextInput1.DisabledBorderColor = RGBA(166, 166, 166, 1)
```

## DisplayMode プロパティ

DisplayMode プロパティは、テキスト入力コントロールの表示モードを指定します。
以下は、参照モードに切り替えてテキストボックスを読み取り専用にする例です。

```
TextInput1.DisplayMode = DisplayMode.View
```

DisplayMode には以下の値を指定できます。

- DisplayMode.Edit：編集モード（既定） 
- DisplayMode.View：参照モード

## Reset 関数

Reset 関数は、テキスト入力コントロールの内容をクリアし、HintText の状態に戻します。
以下は、ボタンクリックでテキストボックスをクリアする例です。  

```
Button1.OnSelect = Reset(TextInput1)
```

## IsMatch / IsMisMatch 関数

IsMatch 関数と IsMisMatch 関数は、それぞれ正規表現によるパターンマッチングの成否を返します。
以下は、数値以外の入力があった場合にエラーメッセージを表示する例です。

```
ErrorLabel.Text = If(IsMisMatch(TextInput1.Text, "^\d*$"), "数字で入力してください！", "")
ErrorLabel.Visible = IsMisMatch(TextInput1.Text, "^\d*$")
```

"^\d*$" は、数字のみで構成された文字列というパターンを表す正規表現です。
IsMisMatch 関数は条件を満たさないとき true を返すので、エラー時のみメッセージが表示されます。

## Validate 関数

Validate 関数は、指定した検証ルールの結果に基づいて書式設定する関数です。
以下は、Validate 関数を使って、必須入力チェックを行う例です。

```
TextInput1.HintText = "必須項目です"
TextInput1.Format = Validate(TextInput1.Required && Not(IsBlank(TextInput1.Text)), TextInput1.HintText, TextInput1.Text)
```

Validate 関数の第1引数に検証ルールを、第2引数にルールを満たさなかった場合に表示する代替テキストを、第3引数にルールを満たした場合に表示するテキストを指定します。

## Errors プロパティ

Errors プロパティは、テキスト入力コントロールの検証エラーメッセージを表示する領域を提供します。
以下は、Errors プロパティを使った必須入力チェックの例です。

```
TextInput1.Required = true
TextInput1.ErrorMessage = "必須項目です"  
ErrorMessage1.Error = First(TextInput1.Errors).Message
```

Required プロパティ と ErrorMessage プロパティを設定することで、未入力時にエラーメッセージが Errors プロパティに格納されます。
First 関数で最初のエラーメッセージを取得し、ErrorMessage1 ラベルに表示しています。