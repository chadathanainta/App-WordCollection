using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace WordCollect
{
    class ItemViewHolder : RecyclerView.ViewHolder
    {
        public TextView KeyWord { get; set; }
        public TextView Detal { get; set; }

        public ItemViewHolder(View itemView) : base(itemView)
        {
           this.KeyWord =  itemView.FindViewById<TextView>(Resource.Id.textkey);
           this.Detal =  itemView.FindViewById<TextView>(Resource.Id.textdetal);
            SetEvent(itemView);
        }

        public void SetEvent(View itemView)
        {
            itemView.Click += delegate
            {
                var context = itemView.Context;
                var intent = new Intent(context, typeof(EditItem));
                intent.PutExtra("word", KeyWord.Text);
                intent.PutExtra("detel", Detal.Text);
                context.StartActivity(intent);
                //Toast.MakeText(context, text: word.Text, duration: ToastLength.Short).Show();
            };
        }
    }
}