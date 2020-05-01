using SocialNW.BLL.DTO;
using System.Collections.Generic;

namespace SocialNW.BLL.Interfaces
{
    public interface IPostService
    {
        IEnumerable<PostDTO> GetAll();
        IEnumerable<PostDTO> GetFriendsPosts(int id);
        PostDTO GetById(int id);
        void Create(PostDTO postDto);
        void Update(int id, PostDTO postDto);
        void Delete(int id);
        IEnumerable<PostDTO> GetPostsByUser(int id);

        void Dispose();
    }
}
