using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ExelonApp
{
    public class HomepageModel
    {
        public List<notificationHistory> Histories { get; set; }

        public HomepageModel()
        {
            Histories = new notificationHistory().GetHistories();
        }
    }
}
