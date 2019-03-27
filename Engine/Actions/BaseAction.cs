using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Actions
{
    public abstract class BaseAction
    {
        public event EventHandler<string> OnActionPerformed;

        protected readonly GameItem _itemInUse;
        protected BaseAction(GameItem itemInUse)
        {
            _itemInUse = itemInUse;
        }

        protected void ReportResult(string result)
        {
            OnActionPerformed?.Invoke(this, result);
        }
    }
}
