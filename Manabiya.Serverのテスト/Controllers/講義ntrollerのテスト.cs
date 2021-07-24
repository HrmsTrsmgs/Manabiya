using Marimo.Manabiya.Server.Controllers;
using System;
using Xunit;
using FluentAssertions;
using Marimo.Manabiya.Shared;

namespace Marimo.Manabiya.Test.Server.Controllers
{
    public class 講義Controllerのテスト

    {
        講義Controller テスト対象 = new 講義Controller();

        public 講義Controllerのテスト()
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
        public void Getで挿入した講義が取得できます()
        {
            var 講義 = new 講義 { Id = 0 };
            テスト対象.Put(講義);

            var 結果 = テスト対象.Get();
            結果.Should().HaveCount(1);
            結果.Should().ContainSingle(x => x == 講義);
        }

        [Fact]
        public void Getで挿入した講義が全て取得できます()
        {
            var 講義0 = new 講義 { Id = 0 };
            テスト対象.Put(講義0);
            var 講義1 = new 講義 { Id = 1 };
            テスト対象.Put(講義1);


            var 結果 = テスト対象.Get();
            結果.Should().HaveCount(2);
            結果.Should().Contain(講義0);
            結果.Should().Contain(講義1);
        }

        [Fact]
        public void Getでidを指定して講義が全て取得できます()
        {
            var 講義0 = new 講義 { Id = 0 };
            テスト対象.Put(講義0);
            var 講義1 = new 講義 { Id = 1 };
            テスト対象.Put(講義1);


            var 結果 = テスト対象.Get(1);
            結果.Should().Be(講義1);
        }

        [Fact]
        public void Getでidを指定して指定したidがなかった場合はnullを返します()
        {
            テスト対象.Put(new 講義 { Id = 0 });

            var 結果 = テスト対象.Get(1);
            結果.Should().BeNull();
        }

        [Fact]
        public void Postで挿入ができます()
        {
            テスト対象.Post(new 講義 { Id = 0 });

            テスト対象.Get().Should().HaveCount(1);
        }

        [Fact]
        public void Putで挿入ができます()
        {
            テスト対象.Put(new 講義 { Id = 0 });

            テスト対象.Get().Should().HaveCount(1);
        }

        [Fact]
        public void Putでidが同じ講義を挿入すると上書きされます()
        {
            テスト対象.Put(new 講義 { Id = 0 });
            var 講義0 = new 講義 { Id = 0 };
            テスト対象.Put(講義0);

            var 結果 = テスト対象.Get();
            結果.Should().HaveCount(1);
            結果.Should().ContainSingle(x => x == 講義0);
        }

        [Fact]
        public void Deleteで指定したidの講義が削除されます()
        {
            var 講義0 = new 講義 { Id = 0 };
            テスト対象.Put(講義0);
            var 講義1 = new 講義 { Id = 1 };
            テスト対象.Put(講義1);

            テスト対象.Delete(0);
            var 結果 = テスト対象.Get();
            結果.Should().HaveCount(1);
            結果.Should().ContainSingle(x => x == 講義1);
        }

    }
}
