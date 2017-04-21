using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HortaAutomatizada.Controls
{
    public class CustomListView : ListView
    {
        public CustomListView() : base(ListViewCachingStrategy.RecycleElement)
        { }
    }
}
