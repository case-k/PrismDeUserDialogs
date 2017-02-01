using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismDeUserDialogs.ViewModels
{
    public class SecondPageViewModel : BindableBase
    {
        public SecondPageViewModel()
        {
            long ret = 0;
            for (long i = 0; i < 2000000000; i++)
                ret += i;
        }
    }
}
