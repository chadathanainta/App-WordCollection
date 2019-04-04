using System.Threading.Tasks;
using Android.App;
using Android.OS;

namespace WordCollect
{
    [Activity(Label = "First")]
    public class First : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_first);
            // Discards _ 
            _ = destAsync();
        }
        

        public async Task destAsync()
        {
            await Task.Delay(2000);
            Finish();
        }
    }
}