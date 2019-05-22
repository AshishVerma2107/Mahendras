using System;
using System.Collections.Generic;

using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using ImageSlider.Model;


namespace ImageSlider.Adapter
{
    class Notice_ListView_Adapter : RecyclerView.Adapter
    {
        int lastPosition = -1;
        public event EventHandler<int> ItemClick;
        Activity ac;
        View itemView;
        RecyclerView mRecyclerView;
        List<NoticeData> examalert_list;


        public Notice_ListView_Adapter(Activity ac, List<NoticeData> examalert_list, RecyclerView mRecyclerView)
        {
            this.ac = ac;
            this.mRecyclerView = mRecyclerView;
            this.examalert_list = examalert_list;

        }

        public override int ItemCount
        {
            get { return examalert_list.Count; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            NoticeBoardtViewHolder photoViewHolder1 = holder as NoticeBoardtViewHolder;
            photoViewHolder1.Caption.Text = examalert_list[position].content_title;
            photoViewHolder1.TextDate.Text = examalert_list[position].content_date;
            Animation animation1 = AnimationUtils.LoadAnimation(ac, (position > lastPosition) ? Resource.Animation.slide_up1 : Resource.Animation.slide_down1);
            photoViewHolder1.ItemView.StartAnimation(animation1);
            lastPosition = position;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Notice_ListView_Adapter_Layout, parent, false);
            NoticeBoardtViewHolder vh = new NoticeBoardtViewHolder(itemView, OnClick, mRecyclerView);
            return vh;
        }

        private void OnClick(int obj)
        {
            ItemClick?.Invoke(this, obj);
        }
    }

    public class NoticeBoardtViewHolder : RecyclerView.ViewHolder
    {

        public TextView Caption { get; set; }
        public TextView TextDate { get; set; }

        public NoticeBoardtViewHolder(View itemview, Action<int> listener, RecyclerView mRecyclerView) : base(itemview)
        {
            Caption = itemview.FindViewById<TextView>(Resource.Id.textView_notice_title);
            TextDate = itemview.FindViewById<TextView>(Resource.Id.textView_notice_text);
            itemview.Click += (sender, e) => listener(mRecyclerView.GetChildAdapterPosition((View)sender));
        }
    }
}


//public override NoticeData this[int position]
//        {
//            get
//            {
//                return notice_data[position];
//            }
//        }
//        public override int Count
//        {
//            get
//            {
//                return notice_data.Count;
//            }
//        }

//        public override Java.Lang.Object GetItem(int position)
//        {
//            return null;
//        }

//        public override long GetItemId(int position)
//        {
//            return position;
//        }

//        public override View GetView(int position, View convertView, ViewGroup parent)
//        {
//            var view = convertView;
//            ViewHolderForNotice holder;

//            var local = new LocalOnClickListener();

//            LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
//            if (view == null)
//            {
//                holder = new ViewHolderForNotice();

//                view = inflater.Inflate(Resource.Layout.Notice_ListView_Adapter_Layout, null);


//                holder.notice_title = (TextView)view.FindViewById(Resource.Id.textView_notice_title);

//                //holder.noticeimage = view.FindViewById<ImageView>(Resource.Id.imageView_notice_image);

//                holder.notice_text = view.FindViewById<TextView>(Resource.Id.textView_notice_text);

//                holder.notice_title.Text = notice_data[position].content_title;
//                holder.notice_text.Text = notice_data[position].content_date;

//             //   var imageBitmap = GetImageBitmapFromUrl(video_data[position].thumb);
//              //  holder.videosimage.SetImageBitmap(imageBitmap);



//                view.Tag = holder;



//                //holder.videosimage.Click += delegate
//                //{
//                //    Intent taskIntentActivity5 = new Intent(context, typeof(YouTube_Activity));

//                //    context.StartActivity(taskIntentActivity5);
//                //};

//            }
//            else
//            {
//                holder = (ViewHolderForNotice)view.Tag;
//            }
//            return view;
//        }


//        private Bitmap GetImageBitmapFromUrl(string url)
//        {
//            Bitmap imageBitmap = null;

//            using (var webClient = new WebClient())
//            {
//                var imageBytes = webClient.DownloadData(url);
//                if (imageBytes != null && imageBytes.Length > 0)
//                {
//                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
//                }
//            }

//            return imageBitmap;
//        }


//    }

//    public class ViewHolderForNotice : Java.Lang.Object
//    {
//        internal TextView notice_title;

//        public ImageView noticeimage { get; set; }
//        public TextView notice_text { get; set; }
//        // public TextView Department { get; set; }
//    }
