# Qiita Auto Post Project

## 概要
このプロジェクトは、GitHubリポジトリに保存されたMarkdown形式の記事を自動的にQiitaに投稿するためのものです。
GitHub Actionsを使用して定期的に記事をチェックし、未投稿の記事をQiitaに投稿します。

## 機能

- Qiitaに自動投稿：未投稿の記事をQiitaに自動投稿します。
- タグ別管理：記事はタグごとに分類され、整理されています。

## プロジェクト構造

```

QiitaAutoPostProject/
│
├── .github/
│   └── workflows/
│       └── qiita_auto_post.yml  # GitHub Actionsのワークフロー定義
│
├── articles/
│   ├── Azure/
│   │   └── Azure1.md            # Azureに関する記事
│   └── Git/                     
│   │   └── Git1.md      　      # Gitに関する記事
├── src/
│   ├── QiitaAutoPost/
│   │   └── Program.cs           # メインの実行ファイル
│   └── QiitaAutoPost.csproj     # プロジェクトファイル
│
│
├── .gitignore                   # Gitの無視ファイルリスト
├── Qiita.sln                    # ソリューションファイル（自動生成）
└── README.md                    # プロジェクトの説明や使い方を記述

```