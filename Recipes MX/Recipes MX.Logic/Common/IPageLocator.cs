using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes_MX.Logic.Common
{
    public interface IPageLocator
    {
        Type GetPageType(string PageName);
    }
}
