using ProductAPI.Models;

namespace api_library.Models
{
    public class Member
    {
        public int NIC { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegisteredDate { get; set; }

        internal static void Add(Member member)
        {
            throw new NotImplementedException();
        }
    }
}
