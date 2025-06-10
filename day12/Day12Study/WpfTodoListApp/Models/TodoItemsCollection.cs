using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTdoListApp.Models;

namespace WpfTodoListApp.Models
{
    public class TodoItemsCollection : ObservableCollection<TodoItem>
    {
        // API로 들어오는 값은 List -> ObservableCollection으로 변환
        public void CopyFrom(IEnumerable<TodoItem> todoitems)
        {
            this.Items.Clear(); // ObservableCollection 자체가 Items 속성 보유

            foreach (TodoItem item in todoitems)
            {
                this.Items.Add(item);  
            }

            //데이터가 변경되었다고 알려줌
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

        }
    }
}
