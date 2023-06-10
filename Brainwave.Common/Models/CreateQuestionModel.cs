using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainwave.Common.Models
{
    public record CreateQuestionModel
    {
        public string Text { get; set; } = string.Empty;
    }
}
