using DAL;
using Entities;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Logic: IBLL
    {
        IDAL dal;
        public Logic()
        {
            dal = new DALDB();
        }
        public bool AddPhoto(Photo photo)
        {
            if (photo.Name == null)
                photo.Name = "NoName";   
            photo.Rating = 0;            
            return dal.AddPhoto(photo);
        }

        /// <summary>
        /// true-пользователь добавлен; false - пользователь уже зареган
        /// </summary>
        /// <param name="user"></param>
        /// <returns>true-пользователь добавлен; false - пользователь уже зареган</returns>
        public bool AddUser(User user)
        {
            if (GetAllUser().FirstOrDefault(x => x.Name == user.Name) == null) 
                return dal.Add(user);                                          
            return false;                                                      
        }

        public bool CheckUser(string Name, string Password)
        {
            return GetAllUser().FirstOrDefault(x=>(x.Name==Name&&x.Password==Password))!=null;
        }

        public IEnumerable<Like> GetAllLikes()      
        {
            return dal.GetAllLikes().ToArray();
        }

        public IEnumerable<Sub> GetAllSubUser(Guid IDUser)
        {
            return dal.GetAllSubUser().ToArray().Where(x=>x.IDUserFirst==IDUser);  
        }

        public IEnumerable<User> GetAllUser()
        {
            return dal.GetAllUser().ToArray();
        }

        public Photo GetPhoto(Guid IDPhoto)
        {
            return dal.GetPhoto(IDPhoto);
        }

        public IEnumerable<Photo> GetPhotosUser(Guid IDUser)
        {
            return dal.GetPhotoesUser(IDUser).ToArray();
        }

        public User GetUser(Guid IDUser)
        {
            return dal.GetUser(IDUser);
        }

        public User GetUser(string Name)
        {
            return GetAllUser().FirstOrDefault(x=>x.Name==Name);
        }

        public bool Liking(Like like)
        {
            if (GetAllLikes().FirstOrDefault(x => x == like) == null)
                return dal.AddLike(like);
            dal.DeleteLike(like);
            return false;                                   //тру - любая удачная попытка добавления или удаления, тут сделал удаление как false            
        }

        public bool Subscribe(Sub sub)
        {
            if (GetAllSubUser(sub.IDUserFirst).FirstOrDefault(x => x.IDUserSecond == sub.IDUserSecond) == null)
                return dal.AddSub(sub);
            dal.DeleteSub(sub);
            return false;                                  
           
        }

    }
}
