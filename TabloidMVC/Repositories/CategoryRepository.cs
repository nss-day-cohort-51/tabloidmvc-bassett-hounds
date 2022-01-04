using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(IConfiguration config) : base(config) { }
        public List<Category> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT id, name
                                        FROM Category
                                        Order by Name ASC ";
                    var reader = cmd.ExecuteReader();

                    var categories = new List<Category>();

                    while (reader.Read())
                    {
                        categories.Add(new Category()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                        });
                    }

                    reader.Close();

                    return categories;
                }
            }
        }

        public Category GetCategoryById(int id)
        {
            using(SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select c.Id as catId, Name
                                        From Category c
                                        Where c.Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            Category category = new Category
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("catId")),
                                Name = reader.GetString(reader.GetOrdinal("Name"))
                            };

                            return category;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
        
        public void Add(Category category)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        Insert Into Category (
                                            Name)
                                        Output Inserted.Id
                                        Values (@Name)";

                    cmd.Parameters.AddWithValue("@Name", category.Name);

                    category.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void DeleteCategory(int categoryId)
        {
            using(SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE
                                        FROM Category
                                        WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", categoryId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
