#### 1. `git switch`の基本

`git switch`は、Git 2.23で追加された新しいコマンドで、ブランチの切り替えをより直感的に行うことができます。以前は`git checkout`でブランチの切り替えやファイルの変更の復元を行っていましたが、`git switch`はブランチの切り替え専用のコマンドとなっています。

#### 2. ブランチを切り替える

現在のブランチから別のブランチへ切り替えたい場合、以下のように使用します。

```bash
git switch <branch-name>
```

例えば、`develop`ブランチに切り替えたい場合は:

```bash
git switch develop
```

#### 3. 新しいブランチを作成して切り替え

`git switch`で新しいブランチを作成しつつ、そのブランチに切り替えることも可能です。以下のコマンドを使用します。

```bash
git switch -c <new-branch-name>
```

例として、`feature-x`という新しいブランチを作成し、そのブランチに切り替えたい場合:

```bash
git switch -c feature-x
```

#### 4. `git checkout`との違い

`git checkout`はファイルの変更の復元やブランチの切り替えなど、多岐にわたる操作を一つのコマンドで行っていました。しかし、`git switch`はその名の通り、ブランチの切り替えに特化しています。この専用コマンドの導入により、操作がより明確かつ直感的になりました。
