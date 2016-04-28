using BLL;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PL.Models
{
    public static class Rep //Класс связывания логики и контроллеров(предворительный список действий)
    {
        private static IBLL bll = new Logic();
        public static bool AddUser(UserM user)
        {
            return bll.AddUser(new Entities.User
            {
                ID = user.ID,
                Name = user.Name,
                Password = user.Password,
                Avatar = user.Avatar,
                MymeType = user.MymeType
            });
        }
        public static bool CheckUser(string Name, string Password)
        {
            return bll.CheckUser(Name, Password);
        }
        public static UserM GetUser(string Name)
        {
            var userE = bll.GetUser(Name);
            var userM = new UserM
            {
                Avatar = userE.Avatar,
                ID = userE.ID,
                MymeType = userE.MymeType,
                Name = userE.Name,
                Password = userE.Password,
            };
            return userM;
        }
        
        public static UserM GetUser(Guid IDUser)
        {
            var userE = bll.GetUser(IDUser);
            return new UserM
            {
                ID = userE.ID,
                Avatar = userE.Avatar,
                MymeType = userE.MymeType,
                Name = userE.Name,
                Password = null,       //Метк для проверки
            };
        }

        public static bool AddPhoto(PhotoM photoM)
        {
            return bll.AddPhoto(new Entities.Photo
            {
                File = photoM.File,
                IDPhoto = photoM.IDPhoto,
                IDUser = photoM.IDUser,
                MymeType = photoM.MymeType,
                Name = photoM.Name,
                Rating = photoM.Rating,
            });
        }

        
        public static PhotoM GetPhoto(Guid IDPhoto)         //чет кажется именно этот метод не пригодится, ну пусть будет пока
        {
            var photoE = bll.GetPhoto(IDPhoto);
            return new PhotoM
            {
                File = photoE.File,
                IDPhoto = photoE.IDPhoto,
                IDUser = photoE.IDUser,
                MymeType = photoE.MymeType,
                Name = photoE.Name,
                Rating = photoE.Rating,
            };
        }

        public static bool Liking(LikeM likeM)
        {
            return bll.Liking(new Entities.Like
            {
                IDPhoto = likeM.IDPhoto,
                IDUser = likeM.IDUser,
            });
        }

        public static bool Subscribe(SubM subM)
        {
            return bll.Subscribe(new Entities.Sub
            {
                IDUserFirst = subM.IDUserFirst,
                IDUserSecond = subM.IDUserSecond,
            });
        }

        public static IEnumerable<UserM> GetAllUser()
        {
            var listUserE = bll.GetAllUser();
            var listUserM = new List<UserM>();
            foreach (var item in listUserE)
            {
                listUserM.Add(new UserM
                {
                    ID = item.ID,
                    Avatar = item.Avatar,
                    MymeType = item.MymeType,
                    Name = item.Name,
                    Password = null,                          //метка для проверки
                });
            }

            return listUserM;
        }
        public static IEnumerable<PhotoM> GetPhotosUser(Guid IDUser)
        {
            var listPhotoE = bll.GetPhotosUser(IDUser);
            var listPhotoM = new List<PhotoM>();
            foreach (var item in listPhotoE)
            {
                listPhotoM.Add(new PhotoM
                {
                    File = item.File,
                    IDPhoto = item.IDPhoto,
                    IDUser = item.IDUser,
                    MymeType = item.MymeType,
                    Name = item.Name,
                    Rating = item.Rating,
                });
            }
            return listPhotoM;
        }
        public static IEnumerable<LikeM> GetAllLikes()         //Возвращаются сразу все лайки всех пользователей, если лайкеров очень много, а также их лайков, то будет очень загруженно, можно возвращать лайки только текущего пользователя, над этим наверное нужно подумать
        {
            var listLikesE = bll.GetAllLikes();
            var listLikesM = new List<LikeM>();
            foreach (var item in listLikesE)
            {
                listLikesM.Add(new LikeM
                {
                    IDPhoto = item.IDPhoto,
                    IDUser = item.IDUser,
                });
            }
            return listLikesM;
        }
        public static IEnumerable<SubM> GetAllSubUser(Guid IDUser)
        {
            var listSubE = bll.GetAllSubUser(IDUser);
            var listSubM = new List<SubM>();
            foreach (var item in listSubE)
            {
                listSubM.Add(new SubM
                {
                    IDUserFirst = item.IDUserFirst,
                    IDUserSecond = item.IDUserSecond,
                });
            }
            return listSubM;
        }
    }
}