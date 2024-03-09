## Hangfireとは

Hangfireは、.NETアプリケーション用のオープンソースのバックグラウンドジョブ処理ライブラリです。これを使用すると、バックグラウンドで実行するための長時間実行のジョブや定期的なタスクを簡単にスケジュールできます。以下はHangfireの主な特徴と利点です。

### Hangfireの主な特徴

1. **背景タスク**: Hangfireは、バックグラウンドで実行するためのさまざまな種類のタスク（単発のタスク、定期的なタスク、遅延タスクなど）をスケジュールする機能を提供します。

2. **永続性**: ジョブはデータベースに保存されるため、アプリケーションの再起動時に失われることはありません。これにより、高い信頼性が保証されます。

3. **ダッシュボード**: Hangfireには、ジョブの監視や管理を行うためのWebベースのダッシュボードが含まれています。このダッシュボードを使用すると、ジョブの状態の確認、履歴の閲覧、実行中のジョブのキャンセルなどが行えます。

4. **スケーラビリティ**: 大量のジョブを効率的に処理できるように設計されており、スケーラビリティに優れています。

5. **多様なストレージオプション**: SQL Server、Redis、MongoDB、PostgreSQLなど、さまざまな種類のストレージオプションをサポートしています。

6. **失敗時のリトライ**: ジョブの実行が失敗した場合、自動的にリトライする機能があります。

### Hangfireの利用例

- **バッチ処理**: 定期的にデータベースをクリーンアップする、レポートを生成するなどのバッチ処理をバックグラウンドで実行できます。

- **メール送信**: メール送信などのリソースを多く消費するタスクを、ユーザーのリクエスト処理から切り離してバックグラウンドで実行できます。

- **長時間実行ジョブ**: ユーザーインターフェースをブロックしないように、長時間実行が必要なジョブをバックグラウンドで実行します。

---

### セットアップ

Hangfireのセットアップは、まず、NuGetパッケージマネージャを使用してHangfireをプロジェクトに追加します。

```csharp
Install-Package Hangfire
```

次に、Startup.csにHangfireサービスとミドルウェアを追加します。

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Hangfireの設定
    services.AddHangfire(x => x.UseSqlServerStorage("<connection string>"));
}

public void Configure(IApplicationBuilder app, IBackgroundJobClient backgroundJobs)
{
    app.UseHangfireDashboard();
    app.UseHangfireServer();
}
```

---

### ジョブの作成とスケジューリング

Hangfireでは、ジョブを即時実行、遅延実行、繰り返し実行のいずれかの方法でスケジュールできます。

#### 即時実行

```csharp
backgroundJobs.Enqueue(() => Console.WriteLine("即時実行のジョブ"));
```

#### 遅延実行

```csharp
backgroundJobs.Schedule(() => Console.WriteLine("30秒後に実行"), TimeSpan.FromSeconds(30));
```

#### 繰り返し実行

```csharp
RecurringJob.AddOrUpdate(() => Console.WriteLine("毎日実行"), Cron.Daily);
```

---

### Hangfireダッシュボード

Hangfireには、ジョブのスケジュールや実行状況を確認できるダッシュボードが付属しています。これはウェブインターフェースを通じて簡単にアクセスできます。

---

### SQL Server Management Studio (SSMS)との統合

Hangfireは、SQL Serverをバックエンドストレージとして使用することができます。これにより、SSMSを使用してジョブの状態や履歴を管理・監視することが可能です。

