# Qiita Auto Post Project

![C#](https://img.shields.io/badge/-C%23-239120?style=flat-square&logo=c-sharp&logoColor=white)

![.NET](https://img.shields.io/badge/-.NET-512BD4?style=flat-square&logo=dot-net&logoColor=white)

![Markdown](https://img.shields.io/badge/-Markdown-000000?style=flat-square&logo=markdown&logoColor=white)

![GitHub Actions](https://img.shields.io/badge/-GitHub%20Actions-2088FF?style=flat-square&logo=github-actions&logoColor=white)

![Build Status](https://github.com/yazaki-hidekuni/Qiita/workflows/Qiita_Auto_Post/badge.svg)

![Last Commit](https://img.shields.io/github/last-commit/yazaki-hidekuni/Qiita)

![Languages Count](https://img.shields.io/github/languages/count/yazaki-hidekuni/Qiita)


## 概要
このプロジェクトは、GitHubリポジトリに保存されたMarkdown形式の記事を自動的にQiitaに投稿するためのものです。GitHub Actionsを使用して定期的に記事をチェックし、未投稿の記事をQiitaに投稿します。

## 機能

- Qiitaに自動投稿：未投稿の記事をQiitaに自動投稿します。
- タグ別管理：記事はタグごとに分類され、整理されています。
- 投稿済み・未投稿の管理：記事は投稿済みと未投稿で分けて管理されます。

## 使用方法
1. 'articles/unposted' ディレクトリにMarkdown形式で記事を作成します。
2. 記事には適切なタグをつけて、タグごとのサブディレクトリに配置します。
3. Githubリポジトリにプッシュします。
4. GitHub Actionsが定期的にリポジトリをチェックし、未投稿の記事をQiitaに投稿します。
5. 投稿された記事は 'articles/posted' ディレクトリに移動されます。

## プロジェクト構造

```
QiitaAutoPostProject/
│
├── .github/
│   └── workflows/
│       └── qiita_auto_post.yml  # GitHub Actionsのワークフロー定義
│
├── articles/
│   ├── unposted/                # 未投稿の記事を保存するディレクトリ
│   │   ├── Azure/
│   │   │   └── Azure1.md        # Azureに関する未投稿の記事
│   │   └── Git/
│   │       └── Git1.md          # Gitに関する未投稿の記事
│   └── posted/                  # 投稿済みの記事を保存するディレクトリ
│       ├── Azure/
│       └── Git/
├── src/
│   ├── QiitaAutoPost/
│   │   └── Program.cs           # メインの実行ファイル
│   └── QiitaAutoPost.csproj     # プロジェクトファイル
│
├── .gitignore                   # Gitの無視ファイルリスト
├── Qiita.sln                    # ソリューションファイル
└── README.md                    # プロジェクトの説明や使い方を記述
```
