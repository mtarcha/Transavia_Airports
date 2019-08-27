using Transavia.Domain.Common;

namespace Transavia.Domain
{
    public class Size : Enumeration<Size, int>
    {
        public static Size Unknown = new Size(1, "null");
        public static Size Small = new Size(1, "small");
        public static Size Medium = new Size(1, "medium");
        public static Size Large = new Size(1, "large");

        private Size(int id, string name) : base(id, name)
        {
        }
    }
}