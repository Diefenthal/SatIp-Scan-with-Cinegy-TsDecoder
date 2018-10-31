/* Copyright 2018 Kay Diefenthal.

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

using System.Windows.Forms;
using SatIp.Properties;

namespace SatIp
{
    public partial class MainForm : Form
    {
        private SSDPClient ssdp;

        public MainForm()
        {
            InitializeComponent();
            Logger.SetLogFilePath("Sample.log", Settings.Default.LogLevel);
            ssdp = new SSDPClient();
            ssdp.DeviceFound += DeviceFound;
            ssdp.DeviceLost += DeviceLost;
            ssdp.FindByType("urn:ses-com:device:SatIPServer:1");
        }

        private void DeviceFound(object sender, SatIpDeviceFoundArgs args)
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker) delegate { DeviceFound(sender, args); });
                return;
            }

            var servernode = treeView1.Nodes[0].Nodes.Add(args.Device.UniqueDeviceName, args.Device.FriendlyName);
            servernode.ToolTipText = args.Device.DeviceDescription;
            servernode.Tag = args.Device;
            foreach (var tuner in args.Device.Tuners)
                switch (tuner.Type)
                {
                    case TunerType.Cable:
                        var dvbcnode = new TreeNode("DVBC Tuner");
                        dvbcnode.Tag = new CableTuner();
                        servernode.Nodes.Add(dvbcnode);
                        break;
                    case TunerType.Satellite:
                        var dvbsnode = new TreeNode("DVBS Tuner");
                        dvbsnode.Tag = new SatelliteTuner();
                        servernode.Nodes.Add(dvbsnode);
                        break;
                    case TunerType.Terrestrial:
                        var dvbtnode = new TreeNode("DVBT Tuner");
                        dvbtnode.Tag = new TerrestrialTuner();
                        servernode.Nodes.Add(dvbtnode);
                        break;
                }
            if (treeView1.Nodes[0].IsExpanded != true)
                treeView1.Nodes[0].Expand();
        }

        private void DeviceLost(object sender, SatIpDeviceLostArgs args)
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker) delegate { DeviceLost(sender, args); });
                return;
            }

            //Logger.Info("Device with UUID :{0} restarts,and will removed from the Devices Tree", args.Uuid);
            if (treeView1.Nodes[0].Nodes.ContainsKey(args.Uuid))
            {
                var tn = treeView1.Nodes[0].Nodes[args.Uuid];
                treeView1.Nodes[0].Nodes.Remove(tn);
                treeView1.Update();
            }
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            panel1.Controls.Clear();
            if (e.Node.Tag != null && e.Node.Tag is SatIpDevice)
            {
                var device = ssdp.FindByUDN(e.Node.Name);
                if (device != null)
                {
                    var deviceinfo = new SatIpDeviceInformation(device);
                    headerlabel.Caption = e.Node.Text;
                    panel1.Controls.Add(deviceinfo);
                    //TransponderScan frm = new TransponderScan(device);
                    //frm.ShowDialog();
                }
            }
            else if (e.Node.Tag != null && e.Node.Tag is CableTuner)
            {
                headerlabel.Caption = string.Format("{0} - {1}", e.Node.Parent.Name, e.Node.Text);
            }
            else if (e.Node.Tag != null && e.Node.Tag is SatelliteTuner)
            {
                var device = ssdp.FindByUDN(e.Node.Parent.Name);
                var satinfo = new Satellite(device);
                panel1.Controls.Add(satinfo);
                headerlabel.Caption = string.Format("{0} - {1}", device.FriendlyName, e.Node.Text);
            }
            else if (e.Node.Tag != null && e.Node.Tag is TerrestrialTuner)
            {
                headerlabel.Caption = string.Format("{0} - {1}", e.Node.Parent.Name, e.Node.Text);
            }
        }

        private void DevicesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ssdp.Dispose();
            ssdp.DeviceFound -= DeviceFound;
            ssdp.DeviceLost -= DeviceLost;
            ssdp = null;
        }
    }
}