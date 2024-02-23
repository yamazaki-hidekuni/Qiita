### Google Maps APIのアニメーション機能: DROPとBOUNCE

Google Maps APIでは、マーカーを地図上に表示する際にアニメーション効果を追加することができます。特に`google.maps.Animation.DROP`と`google.maps.Animation.BOUNCE`は、マーカーの表示をより目を引くものにするためによく使用されるアニメーションです。これらのアニメーションは、ユーザーの注意を引きつけたり、特定の場所を強調表示するのに役立ちます。以下では、これら二つのアニメーション効果の特徴と実装方法について解説します。

#### google.maps.Animation.DROP

`DROP`アニメーションは、マーカーが画面の上から落ちてきて、地図上の指定された位置に着地するようなエフェクトです。このアニメーションは、新しいマーカーを追加する際に特に効果的で、ユーザーにその位置を強く意識させることができます。

##### 実装方法

```javascript
var marker = new google.maps.Marker({
    position: {lat: -34.397, lng: 150.644},
    map: map,
    animation: google.maps.Animation.DROP
});
```

このコードスニペットは、緯度-34.397、経度150.644の位置に`DROP`アニメーションを持つマーカーを作成し、地図`map`に追加します。

#### google.maps.Animation.BOUNCE

`BOUNCE`アニメーションは、マーカーが地図上で連続的に上下に跳ねるエフェクトです。このアニメーションは、ユーザーの注意を引くためや、アクティブな状態や選択された場所を示すのに適しています。

##### 実装方法

```javascript
var marker = new google.maps.Marker({
    position: {lat: -34.397, lng: 150.644},
    map: map,
    animation: google.maps.Animation.BOUNCE
});
```

この例では、同じ位置に`BOUNCE`アニメーションを持つマーカーを配置しています。アニメーションを停止させるには、マーカーの`setAnimation(null)`メソッドを呼び出します。

#### アニメーションの制御

マーカーにアニメーションを設定した後、特定のアクション（例えば、ユーザーがマーカーをクリックした時）に応じてアニメーションを開始または停止させることができます。例えば、`BOUNCE`アニメーションを持つマーカーをクリックするとアニメーションが停止するように設定することが可能です。

```javascript
marker.addListener('click', function() {
    if (marker.getAnimation() !== null) {
        marker.setAnimation(null);
    } else {
        marker.setAnimation(google.maps.Animation.BOUNCE);
    }
});
```

このコードは、マーカーがクリックされた時に、アニメーションが既に実行中であれば停止し、そうでなければ`BOUNCE`アニメーションを開始するように設定します。