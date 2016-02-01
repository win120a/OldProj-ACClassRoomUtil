/*
   Copyright (C) 2011-2014 AC Inc. (Andy Cheung)

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Digital_Clock_WindowsForm
{
    public partial class ClockWindow : Form
    {
        public ClockWindow()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Int32Converter i32c = new Int32Converter();
            h.Text = i32c.ConvertToString(DateTime.Now.Hour);
            m.Text = i32c.ConvertToString(DateTime.Now.Minute);
        }

        private void ClockWindow_Load(object sender, EventArgs e)
        {
            Int32Converter i32c = new Int32Converter();
            h.Text = i32c.ConvertToString(DateTime.Now.Hour);
            m.Text = i32c.ConvertToString(DateTime.Now.Minute);
            timer1.Start();
        }
    }
}
