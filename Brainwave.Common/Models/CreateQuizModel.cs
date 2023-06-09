using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainwave.Common.Models
{
    public record CreateQuizModel
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
