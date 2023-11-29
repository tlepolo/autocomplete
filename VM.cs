using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfApp1
{
    internal class VM : ObservableRecipient
    {
        public Person Person { get; set; } = new Person();



        public ObservableCollection<string> Recps { get; set; }


        public VM()
        {
            Recps = new ObservableCollection<string>()
        {
            "A1",
            "A2",
            "A3",
            "B1",
            "B2",
            "B3",
            "C1",
            "C2",
            "C3",
            "C4",
        };
        }
    }

    class Person : ObservableValidator
    {



        [Required]
        public string Name { get; set; }

        public Person()
        {
        }

    }
}
