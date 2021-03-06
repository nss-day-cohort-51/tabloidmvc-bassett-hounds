using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ICommentRepository
    {
        List<Comment> GetCommentsByPostId(int postId);
        void AddComment(Comment comment);
        void UpdateComment(Comment comment);
        void DeleteComment(int commentId);

        Comment GetCommentById(int id);
    }
}
