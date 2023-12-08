## Console.WriteLineとは？

`Console.WriteLine`は、C#の標準出力メソッドであり、文字列をコンソール（コマンドライン）に出力し、その後に改行を挿入します。これにより、次の出力が新しい行から始まるようになります。

### 基本的な使い方

最も基本的な使い方は、単に文字列を引数として渡すことです。

```csharp
Console.WriteLine("Hello, World!");
```

このコードは、コンソールに"Hello, World!"と表示し、次の行にカーソルを移動します。

### 複数の引数を含む使い方

`Console.WriteLine`は、複数のオブジェクトを受け取り、それらを文字列に変換して出力することもできます。

```csharp
int number = 10;
Console.WriteLine("The number is {0}.", number);
```

この例では、`{0}`は`number`変数の値に置き換えられ、"The number is 10."と出力されます。

### 特殊文字の扱い

`Console.WriteLine`を使用するとき、特殊文字（例えば、タブや改行）を含む文字列も出力できます。

```csharp
Console.WriteLine("First line.\nSecond line.");
```

`\n`は改行文字であり、このコードは2行にわたって出力されます。

### 書式指定の利用

`Console.WriteLine`は、書式指定を使用して、数値や日付などを特定の形式で出力することができます。

```csharp
Console.WriteLine("Pi is {0:F2}.", Math.PI);
```

`{0:F2}`は、`Math.PI`の値を小数点以下2桁で丸めて出力するよう指定しています。

## Console.WriteLineの応用

`Console.WriteLine`は、デバッグや簡単な情報の表示に非常に便利です。例えば、プログラムの実行中に変数の値を確認したり、エラーメッセージを表示したりするのに使われます。

### デバッグ時の使用例

```csharp
int sum = 0;
for (int i = 1; i <= 5; i++) {
    sum += i;
    Console.WriteLine("Adding {0}, sum is now {1}.", i, sum);
}
```

このコードは、ループの各ステップで現在の合計値を表示します。

### エラーメッセージの表示

```csharp
try {
    // 危険な操作を試みる
} catch (Exception ex) {
    Console.WriteLine("An error occurred: {0}", ex.Message);
}
```

例外が発生した場合、エラーメッセージがコンソールに出力されます。