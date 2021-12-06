using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DotOS
{
    class PageService
    {
        private Stack<Page> _history = new();
        public event Action<Page> OnPageChanged;
        public void Navigate(Page page)
        {
            _history.Push(page);
            OnPageChanged?.Invoke(page);
        }
        public void GoToBack()
        {
            if (_history.Count == 1)
                return;

            _history.Pop();
            OnPageChanged?.Invoke(_history.Peek());
        }
    }
}
