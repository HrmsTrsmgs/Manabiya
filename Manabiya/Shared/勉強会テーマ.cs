using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marimo.Manabiya.Shared
{
    public class 勉強会テーマ
    {
        public int Id { get; init; }

        [Required(ErrorMessage = "タイトルを入力してください。")]
        public string タイトル { get; set; } = "";
    }
}
