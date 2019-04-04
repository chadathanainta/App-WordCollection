using System.Collections.Generic;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;

namespace WordCollect
{
    public class ColectionAdapter : RecyclerView.Adapter
    {
        public Activity activity { get; set; }
        public List<Collection> mycollections { get; set; }


        public ColectionAdapter(Activity activity, List<Collection> mycollections)
        {
            this.activity = activity;
            this.mycollections = mycollections;
        }

        public override int ItemCount => mycollections.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            // Replace the contents of the view with that element
            Collection collection = this.mycollections[position];
            var viewHolder = holder as ItemViewHolder;
            viewHolder.KeyWord.Text = collection.KeyWord;
            viewHolder.Detal.Text = collection.Detal;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.activity_card, parent, false);
            return new ItemViewHolder(view);
        }
    }
}