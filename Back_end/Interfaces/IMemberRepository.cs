using api_library.Models;
using ProductAPI.Models;

namespace api_library.Interfaces
{
    public interface IMemberRepository
    {
       public  Task<IEnumerable<Member>> GetAllMembersAsync();
       public  Task<Member> GetMemberByIdAsync(int NIC);
       public Task<int> AddMemberAsync(Member member);
       public Task<int> UpdateMemberAsync(Member member);
       public Task<int> DeleteMemberAsync(int NIC);

        internal async Task<Member> UpdateMemberAsync(int nIC)
        {
            throw new NotImplementedException();
        }
    }
}
