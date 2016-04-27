using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IDAL
    {
        bool Add(User user);
        bool AddPhoto(Photo photo);
        bool AddLike(Like like);
        bool AddSub(Sub sub);  //Тут походу еще одна таблица нужна, для подписок

        bool DeleteLike(Like like);
        bool DeleteSub(Sub sub);

        User GetUser(Guid IDUser);   // Спецзапрос
        Photo GetPhoto(Guid IDPhoto);// Спецзапрос     
        Photo GetAvatar(Guid IDUser);
        IEnumerable<Photo> GetPhotoesUser(Guid IDUser);
        IEnumerable<User> GetAllUser();
        /// <summary>
        /// Возвращает все подписки пользователя
        /// </summary>
        /// <returns></returns>
        IEnumerable<Sub> GetAllSubUser();
        IEnumerable<Like> GetAllLikes();  //возвращает пары гуидов, для последующей проверки на то, первый ли это лайк
    }
}
