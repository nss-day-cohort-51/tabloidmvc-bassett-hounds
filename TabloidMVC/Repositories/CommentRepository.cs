using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;
using Microsoft.Data.SqlClient;

namespace TabloidMVC.Repositories
{
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(IConfiguration config) : base(config) { }
        public List<Comment> GetCommentsByPostId(int postId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                     SELECT Id, PostId,UserProfileId,Subject,Content,CreateDateTime 
                                      FROM Comment
                                       Where PostId = @postId";
                    cmd.Parameters.AddWithValue("@postId", postId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Comment> comments = new List<Comment>();
                        while (reader.Read())
                        {
                            Comment comment = new Comment()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                PostId = reader.GetInt32(reader.GetOrdinal("PostId")),
                                UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                                Content = reader.GetString(reader.GetOrdinal("Content")),
                                Subject = reader.GetString(reader.GetOrdinal("Subject")),
                                CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),

                            };
                            comments.Add(comment);
                        }
                        return comments;
                    }



                }
            }
        }
        public void AddComment(Comment comment)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Comment (
                            PostId,UserProfileId,Subject,Content,CreateDateTime 
                                  )
                        OUTPUT INSERTED.ID
                        VALUES (
                            @PostId, @UserProfileId, @Subject,@Content, @CreateDateTime )";

                    cmd.Parameters.AddWithValue("@PostId", comment.PostId);
                    cmd.Parameters.AddWithValue("@UserProfileId", comment.UserProfileId);
                    cmd.Parameters.AddWithValue("@Subject", comment.Subject);
                    cmd.Parameters.AddWithValue("@Content", comment.Content);
                    cmd.Parameters.AddWithValue("@CreateDateTime", comment.CreateDateTime);

                    comment.Id = (int)cmd.ExecuteScalar();

                }

            }
        }
        public void UpdateComment(Comment comment)
        {
            using(SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Comment
                                        SET
                                            Subject = @subject,
                                            Content = @content                                            
                                        WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@subject", comment.Subject);
                    cmd.Parameters.AddWithValue("@content", comment.Content);
                    cmd.Parameters.AddWithValue("@id", comment.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public Comment GetCommentById(int id)
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                                        Select Comment.Id, Comment.PostId,
                                        Comment.UserProfileId, Comment.Subject,
                                        Comment.Content, Comment.CreateDateTime
                                        From Comment
                                        Where Comment.id = @id
                                        ";

                        cmd.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Comment comment = new Comment
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                                    PostId = reader.GetInt32(reader.GetOrdinal("PostId")),
                                    Subject = reader.GetString(reader.GetOrdinal("Subject")),
                                    Content = reader.GetString(reader.GetOrdinal("Content")),
                                    CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime"))
                                };

                                return comment;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }
        public void DeleteComment(int commentId)
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                                        Delete From Comment
                                        Where Id = @id
                                        ";

                        cmd.Parameters.AddWithValue("@id", commentId);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
    }
}
