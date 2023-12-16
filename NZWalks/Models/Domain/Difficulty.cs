using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace NZWalks.Models.Domain
{
    public class Difficulty
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
