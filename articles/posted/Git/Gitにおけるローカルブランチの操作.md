### Gitにおけるローカルブランチの操作

今回は、具体的な開発シナリオを例に、ローカルブランチの操作手順を解説していきます。

#### 前提条件

新しいログイン機能の開発を始めることになりました。以下のようなブランチ構成を考えています。

- `main`ブランチ：本番環境のコード。
- `develop`ブランチ：開発中のコード。
- `feature`ブランチ：ログイン機能の開発専用ブランチ。

この構成を元に、ブランチ操作の手順を見ていきましょう。

#### 手順1: `develop` ブランチでの準備

まずは、`develop`ブランチを最新の状態にしておきます。

```bash
git switch develop
git pull origin develop
```

#### 手順2: `feature` ブランチの作成とログイン機能の開発

新機能の開発のため、`develop`ブランチから`feature`ブランチを作成します。

```bash
git switch -c feature-login
```

次に、ログイン機能の開発を進め、変更をコミットします。

```bash
git add [変更したファイル]
git commit -m "ログイン機能の追加"
```

#### 手順3: `develop` ブランチにログイン機能を統合

開発が完了したら、`develop`ブランチにログイン機能を統合します。

```bash
git switch develop
git merge feature-login
```

**TIPS**: マージの際にコンフリクトが発生したら、手動で修正しましょう。コンフリクト解消後はコミットを忘れずに！

#### 手順4: `main` ブランチに変更を反映

テストなどが完了し、`develop`ブランチの内容が安定していることを確認したら、`main`ブランチに反映させます。

```bash
git switch main
git merge develop
```
