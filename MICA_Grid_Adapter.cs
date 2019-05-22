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
    class MICA_Grid_Adapter : BaseAdapter<NoticeBoardModel>
    {

        List<NoticeBoardModel> users;

        public MICA_Grid_Adapter(List<NoticeBoardModel> users)
        {
            this.users = users;
        }

        public override NoticeBoardModel this[int position]
        {
            get
            {
                return users[position];
            }
        }

        public override int Count
        {
            get
            {
                return users.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;

            if (view == null)
            {
                view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Mica_GridView_Adapter, parent, false);

                var photo = view.FindViewById<ImageView>(Resource.Id.imageViewGrid_mica);
                var name = view.FindViewById<TextView>(Resource.Id.textViewGrid_mica);
                var department = view.FindViewById<TextView>(Resource.Id.departmentTextView);

                view.Tag = new ViewHolder() { Photo = photo, Name = name, Department = department };
            }

            var holder = (ViewHolder)view.Tag;

            holder.Photo.SetImageResource(users[position].ImageUrl);
            holder.Name.Text = users[position].Name;
            holder.Department.Text = users[position].Department;


            return view;

        }
    }

    public class ViewHolderforMica : Java.Lang.Object
    {
        internal TextView txtview;

        public ImageView Photo { get; set; }
        public TextView Name { get; set; }
        public TextView Department { get; set; }
    }
}