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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace SatIp
{
    public class SatIpDevice : IDisposable, INotifyPropertyChanged
    {
        private bool _disposed;
        private Icon[] _iconList = new Icon[4];

        /// <summary>
        ///     Default constructor.
        /// </summary>
        /// <param name="url">Device URL.</param>
        internal SatIpDevice(string url)
        {
            if (url == null) throw new ArgumentNullException("url");

            Init(new Uri(url));
            RtspSession = new RtspSession(BaseUrl.Host);
        }

        public List<Tuner> Tuners { get; set; }

        public RtspSession RtspSession { get; set; }

        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        ~SatIpDevice()
        {
            Dispose(false);
        }

        private void ReadCapability(string capability)
        {
            var cap = capability.Split('-');
            switch (cap[0].ToLower())
            {
                case "dvbs":
                case "dvbs2":
                {
                    SupportsDVBS = true;

                    for (var i = 0; i < int.Parse(cap[1]); i++) Tuners.Add(new SatelliteTuner());

                    break;
                }
                case "dvbc":
                case "dvbc2":
                {
                    SupportsDVBC = true;

                    for (var i = 0; i < int.Parse(cap[1]); i++) Tuners.Add(new CableTuner());

                    break;
                }
                case "dvbt":
                case "dvbt2":
                {
                    SupportsDVBT = true;


                    for (var i = 0; i < int.Parse(cap[1]); i++) Tuners.Add(new TerrestrialTuner());

                    break;
                }
            }
        }

        #region method Init

        public string GetImage(int index)
        {
            var icon = (Icon) _iconList.GetValue(index);
            return icon.Url;
        }

        private void Init(Uri locationUri)
        {
            try
            {
                Tuners = new List<Tuner>();
                //Logger.Info("the Description Url is {0}", locationUri);
                BaseUrl = locationUri;
                var document = XDocument.Load(locationUri.AbsoluteUri);
                var xnm = new XmlNamespaceManager(new NameTable());
                XNamespace n1 = "urn:ses-com:satip";
                XNamespace n0 = "urn:schemas-upnp-org:device-1-0";
                xnm.AddNamespace("root", n0.NamespaceName);
                xnm.AddNamespace("satip:", n1.NamespaceName);
                if (document.Root != null)
                {
                    var deviceElement = document.Root.Element(n0 + "device");

                    DeviceDescription = document.Declaration + document.ToString();
                    //Logger.Info("The Description has this Content \r\n{0}",_deviceDescription);
                    if (deviceElement != null)
                    {
                        var devicetypeElement = deviceElement.Element(n0 + "deviceType");
                        if (devicetypeElement != null)
                            DeviceType = devicetypeElement.Value;
                        var friendlynameElement = deviceElement.Element(n0 + "friendlyName");
                        if (friendlynameElement != null)
                            FriendlyName = friendlynameElement.Value;
                        var manufactureElement = deviceElement.Element(n0 + "manufacturer");
                        if (manufactureElement != null)
                            Manufacturer = manufactureElement.Value;
                        var manufactureurlElement = deviceElement.Element(n0 + "manufacturerURL");
                        if (manufactureurlElement != null)
                            ManufacturerUrl = manufactureurlElement.Value;
                        var modeldescriptionElement = deviceElement.Element(n0 + "modelDescription");
                        if (modeldescriptionElement != null)
                            ModelDescription = modeldescriptionElement.Value;
                        var modelnameElement = deviceElement.Element(n0 + "modelName");
                        if (modelnameElement != null)
                            ModelName = modelnameElement.Value;
                        var modelnumberElement = deviceElement.Element(n0 + "modelNumber");
                        if (modelnumberElement != null)
                            ModelNumber = modelnumberElement.Value;
                        var modelurlElement = deviceElement.Element(n0 + "modelURL");
                        if (modelurlElement != null)
                            ModelUrl = modelurlElement.Value;
                        var serialnumberElement = deviceElement.Element(n0 + "serialNumber");
                        if (serialnumberElement != null)
                            SerialNumber = serialnumberElement.Value;
                        var uniquedevicenameElement = deviceElement.Element(n0 + "UDN");
                        if (uniquedevicenameElement != null) UniqueDeviceName = uniquedevicenameElement.Value;
                        var iconList = deviceElement.Element(n0 + "iconList");
                        if (iconList != null)
                        {
                            var icons = from query in iconList.Descendants(n0 + "icon")
                                select new Icon
                                {
                                    // Needed to change mimeType to mimetype. XML is case sensitive 
                                    MimeType = (string) query.Element(n0 + "mimetype"),
                                    Url = (string) query.Element(n0 + "url"),
                                    Height = (int) query.Element(n0 + "height"),
                                    Width = (int) query.Element(n0 + "width"),
                                    Depth = (int) query.Element(n0 + "depth")
                                };

                            _iconList = icons.ToArray();
                        }

                        var presentationUrlElement = deviceElement.Element(n0 + "presentationURL");
                        if (presentationUrlElement != null) PresentationUrl = presentationUrlElement.Value;
                        var capabilitiesElement = deviceElement.Element(n1 + "X_SATIPCAP");
                        if (capabilitiesElement != null)
                        {
                            Capabilities = capabilitiesElement.Value;
                            if (capabilitiesElement.Value.Contains(','))
                            {
                                var capabilities = capabilitiesElement.Value.Split(',');
                                foreach (var capability in capabilities) ReadCapability(capability);
                            }
                            else
                            {
                                ReadCapability(capabilitiesElement.Value);
                            }
                        }

                        var m3uElement = deviceElement.Element(n1 + "X_SATIPM3U");
                        if (m3uElement != null) M3U = m3uElement.Value;
                    }
                }
            }
            catch (Exception exception)
            {
                //Logger.Error("It give a Problem with the Description {0}", exception);
            }
        }

        #endregion

        #region Properties 

        /// <summary>
        ///     Gets device type.
        /// </summary>
        public string DeviceType { get; private set; } = "";

        /// <summary>
        ///     Gets device short name.
        /// </summary>
        public string FriendlyName { get; private set; } = "";

        /// <summary>
        ///     Gets manufacturer's name.
        /// </summary>
        public string Manufacturer { get; private set; } = "";

        /// <summary>
        ///     Gets web site for Manufacturer.
        /// </summary>
        public string ManufacturerUrl { get; private set; } = "";

        /// <summary>
        ///     Gets device long description.
        /// </summary>
        public string ModelDescription { get; private set; } = "";

        /// <summary>
        ///     Gets model name.
        /// </summary>
        public string ModelName { get; private set; } = "";

        /// <summary>
        ///     Gets model number.
        /// </summary>
        public string ModelNumber { get; private set; } = "";

        /// <summary>
        ///     Gets web site for model.
        /// </summary>
        public string ModelUrl { get; private set; } = "";

        /// <summary>
        ///     Gets serial number.
        /// </summary>
        public string SerialNumber { get; private set; } = "";

        /// <summary>
        ///     Gets unique device name.
        /// </summary>
        public string UniqueDeviceName { get; private set; } = "";

        /// <summary>
        ///     Gets device UI url.
        /// </summary>
        public string PresentationUrl { get; private set; } = "";

        /// <summary>
        ///     Gets UPnP device XML description.
        /// </summary>
        public string DeviceDescription { get; private set; }

        public Uri BaseUrl { get; set; }

        public string M3U { get; set; } = "";

        public string Capabilities { get; set; } = "";


        public bool SupportsDVBS { get; set; }

        public bool SupportsDVBC { get; set; }

        public bool SupportsDVBT { get; set; }

        #endregion

        #region Protected Methods

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    if (RtspSession != null)
                    {
                        RtspSession.Dispose();
                        RtspSession = null;
                    }

            _disposed = true;
        }

        #endregion

        #region Public Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void GetTunerStates()
        {
            //_rtspSession.Describe(dev);
        }

        #endregion
    }
}