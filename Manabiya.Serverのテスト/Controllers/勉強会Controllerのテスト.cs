using Marimo.Manabiya.Server.Controllers;
using System;
using Xunit;
using FluentAssertions;
using Marimo.Manabiya.Shared;

namespace Marimo.Manabiya.Test.Server.Controllers
{
    public class 勉強会Controllerのテスト

    {
        勉強会Controller テスト対象 = new 勉強会Controller();

        public 勉強会Controllerのテスト()
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
        public void Getで挿入した勉強会が取得できます()
        {
            var 勉強会 = new 勉強会 { Id = 0 };
            テスト対象.Put(勉強会);

            var 結果 = テスト対象.Get();
            結果.Should().HaveCount(1);
            結果.Should().ContainSingle(x => x == 勉強会);
        }

        [Fact]
        public void Getで挿入した勉強会が全て取得できます()
        {
            var 勉強会0 = new 勉強会 { Id = 0 };
            テスト対象.Put(勉強会0);
            var 勉強会1 = new 勉強会 { Id = 1 };
            テスト対象.Put(勉強会1);


            var 結果 = テスト対象.Get();
            結果.Should().HaveCount(2);
            結果.Should().Contain(勉強会0);
            結果.Should().Contain(勉強会1);
        }

        [Fact]
        public void Getでidを指定して勉強会が全て取得できます()
        {
            var 勉強会0 = new 勉強会 { Id = 0 };
            テスト対象.Put(勉強会0);
            var 勉強会1 = new 勉強会 { Id = 1 };
            テスト対象.Put(勉強会1);


            var 結果 = テスト対象.Get(1);
            結果.Should().Be(勉強会1);
        }

        [Fact]
        public void Getでidを指定して指定したidがなかった場合はnullを返します()
        {
            テスト対象.Put(new 勉強会 { Id = 0 });

            var 結果 = テスト対象.Get(1);
            結果.Should().BeNull();
        }

        [Fact]
        public void Postで挿入ができます()
        {
            テスト対象.Post(new 勉強会 { Id = 0 });

            テスト対象.Get().Should().HaveCount(1);
        }

        [Fact]
        public void Putで挿入ができます()
        {
            テスト対象.Put(new 勉強会 { Id = 0 });

            テスト対象.Get().Should().HaveCount(1);
        }

        [Fact]
        public void Putでidが同じ勉強会を挿入すると上書きされます()
        {
            テスト対象.Put(new 勉強会 { Id = 0 });
            var 勉強会0 = new 勉強会 { Id = 0 };
            テスト対象.Put(勉強会0);

            var 結果 = テスト対象.Get();
            結果.Should().HaveCount(1);
            結果.Should().ContainSingle(x => x == 勉強会0);
        }

        [Fact]
        public void Deleteで指定したidの勉強会が削除されます()
        {
            var 勉強会0 = new 勉強会 { Id = 0 };
            テスト対象.Put(勉強会0);
            var 勉強会1 = new 勉強会 { Id = 1 };
            テスト対象.Put(勉強会1);

            テスト対象.Delete(0);
            var 結果 = テスト対象.Get();
            結果.Should().HaveCount(1);
            結果.Should().ContainSingle(x => x == 勉強会1);
        }

    }
}
