using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ITagRepository
    {
        List<Tag> GetAllTags();

        void AddTag(Tag tag);

        void DeleteTag(int tagId);

        Tag GetTagById(int id);
        void UpdateTag(Tag tag);
    }
}
