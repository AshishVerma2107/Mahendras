using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ImageSlider.Model;

namespace ImageSlider.Adapter
{
    class UserDataNoticeBoard
    {
        public static List<NoticeBoardModel> Users { get; private set; }

        static UserDataNoticeBoard()
        {
            var temp = new List<NoticeBoardModel>();

            AddUser(temp);
           // AddUser(temp);
          //  AddUser(temp);

            Users = temp.OrderBy(i => i.Name).ToList();
        }

        static void AddUser(List<NoticeBoardModel> users)
        {
            users.Add(new NoticeBoardModel()
            {
                Name = "DELIVERY NOTICE",
                Department = "Facebook may soon let you chat with friends on mobile without Messenger",
                ImageUrl = Resource.Drawable.image6,
                Details = ""
            });

            users.Add(new NoticeBoardModel()
            {
                Name = "RETENTION OF TITLE",
                Department = "Facebook may soon let you chat with friends on mobile without Messenger",
                ImageUrl = Resource.Drawable.image7,
                Details = ""

            });
            users.Add(new NoticeBoardModel()
            {
                Name = "WARRANTY OF TITLE",
                Department = "Facebook may soon let you chat with friends on mobile without Messenger",
                ImageUrl = Resource.Drawable.images14,
                Details = ""

            });

            users.Add(new NoticeBoardModel()
            {
                Name = "PASSING OF TITLE",
                Department = "Facebook may soon let you chat with friends on mobile without Messenger",
                ImageUrl = Resource.Drawable.image5,
                Details = ""

            }); users.Add(new NoticeBoardModel()
            {
                Name = "RESTORATION OF TITLE",
                Department = "Facebook may soon let you chat with friends on mobile without Messenger",
                ImageUrl = Resource.Drawable.images16,
                Details = ""

            });
        }
    }
}
