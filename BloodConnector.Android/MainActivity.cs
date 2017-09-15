using Android.App;
using Android.Widget;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using BloodConnector.Shared.Constants;

namespace BloodConnector.Android
{
    [Activity(Label = "BloodConnector", Theme = "@style/Theme.BloodConnector1", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : ActionBarActivity
    {
        private DrawerLayout _drawerLayout;
        private V7Toolbar _toolbar;
        private MyActionBarDrawerToggle _drawerToggle;
        private NavigationView _navigationView;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            _drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            // Init toolbar
            _toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(_toolbar);
            SupportActionBar.Title = Const.APP_TITLE;
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);

            // Attach item selected handler to navigation view
            _navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            _navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;

            //Create ActionBarDrawerToggle button and add it to the toolbar
            _drawerToggle = new MyActionBarDrawerToggle(this,  _drawerLayout, Resource.String.openDrawer, Resource.String.closeDrawer); //Host Activity,DrawerLayout, Opened Message, Closed Message
            _drawerLayout.SetDrawerListener(_drawerToggle);
            _drawerToggle.SyncState();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            _drawerToggle.OnOptionsItemSelected(item);
            return base.OnOptionsItemSelected(item);
        }

        void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            e.MenuItem.SetChecked(true);

            switch (e.MenuItem.ItemId)
            {
                case (Resource.Id.nav_home):
                    Toast.MakeText(this, "Home selected!", ToastLength.Short).Show();
                    break;
                case (Resource.Id.nav_messages):
                    Toast.MakeText(this, "Message selected!", ToastLength.Short).Show();
                    break;
            }

            // Close drawer
            _drawerLayout.CloseDrawers();
        }
    }
}