# PowerAppsのSaveData関数でオフラインデータを保存する

今回は、PowerAppsのSaveData関数について詳しく解説します。SaveData関数は、オフライン対応アプリを開発する上で欠かせない機能の一つです。この関数を使うことで、アプリがオフラインの際にデータを端末に保存し、オンラインに戻った際にそのデータを利用することができます。

## SaveData関数とは？

SaveData関数は、PowerAppsの組み込み関数の一つです。この関数は、指定したキーと値のペアをデバイスのローカルストレージに保存します。保存されたデータは、アプリが再起動されても維持されます。

SaveData関数の構文は以下の通りです。

```
SaveData(キー, 値)
```

- `キー`：保存するデータを識別するための一意の文字列を指定します。
- `値`：保存するデータを指定します。テキスト、数値、ブール値、レコード、テーブルなどを保存できます。

## SaveData関数の使い方

SaveData関数は、主に以下のような場面で使用されます。

1. オフラインデータの保存
2. アプリの設定情報の保存
3. ユーザー固有のデータの保存

### オフラインデータの保存

オフライン対応アプリを開発する際、SaveData関数を使ってオフラインデータをデバイスに保存します。

```
SaveData("ProductsData", ProductsCollection)
```

上記の例では、「ProductsCollection」のデータを「ProductsData」というキーで保存しています。

### アプリの設定情報の保存

アプリの設定情報を保存する際、SaveData関数を使ってデバイスに保存します。

```
SaveData("Settings", {
    DarkMode: true,
    FontSize: 14
})
```

この例では、ダークモードの有効/無効と文字サイズの設定情報を「Settings」というキーで保存しています。

### ユーザー固有のデータの保存

ユーザー固有のデータを保存する際、SaveData関数を使ってデバイスに保存します。

```
SaveData("UserProfile", {
    Name: "John Doe",
    Email: "john@example.com"
})
```

この例では、ユーザーのプロファイル情報を「UserProfile」というキーで保存しています。

## SaveData関数とLoadData関数

SaveData関数で保存したデータは、LoadData関数を使って読み込むことができます。LoadData関数の構文は以下の通りです。

```
LoadData(キー)
```

- `キー`：読み込むデータを識別するためのキーを指定します。

以下は、SaveData関数で保存したデータをLoadData関数で読み込む例です。

```
If(Connection.Connected,
    // オンラインの場合、サーバーからデータを取得
    Collect(ProductsCollection, Products),
    // オフラインの場合、保存されたデータを読み込む
    Collect(ProductsCollection, LoadData("ProductsData"))
)
```

この例では、オンラインの場合はサーバーからデータを取得し、オフラインの場合はSaveData関数で保存したデータをLoadData関数で読み込んでいます。