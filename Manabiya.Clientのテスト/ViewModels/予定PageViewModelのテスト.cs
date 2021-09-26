using FluentAssertions;
using Marimo.Manabiya.Client;
using Marimo.Manabiya.Client.ViewModels;
using Marimo.Manabiya.Shared;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Marimo.Manabiya.Test.Client.ViewModels
{
    public class 予定PageViewModelのテスト
    {
        予定PageViewModel テスト対象;

        Mock<Web通信> mock = new();

        public 予定PageViewModelのテスト()
        {
            テスト対象 = new(mock.Object);
        }

        [Fact]
        public void Isダイアログ表示中の初期値はfalseです()
        {
            テスト対象.Isダイアログ表示中.Should().BeFalse();
        }

        [Fact]
        public void Isダイアログ表示中は設定できます()
        {
            テスト対象.Isダイアログ表示中.Should().BeFalse();

            テスト対象.Isダイアログ表示中 = true;

            テスト対象.Isダイアログ表示中.Should().BeTrue();
        }

        [Fact]
        public void Isダイアログ表示中の変更は通知されます()
        {
            object senderの結果 = null;
            PropertyChangedEventArgs eの結果 = null;
            テスト対象.PropertyChanged += (sender, e) =>
            {
                senderの結果 = sender;
                eの結果 = e;
                テスト対象.Isダイアログ表示中.Should().BeTrue();
            };

            テスト対象.Isダイアログ表示中 = true;

            senderの結果.Should().NotBeNull();
            senderの結果.Should().BeSameAs(テスト対象);

            eの結果.PropertyName.Should().Be(nameof(テスト対象.Isダイアログ表示中));
        }

        [Fact]
        public void Isダイアログ表示中の変更中は通知されます()
        {
            object senderの結果 = null;
            PropertyChangingEventArgs eの結果 = null;
            テスト対象.PropertyChanging += (sender, e) =>
            {
                senderの結果 = sender;
                eの結果 = e;
                テスト対象.Isダイアログ表示中.Should().BeFalse();
            };

            テスト対象.Isダイアログ表示中 = true;

            senderの結果.Should().NotBeNull();
            senderの結果.Should().BeSameAs(テスト対象);

            eの結果.PropertyName.Should().Be(nameof(テスト対象.Isダイアログ表示中));
        }

        [Fact]
        public void 新規テーマは初期状態で設定されています()
        {
            テスト対象.新規テーマ.Should().NotBeNull();
        }

        [Fact]
        public void 予定一覧は初期状態でnullです()
        {
            テスト対象.予定一覧.Should().BeNull();
        }

        [Fact]
        public async Task 予定一覧を読み込むCommandでWebから取得した予定が予定一覧に読み込まれます()
        {
            var 結果 = new 勉強会テーマ[] { };
            mock.Setup(x => x.Get勉強会テーマAsync())
                .ReturnsAsync(結果);

            テスト対象.予定一覧.Should().BeNull();

            await テスト対象.予定一覧を読み込むCommand.ExecuteAsync(null);

            テスト対象.予定一覧.Should().BeSameAs(結果);
        }

        [Fact]
        public void ダイアログの表示を切り替えるCommandでIsダイアログ表示中を切り替えられます()
        {
            テスト対象.Isダイアログ表示中.Should().BeFalse();

            テスト対象.ダイアログの表示を切り替えるCommand.Execute(true);

            テスト対象.Isダイアログ表示中.Should().BeTrue();
        }

        [Fact]
        public async Task 追加するCommandで新規テーマがWebを通じて追加されます()
        {
            var 入力中のテーマ = テスト対象.新規テーマ;
            勉強会テーマ postされたテーマ = null;

            mock.Setup(x => x.Post勉強会テーマAsync(It.IsAny<勉強会テーマ>()))
                .Callback<勉強会テーマ>(x => postされたテーマ = x);

            await テスト対象.追加するCommand.ExecuteAsync(null);

            postされたテーマ.Should().BeSameAs(入力中のテーマ);
        }

        [Fact]
        public async Task 追加するCommandで新規テーマは新しいものに置き換えられます()
        {
            var 入力中のテーマ = テスト対象.新規テーマ;

            await テスト対象.追加するCommand.ExecuteAsync(null);

            テスト対象.新規テーマ.Should().NotBeSameAs(入力中のテーマ);
        }

        [Fact]
        public async Task 追加するCommandで新しく作られたテーマのタイトルは空です()
        {
            await テスト対象.追加するCommand.ExecuteAsync(null);

            テスト対象.新規テーマ.タイトル.Should().BeEmpty();
        }

        [Fact]
        public async Task 追加するCommandでダイアログの表示は終わります()
        {
            テスト対象.Isダイアログ表示中 = true;

            await テスト対象.追加するCommand.ExecuteAsync(null);

            テスト対象.Isダイアログ表示中.Should().BeFalse();
        }
    }
}
