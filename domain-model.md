# 想定ドメインモデル

注文承認コンテキスト

## パイプライン

1. 注文される(イベント発行)
2. 注文検証
3. 価格計算
4. 注文承認
5. 後続イベント発信

## WF詳細

### 注文される

#### 概要

入口

#### 依存関係

多分ない

### 注文検証

#### 概要

「未検証の注文」を検証して、「検証済み注文」を作成

検証サービスは外部なので非同期

検証失敗したらError

#### 依存関係

- 住所検証サービス
- 製品コード存在サービス

### 価格計算

#### 概要

「検証済み注文」合計金額を計算し、「計算済み注文」を生成

製品価格取得失敗したらError

#### 依存関係

- 製品価格取得サービス

### 注文承認

#### 概要

「計算済み注文」を承認する

注文承認した件を発注者にお知らせする

発注者へのお知らせが失敗しても別に気にしない

#### 依存関係

- 発注者にお知らせ

### 後続イベント作成

#### 概要

注文が確定したというイベントを後続のコンテキストに発行

発行したイベントは次のコンテキストが受け取る

#### 依存関係

なし