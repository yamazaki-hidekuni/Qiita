#### 1. `git checkout`の多機能性

`git checkout`は2つの主なタスクを行ってきました。
1. ブランチやコミットへの切り替え
2. ワーキングディレクトリのファイルを特定の状態に戻す

これらの機能性が1つのコマンドに詰め込まれていることが、混乱の一因となっていました。

#### 2. 新コマンド: `git switch`と`git restore`

Git 2.23では、上述の2つのタスクを明確に分離する新しいコマンドが導入されました。
- `git switch`: ブランチやコミットへの切り替えに特化
- `git restore`: ワーキングディレクトリやステージングエリアの変更を復元するためのコマンド

この変更により、各コマンドの役割が明確になり、Gitの操作が直感的になりました。

#### 3. コマンドの変更点: 対応表

| **古い`git checkout`の使用方法** | **新しいコマンド** |
|-----------------------------|-------------------|
| `git checkout <branch>`       | `git switch <branch>` |
| `git checkout -b <new-branch>`| `git switch -c <new-branch>` |
| `git checkout <file>`         | `git restore --source=HEAD --staged --worktree <file>` |
| `git checkout -- <file>`      | `git restore --source=HEAD --worktree <file>` |
| `git checkout --patch <file>` | `git restore --source=HEAD --patch <file>` |

#### 4. `git restore`の注意点

`git restore`をオプションやファイルパス指定なしで実行すると、`fatal: you must specify path(s) to restore`というエラーメッセージが表示されます。これは、対象となるファイルやディレクトリのパスを明示的に指定する必要があるためです。
