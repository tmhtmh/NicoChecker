# NicoChecker

## 機能紹介
* ニコニコ動画のマイリスト内動画の既読管理を行うアプリです
* TOPページでは、登録したマイリスト一覧が表示され、マイリスト毎に未読動画数を表示します。
* TOPページの+ボタンをタップすることで、監視したいマイリストを追加することができます（マイリスト番号を入力して追加）
* マイリストにはタグとコメントを追加することができます
* マイリストをタップすると、マイリスト内の動画一覧が表示されます（未読動画には青丸のマークがつきます）
* 動画をタップすると、既読未読を変更したり、動画ページに遷移することができます

## 開発環境
* macOS
* Xamarin Studio

## 利用しているOSS
* [xam.plugin.Settings](https://github.com/jamesmontemagno/SettingsPlugin)
設定値管理のために利用

## MasterDetailPage
* このPageを利用することで、左からのメニュー表示を簡単に実現することができます
* `MasterDetailPage`を継承した画面を作成し、その画面のコンストラクタで、`Detail`と`Master`を指定します
* `Detail`がメインとなる画面で、`Master`が左から出るメニューの画面です

```cs:MainPage.xaml.cs
public MainPage()
{
    InitializeComponent();
    DetailPage = new MainDetailPage();
    this.Detail = Utils.createNavigation(DetailPage);
    this.Master = new MainMasterPage(this, DetailPage);
}
```

## Toolbarにボタンを追加する

```cs:MainDetailPage.xaml.cs
ToolbarItems.Add(new ToolbarItem("追加"/** 表示したい文字 */, "ic_add.png"/** 表示したい画像*/, async () =>
{
    // ボタンが押された時の処理を書く
    await Navigation.PushAsync(new AddPlayListManualPage(this, this.Service));
}));
```

## ListView
* `ItemsSource`に表示したいアイテムのリストを設定する
```cs:MainDetailPage.xaml.cs
this.ListView.ItemsSource = DisplayPlaylists;
```

* `ItemSource`に、`ObservableCollection`を指定すると、コレクションの値の変更が直ちにUIに反映されます
```cs:MainDetailPage.xaml.cs
private ObservableCollection<Playlist> DisplayPlaylists = new ObservableCollection<Playlist>();
...
// コレクションを変更するだけで、直ちにUI表示に反映されます
this.DisplayPlaylists.Add(playlist);
```

## ListViewのCellのContextActions
* `ContextActions`をセットすることで、Androidでは長押ししたとき、iOSではセルを左にスワイプした時に、セルに対する操作メニューを表示することができます
* `Text`にはメニューに表示するテキスト、`Icon`にはメニューに表示するアイコン（Androidのみ有効）を指定します
* `IsDestructive`に`true`を設定すると、強調するメニューになります（iOSだと赤背景の白文字メニュー、削除など強調したいメニューの場合に利用する）
* `CommandParameter`に`"{Binding .}"`を指定することで、メニューが押された時のイベントの引数に、自身のアイテムを渡すことができる
* `Clicekd`に、メニューが押された時のイベントメソッド名を指定する

```xml:MainDetailPage.xaml
<ViewCell>
    <ViewCell.ContextActions>
        <MenuItem Text="削除" Icon="ic_delete.png" IsDestructive="true" CommandParameter="{Binding .}" Clicked="Handle_DeleteClicked" />
        ...
    </ViewCell.ContextActions>
    ...
</ViewCell>
```

```cs:MainDetailPage.xaml.cs
private void Handle_DeleteClicked(object sender, System.EventArgs e)
{
    // どのアイテムが押されたかを取得する
    Playlist playlist = (Playlist)((MenuItem)sender).CommandParameter;

    // 押されたアイテムに対する処理を記述する
    ...
}　
```
## アプリがフォアグラウンドになった時に処理をする方法
* `Application`を継承するクラスの`OnResume`メソッドに処理を記述する
```cs:App.xaml.cs
protected async override void OnResume()
{
    // Handle when your app resumes
    // 履歴取得 
    if (string.IsNullOrEmpty(Settings.NicoLoginCookies))
    {
        return;
    } 
    await DetailPage.Start();
}
```

## 設定値の管理
* `xam.plugin.Settings`を導入すると、helper/Settings.csファイルが自動で生成される
* 上記ファイルに管理したい設定値を追加していく(key, value)
```cs:Settings.cs
public static string NicoUser
{
    get
    {
        return AppSettings.GetValueOrDefault<string>(Keys.NicoUser.ToString(), SettingsDefault);
    }
    set
    {
        AppSettings.AddOrUpdateValue<string>(Keys.NicoUser.ToString(), value);
    }
}
```
