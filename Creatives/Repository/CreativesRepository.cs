
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Creatives.Models;
using Creatives.Repository;
using Creatives.Service;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using WebMatrix.WebData;



namespace Creatives.Repository
{

    public class CreativesRepository : ICreativesRepository
    {


        private readonly EntityContext _db;

        public CreativesRepository(EntityContext db)
        {

            _db = db;
        }

        public static void AddUser(RegisterModel model)
        {
            string confirmationToken =
                WebSecurity.CreateUserAndAccount(model.Email, model.Password,
                    new { FirstName = model.FirstName, LastName = model.LastName, About = model.About }, true);
            var user = model;


            BodyEmail.BodySend(user, confirmationToken);

        }

        public User GetUserByName(string name)
        {

            return _db.User.Find(WebSecurity.GetUserId(name));
        }

        public User GetUserById(int id)
        {
            return _db.User.Find(id);
        }

        public void ModifiedUser(User user)
        {
            _db.Entry(user).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public Creative GetCreativeById(int id)
        {
            return _db.Creative.Find(id);
        }


        public void AddTag(int creativeId)
        {

            var creative = GetCreativeById(creativeId);
            creative.Tag.Clear();


            string[] split = creative.Tagon.Split(new Char[] { '#', ' ', ',' });
            foreach (var s in split)
            {
                if (s != "")
                {
                    var tag2 = _db.Tag.SingleOrDefault(r => r.Title == s);
                    if (tag2 != null)
                    {
                        creative.Tag.Add(tag2);
                    }
                    else
                    {
                        _db.Tag.Add(new Tag() { Title = s });
                        _db.SaveChanges();

                        var a = creative.Tag.SingleOrDefault(r => r.Title == s);
                        if (a == null)
                        {
                            var tag1 = _db.Tag.Single(r => r.Title == s);
                            creative.Tag.Add(tag1);
                        }


                    }
                }
                _db.SaveChanges();
            }
        }

        public void AddChapter(Chapter chapter)
        {
            _db.Chapter.Add(chapter);
            _db.SaveChanges();
        }

        public void ModifiedCreatives(Creative creative)
        {
            _db.Entry(GetCreativeById(creative.Creativeid)).CurrentValues.SetValues(creative);
            _db.SaveChanges();
        }

        public Chapter GetChapterById(int id)
        {
            return _db.Chapter.Find(id);
        }

        public void ModifiedChapter(Chapter chapter)
        {
            var chapter1 = GetChapterById(chapter.ChapterId);
            _db.Entry(chapter1).CurrentValues.SetValues(chapter);
            _db.SaveChanges();
        }

        public void AddPictures(HttpPostedFileBase fileUpload, string path, int id, string title)
        {

            var creatives = GetCreativeById(id);
            if (fileUpload != null)
            {
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                {
                    Image image = ImageUpload.ResizeFile(fileUpload, 228, 266);
                    image.Save(Path.Combine(path, title));
                    creatives.Picture.Add(new Picture() { Url = Path.Combine("/images/", title), Titel = title });
                    _db.SaveChanges();
                }
            }
        }

        public List<Creative> GetTenLastCreatives()
        {
            return _db.Creative.OrderByDescending(r => r.DateCreate).Take(10).ToList();
        }

        public void ChangeOrderByChapter(int numbInt, int countInt, int id)
        {
            for (int i = countInt - numbInt; i >= 0; i--)
            {
                var creative = GetCreativeById(id);
                var chapter = creative.Chapter.Single(r => r.NumbChapter == countInt);
                chapter.NumbChapter++;
                countInt--;
                _db.SaveChanges();
            }

        }

        public void ChangeNumberChapter(int[] items, int id)
        {
            var creative = GetCreativeById(id);
            foreach (var number in creative.Chapter)
            {
                var b = number.NumbChapter;
                for (var strNumber = 0; strNumber < items.Length; strNumber++)
                {

                    if (items[strNumber] == b)
                    {
                        number.NumbChapter = strNumber++;
                        _db.SaveChanges();
                    }
                }
            }
       }

        public List<Creative> GetAllCreatives()
        {
            return _db.Creative.OrderByDescending(r=>r.DateCreate).ToList();
        }

        public List<Creative> GetAllCreativesWithTag(int id)
        {
            var tag = _db.Tag.Find(id);
            return tag.Creative.ToList();

        }


    }
}