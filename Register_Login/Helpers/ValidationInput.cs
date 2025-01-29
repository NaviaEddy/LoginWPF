using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Register_Login.Helpers
{
    public partial class ValidationInput: Component
    {    
        public ValidationInput()
        {
            InitializeComponent();
        }

        public ValidationInput(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
