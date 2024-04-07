## Connection.Connectedプロパティとは？

Connection.Connectedは、PowerAppsの組み込みプロパティの一つです。このプロパティは、デバイスがインターネットに接続されているかどうかを示すブール値（真偽値）を返します。

- インターネットに接続されている場合は`true`を返します。
- インターネットに接続されていない場合は`false`を返します。

このプロパティを使用することで、アプリがオンラインかオフラインかを判断し、それぞれの状態に応じた処理を行うことができます。

## Connection.Connectedプロパティの使い方

Connection.Connectedプロパティは、主に以下のような場面で使用されます。

1. オフライン対応アプリの開発
2. データの同期処理
3. ネットワーク接続状態に応じたUI制御

### オフライン対応アプリの開発

オフライン対応アプリを開発する際、Connection.Connectedプロパティを使ってオンライン/オフラインを判断し、それぞれの状態に適した処理を行います。

```
If(Connection.Connected,
    // オンラインの場合の処理
    Collect(ProductsCollection, Products),
    // オフラインの場合の処理
    Collect(ProductsCollection, ProductsOfflineCollection)
)
```

上記の例では、オンラインの場合は「Products」テーブルからデータを取得し、オフラインの場合は「ProductsOfflineCollection」からデータを取得しています。

### データの同期処理

データの同期処理を行う際、Connection.Connectedプロパティを使ってオンライン/オフラインを判断し、適切なタイミングでデータを同期します。

```
If(Connection.Connected,
    // オンラインの場合、データを同期
    Patch(Products, ProductsCollection),
    // オフラインの場合、何もしない
    Notify("オフラインのためデータを同期できません。", NotificationType.Warning)
)
```

この例では、オンラインの場合はPatch関数を使ってデータを同期し、オフラインの場合は通知を表示しています。

### ネットワーク接続状態に応じたUI制御

ネットワーク接続状態に応じてUIを制御したい場合、Connection.Connectedプロパティを使って表示内容を切り替えます。

```
If(Connection.Connected,
    // オンラインの場合、通常のUIを表示
    Screen1,
    // オフラインの場合、オフライン用のUIを表示
    OfflineScreen
)
```

この例では、オンラインの場合は「Screen1」を表示し、オフラインの場合は「OfflineScreen」を表示しています。