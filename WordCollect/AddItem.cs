using System.Text.RegularExpressions;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using Android.Widget;
using SQLite;

namespace WordCollect
{
    public delegate void MyEvent(string str);

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class AddItem : AppCompatActivity
    {
        protected Button BtnCancel = null;

        protected Button BtnAdd = null;

        protected EditText ikey = null;

        protected EditText idet = null;

        protected SqlManage sqlManage = new SqlManage();

        public event MyEvent MyToast;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_additem);
            SetEvent();
        }

        public void SetEvent()
        {
            BtnCancel = FindViewById<Button>(Resource.Id.cancel);
            BtnCancel.Click += delegate { this.Finish(); };

            BtnAdd = FindViewById<Button>(Resource.Id.addword);
            BtnAdd.Click += (o, e) => { AddWord(); };

            this.MyToast += new MyEvent(showToast);
        }

        public void AddWord()
        {
            ikey = FindViewById<EditText>(Resource.Id.ikey);
            idet = FindViewById<EditText>(Resource.Id.idet);

            ///<summary>
            /// Regular Expressions
            ///https://medium.com/@_trw/regular-expressions-%E0%B8%84%E0%B8%B7%E0%B8%AD%E0%B8%AD%E0%B8%B0%E0%B9%84%E0%B8%A3-2fab4a91ea34
            /// </summary>
            string pattern = @"\w+.*\S|\w";
            var m_ikey = Regex.Match(ikey.Text, pattern);
            var m_idet = Regex.Match(idet.Text, pattern);


            if ((!m_ikey.Success) || (!m_idet.Success)) MyToast("please : check word");
            else
            {
                ikey.Text = m_ikey.Value;
                idet.Text = m_idet.Value;
                Collection collection = new Collection(ikey.Text, idet.Text);
                try
                {
                    sqlManage.InsertWord(collection);
                    MyToast("insert Success");
                    this.Finish();
                }
                catch (SQLiteException ex)
                {
                    MyToast("This word is already there.");
                    Log.Info("SQLiteEx", ex.Message);
                    //MyToast(ex.Message);
                }
            }
        }

        public void showToast(string str)
        {
            Toast.MakeText(Application.Context, str, ToastLength.Short).Show();
        }
    }
}