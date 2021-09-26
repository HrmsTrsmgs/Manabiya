using CommunityToolkit.Mvvm.ComponentModel;
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
    public class 予定PageViewModel : ObservableObject
    {
        Web通信 web通信;

        bool isダイアログ表示中 = false;
        public bool Isダイアログ表示中
        {
            get => isダイアログ表示中;
            set => SetProperty(ref isダイアログ表示中, value);
        }
            

        public 勉強会テーマ 新規テーマ = new();

        public IEnumerable<勉強会テーマ> 予定一覧;

        public RelayCommand<bool> ダイアログの表示を切り替えるCommand { get; }
        public virtual AsyncRelayCommand 予定一覧を読み込むCommand { get; }
        public AsyncRelayCommand 追加するCommand { get; }


        public 予定PageViewModel(Web通信 web通信)
        {
            this.web通信 = web通信;

            ダイアログの表示を切り替えるCommand = new(isダイアログ表示中 =>
            {
                Isダイアログ表示中 = isダイアログ表示中;
            });

            予定一覧を読み込むCommand = new(async () =>
            {
                予定一覧 = await web通信.Get勉強会テーマAsync();
            });

            追加するCommand = new(async () =>
            {
                await web通信.Post勉強会テーマAsync(新規テーマ);
                新規テーマ = new();
                isダイアログ表示中 = false;

                await 予定一覧を読み込むCommand.ExecuteAsync(null);
            });
        }
    }
}
