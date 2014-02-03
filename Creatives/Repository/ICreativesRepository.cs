using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Creatives.Models;

namespace Creatives.Repository
{
    public interface ICreativesRepository
    {
        
        User GetUserByName(string name);
        User GetUserById(int id);
        void ModifiedUser(User user);
        Creative GetCreativeById(int id);
        void AddTag(int creativeId);
        void AddChapter(Chapter chapter);
        void ModifiedCreatives(Creative creative);
        Chapter GetChapterById(int id);
        void ModifiedChapter(Chapter chapter);
        void AddPictures(HttpPostedFileBase fileUpload,string path,int id,string title);
        List<Creative> GetTenLastCreatives();
        void ChangeOrderByChapter(int numbInt,int countInt,int id);
        void ChangeNumberChapter(int[] items,int id);
        List<Creative> GetAllCreatives();
        List<Creative> GetAllCreativesWithTag(int id);

    }
}
