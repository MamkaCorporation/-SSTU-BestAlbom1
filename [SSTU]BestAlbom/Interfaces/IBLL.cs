using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IBLL
    {
        bool AddUser(User user); 
        bool Liking(Like like); 
        bool AddPhoto(Photo photo);
        bool Subscribe(Sub sub);


        User GetUser(Guid IDUser);
        User GetUser(string Name);
        bool CheckUser(string Name, string Password); 
        IEnumerable<User> GetAllUser();
        IEnumerable<Photo> GetPhotosUser(Guid IDUser);
        IEnumerable<Like> GetAllLikes();
        IEnumerable<Sub> GetAllSubUser(Guid IDUser);
        Photo GetPhoto(Guid IDPhoto);
    }
}
