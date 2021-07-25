using Marimo.Manabiya.Server.Controllers;
using System;
using Xunit;
using FluentAssertions;
using Marimo.Manabiya.Shared;
using System.Linq;

namespace Marimo.Manabiya.Test.Server.Controllers
{
    public class 勉強会テーマControllerのテスト

    {
        勉強会テーマController テスト対象 = new 勉強会テーマController();

        public 勉強会テーマControllerのテスト()
        {
            foreach(var data in テスト対象.Get())
            {
                テスト対象.Delete(data.Id);
            }
        }

        [Fact]
        public void Getは初期状態で空を返します()
        {
            テスト対象.Get().Should().BeEmpty();
        }

        [Fact]
        public void Getで挿入した勉強会テーマが取得できます()
        {
            var 勉強会テーマ = new 勉強会テーマ { Id = 0 };
            テスト対象.Put(勉強会テーマ);

            var 結果 = テスト対象.Get();
            結果.Should().HaveCount(1);
            結果.Should().ContainSingle(x => x == 勉強会テーマ);
        }

        [Fact]
        public void Getで挿入した勉強会テーマが全て取得できます()
        {
            var 勉強会テーマ0 = new 勉強会テーマ { Id = 0 };
            テスト対象.Put(勉強会テーマ0);
            var 勉強会テーマ1 = new 勉強会テーマ { Id = 1 };
            テスト対象.Put(勉強会テーマ1);


            var 結果 = テスト対象.Get();
            結果.Should().HaveCount(2);
            結果.Should().Contain(勉強会テーマ0);
            結果.Should().Contain(勉強会テーマ1);
        }

        [Fact]
        public void Getでidを指定して勉強会テーマが全て取得できます()
        {
            var 勉強会テーマ0 = new 勉強会テーマ { Id = 0 };
            テスト対象.Put(勉強会テーマ0);
            var 勉強会テーマ1 = new 勉強会テーマ { Id = 1 };
            テスト対象.Put(勉強会テーマ1);


            var 結果 = テスト対象.Get(1);
            結果.Should().Be(勉強会テーマ1);
        }

        [Fact]
        public void Getでidを指定して指定したidがなかった場合はnullを返します()
        {
            テスト対象.Put(new 勉強会テーマ { Id = 0 });

            var 結果 = テスト対象.Get(1);
            結果.Should().BeNull();
        }

        [Fact]
        public void Postで最初の要素はidを0にして挿入ができます()
        {
            テスト対象.Post(new 勉強会テーマ { Id = 100 });

            var 結果 = テスト対象.Get();
            結果.Should().HaveCount(1);
            結果.ElementAt(0).Id.Should().Be(0);
        }

        [Fact]
        public void Postでidは自動で採番されます()
        {
            テスト対象.Post(new 勉強会テーマ { Id = 100 });
            テスト対象.Post(new 勉強会テーマ { Id = 100 });

            var 結果 = テスト対象.Get();
            結果.Should().HaveCount(2);
            結果.Should().ContainSingle(x => x.Id == 0);
            結果.Should().ContainSingle(x => x.Id == 1);
        }

        [Fact]
        public void Putでidが同じ勉強会テーマを更新すると上書きされます()
        {
            テスト対象.Put(new 勉強会テーマ { Id = 0 });
            var 勉強会テーマ0 = new 勉強会テーマ { Id = 0 };
            テスト対象.Put(勉強会テーマ0);

            var 結果 = テスト対象.Get();
            結果.Should().HaveCount(1);
            結果.Should().ContainSingle(x => x == 勉強会テーマ0);
        }

        [Fact]
        public void Putで挿入ができます()
        {
            テスト対象.Put(new 勉強会テーマ { Id = 0 });

            テスト対象.Get().Should().HaveCount(1);
        }

        [Fact]
        public void Deleteで指定したidの勉強会テーマが削除されます()
        {
            var 勉強会テーマ0 = new 勉強会テーマ { Id = 0 };
            テスト対象.Put(勉強会テーマ0);
            var 勉強会テーマ1 = new 勉強会テーマ { Id = 1 };
            テスト対象.Put(勉強会テーマ1);

            テスト対象.Delete(0);
            var 結果 = テスト対象.Get();
            結果.Should().HaveCount(1);
            結果.Should().ContainSingle(x => x == 勉強会テーマ1);
        }

    }
}
