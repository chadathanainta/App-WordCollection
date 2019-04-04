using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace WordCollect
{

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    class EditItem : AppCompatActivity
    {
        EditText ikey = null;
        EditText idet = null;
        Button btncancel = null;
        Button btndelete = null;
        Button editword = null;
        Button okword = null;
        SqlManage sqlManage = new SqlManage();



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_edit);

            ikey = FindViewById<EditText>(Resource.Id.ikey);
            idet = FindViewById<EditText>(Resource.Id.idet);

            
            ikey.Text = Intent.Extras.GetString("word");
            idet.Text = Intent.Extras.GetString("detel");
            SetEvent();
        }

        public void SetEvent()
        {
            btncancel = FindViewById<Button>(Resource.Id.cancel);
            btncancel.Click += (o, e) => { this.Finish(); };

            btndelete = FindViewById<Button>(Resource.Id.deleword);
            btndelete.Click += delegate { DeleteWord(); };

            editword = FindViewById<Button>(Resource.Id.editeword);
            editword.Click += delegate { EditWord(); };

            okword = FindViewById<Button>(Resource.Id.okeword);
            okword.Click += delegate { UpdateWord(); };
        }


        public void UpdateWord()
        {
            ///<summary>
            /// Regular Expressions
            ///https://medium.com/@_trw/regular-expressions-%E0%B8%84%E0%B8%B7%E0%B8%AD%E0%B8%AD%E0%B8%B0%E0%B9%84%E0%B8%A3-2fab4a91ea34
            /// </summary>
            /// 
            string pattern = @"\w+.*\S|\w";
            var m_idet = Regex.Match(idet.Text, pattern);

            if (!m_idet.Success) Toast.MakeText(this, "please : check detal", ToastLength.Short).Show();
            else
            {
                Collection collection = new Collection(ikey.Text, idet.Text);
                if (sqlManage.UpdateWord(collection))
                {
                    Toast.MakeText(this, "update success", ToastLength.Short).Show();
                    this.Finish();
                }
            }
        }


        public void EditWord()
        {
            idet.Enabled = true;
            idet.Focusable = true;
            editword.Visibility = ViewStates.Gone;
            okword.Visibility = ViewStates.Visible;
            btndelete.Visibility = ViewStates.Gone;
        }


        public void DeleteWord()
        {
            sqlManage.RemoveWord(ikey.Text);
            Toast.MakeText(this, "delete success", ToastLength.Short).Show();
            this.Finish();
        }
        
    }
}