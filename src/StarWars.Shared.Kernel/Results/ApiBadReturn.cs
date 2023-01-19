using System.Collections.Generic;

namespace StarWars.Shared.Kernel.Results
{
    public class ApiBadReturn
    {
        public bool Success { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
