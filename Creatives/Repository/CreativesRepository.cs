﻿
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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
                 WebSecurity.CreateUserAndAccount(model.Email, model.Password, new { FirstName = model.FirstName, LastName = model.LastName, About = model.About }, true);
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

        public void AddCreatives(Creative creative)
        {
            _db.Creative.Add(creative);
            _db.SaveChanges();

        }

        public void AddTag(string tag, int creativeId)
        {
            var creative = _db.Creative.SingleOrDefault(r => r.Creativeid == creativeId);

            string[] split = tag.Split(new Char[] { '#' });
            foreach (var s in split)
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
                    var tag1 = _db.Tag.Single(r => r.Title == s);
                    creative.Tag.Add(tag1);
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
            _db.Entry(creative).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public Chapter GetChapterById(int id)
        {
            return _db.Chapter.Find(id);
        }

        public void ModifiedChapter(Chapter chapter)
        {
            _db.Entry(chapter).State = EntityState.Modified;
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
   
    }
}