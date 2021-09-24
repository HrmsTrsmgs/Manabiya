using CommunityToolkit.Mvvm.Input;
using Marimo.Manabiya.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Marimo.Manabiya.Client.ViewModels
{
    public class 予定PageViewModel : INotifyPropertyChanged
    {
        public HttpClient http = new();

        public bool isダイアログ表示中 = false;

        public 勉強会テーマ 新規テーマ = new();

        public 勉強会テーマ[] 予定一覧;

        public event PropertyChangedEventHandler PropertyChanged;

        public AsyncRelayCommand 予定一覧を読み込むCommand { get; }
        public AsyncRelayCommand 追加するCommand { get; }

        public RelayCommand<bool> ダイアログの表示を切り替える { get; }

        public 予定PageViewModel(HttpClient httpClient)
        {
            http = httpClient;
            予定一覧を読み込むCommand = new(async () =>
            {
                予定一覧 = await http.GetFromJsonAsync<勉強会テーマ[]>("勉強会テーマ");
            });

            追加するCommand = new(async () =>
            {
                await http.PostAsJsonAsync("勉強会テーマ", 新規テーマ);
                新規テーマ = new();
                isダイアログ表示中 = false;

                await 予定一覧を読み込むCommand.ExecuteAsync(null);
            });

            ダイアログの表示を切り替える = new(isダイアログ表示中 =>
            {
                this.isダイアログ表示中 = isダイアログ表示中;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(isダイアログ表示中)));
            });
              

            Task.Run(async () => await 予定一覧を読み込むCommand.ExecuteAsync(null));
        }
    }
}
