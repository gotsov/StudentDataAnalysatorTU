using Prism.Events;
using StudentDataAnalysator.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDataAnalysator.Events
{
    public class GetLogsListEvent : PubSubEvent<ObservableCollection<Log>>
    {
    }
}
