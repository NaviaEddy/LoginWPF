using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Register_Login.Models
{
    public partial class Database: Component
    {    
        public Database()
        {
            InitializeComponent();
        }

        public Database(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
