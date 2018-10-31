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
    public partial class SatIpDeviceInformation : UserControl
    {
        private readonly SatIpDevice device;

        public SatIpDeviceInformation()
        {
            InitializeComponent();
        }

        public SatIpDeviceInformation(SatIpDevice device)
        {
            InitializeComponent();
            this.device = device;
            tbxDeviceType.Text = device.DeviceType;
            tbxFriendlyName.Text = device.FriendlyName;
            tbxManufacture.Text = device.Manufacturer;
            tbxModelDescription.Text = device.ModelDescription;
            tbxUniqueDeviceName.Text = device.UniqueDeviceName;
            tbxManufactureUrl.Text = device.ManufacturerUrl;
            tbxPresentationUrl.Text = device.PresentationUrl;
            pbxDVBC.Image = Resources.dvb_c;
            pbxDVBC.Visible = device.SupportsDVBC;
            pbxDVBS.Image = Resources.dvb_s;
            pbxDVBS.Visible = device.SupportsDVBS;
            pbxDVBT.Image = Resources.dvb_t;
            pbxDVBT.Visible = device.SupportsDVBT;

            try
            {
                var imageUrl =
                    string.Format(device.FriendlyName == "OctopusNet" ? "http://{0}:{1}/{2}" : "http://{0}:{1}{2}",
                        device.BaseUrl.Host, device.BaseUrl.Port, device.GetImage(1));
                pbxManufactureBrand.LoadAsync(imageUrl);
                pbxManufactureBrand.Visible = true;
            }
            catch
            {
                pbxManufactureBrand.Visible = false;
            }
        }

        public override string ToString()
        {
            return string.Format("name:{0}-type{1}", device.FriendlyName, "Satellitetuner");
        }
    }
}