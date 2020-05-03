using SocialNW.BLL.DTO;
using System.Collections.Generic;

namespace SocialNW.BLL.Interfaces
{
    public interface ICommentService
    {
        IEnumerable<CommentDTO> GetAll();
        void Create(CommentDTO comment);
        IEnumerable<CommentDTO> GetCommentsToPost(int? id);

        void Dispose();
    }
}
