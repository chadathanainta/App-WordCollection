using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Android.Content;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using System.Collections.Generic;

namespace WordCollect
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        protected FloatingActionButton Addword = null;

        protected SqlManage sqlManage = new SqlManage();

        protected EditText search = null;

        public RecyclerView recyclerView = null;

        public ColectionAdapter colectionAdapter = null;

        public List<Collection> collections = new List<Collection>();


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            Intent intent = new Intent(this, typeof(First));
            StartActivity(intent);
            
            SetContentView(Resource.Layout.activity_main);

            SetEvent();
            CreaTetable();
            collections = sqlManage.SelectAllTable();
            CardViewitem(collections);
        }


        public void SetEvent()
        {
            Addword = FindViewById<FloatingActionButton>(Resource.Id.fab);
            Addword.Click += (o, e) =>
            {
                Intent add_activity = new Intent(this, typeof(AddItem));
                this.StartActivity(add_activity);
            };

            search = FindViewById<EditText>(Resource.Id.searchword);
            search.TextChanged += delegate
            {
                collections = sqlManage.SelectAllTable(search.Text);
                CardViewitem(collections);
            };

        }


        public void CreaTetable() => sqlManage.CreateDatabase();

        public void CardViewitem(List<Collection> collections)
        {
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(this));
            colectionAdapter = new ColectionAdapter(this, collections);
            recyclerView.SetAdapter(colectionAdapter);
        }


        protected override void OnResume()
        {
            base.OnResume();
            collections = sqlManage.SelectAllTable();
            CardViewitem(collections);

        }

        protected override void OnPause()
        {
            base.OnPause();
            colectionAdapter = null;
            collections.Clear();
        }
    }
}