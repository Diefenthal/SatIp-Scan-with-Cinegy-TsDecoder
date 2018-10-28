﻿/* Copyright 2017 Cinegy GmbH.

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

using System.Collections.Generic;
using Cinegy.TsDecoder.TransportStream;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Cinegy.TsDecoder.Tables
{
    public class NetworkInformationTable : Table
    {
        public ushort TransportStreamId { get; set; }
        public byte Reserved2 { get; set; }
        public byte VersionNumber { get; set; }
        public bool CurrentNextIndicator { get; set; }
        public byte SectionNumber { get; set; }
        public byte LastSectionNumber { get; set; }

        public byte ReservedFutureUse { get; set; }
        public ushort NetworkDescriptorsLength { get; set; }
        public byte ReservedFutureUse2 { get; set; }
        public ushort TransportStreamLoopLength { get; set; }
        public IEnumerable<NetworkInformationItem> Items { get; set; }
    }

    public class NetworkInformationItem
    {
        public ushort TransportStreamId { get; set; }
        public ushort OriginalNetworkId { get; set; }
        public string OriginalNetworkIdString => GetOriginalNetworkIdName(OriginalNetworkId);
        public byte ReservedFutureUse { get; set; }
        public ushort TransportDescriptorsLength { get; set; }
        public IEnumerable<Descriptor> Descriptors { get; set; }

        public static string GetNetworkIdName(ushort networkId)
        {
            if (networkId >= 0x0000 && networkId <= 0x0000) return "Reserved Reserved";
            if (networkId >= 0x0001 && networkId <= 0x0001)
                return "Astra Satellite Network 19,2°E Société Européenne des Satellites";
            if (networkId >= 0x0002 && networkId <= 0x0002)
                return "Astra Satellite Network 28,2°E Société Européenne des Satellites";
            if (networkId >= 0x0003 && networkId <= 0x0019) return "Astra 1 - 23 Société Européenne des Satellites";
            if (networkId >= 0x001A && networkId <= 0x001A) return "Intelsat IS-907 at 332.5E Intelsat";
            if (networkId >= 0x001B && networkId <= 0x001B) return "TrendTV	Communication Trends Ltd.";
            if (networkId >= 0x001C && networkId <= 0x001C) return "HELLAS SAT	Hellas-Sat S.A.";
            if (networkId >= 0x001D && networkId <= 0x001D) return "NRK	NRK";
            if (networkId >= 0x0020 && networkId <= 0x0020) return "ASTRA Société Européenne des Satellites";
            if (networkId >= 0x0021 && networkId <= 0x0026) return "Hispasat Network 1 - 6 Hispasat S.A .";
            if (networkId >= 0x0027 && networkId <= 0x0029) return "Hispasat 30°W	Hispasat FSS";
            if (networkId >= 0x002A && networkId <= 0x002A) return "Multicanal	Multicanal  ";
            if (networkId >= 0x002B && networkId <= 0x002B) return "Telstra Saturn Satellite TelstraSaturn Limited  ";
            if (networkId >= 0x002C && networkId <= 0x002C)
                return "Orbit Satellite Television and Radio Network	Orbit Communications Company  ";
            if (networkId >= 0x002D && networkId <= 0x002D) return "Alpha TV	Alpha Digital Synthesis S.A.";
            if (networkId >= 0x002E && networkId <= 0x002E) return "Xantic Xantic BU Broadband";
            if (networkId >= 0x002F && networkId <= 0x002F) return "TVNZ Digital	TVNZ  ";
            if (networkId >= 0x0030 && networkId <= 0x0030)
                return "Canal+ Satellite Network	Canal+ SA (for Intelsat 601)";
            if (networkId >= 0x0031 && networkId <= 0x0031) return "Hispasat – VIA DIGITAL	Hispasat S.A.";
            if (networkId >= 0x0032 && networkId <= 0x0034) return "Hispasat Network 7 - 9 Hispasat S.A.";
            if (networkId >= 0x0035 && networkId <= 0x0035) return "TV Africa Telemedia (PTY) Ltd";
            if (networkId >= 0x0036 && networkId <= 0x0036) return "TV Cabo	TV Cabo Portugal  ";
            if (networkId >= 0x0037 && networkId <= 0x0037) return "STENTOR	France Telecom, CNES and DGA";
            if (networkId >= 0x0038 && networkId <= 0x0038) return "OTE Hellenic Telecommunications Organization S.A .";
            if (networkId >= 0x0039 && networkId <= 0x0039) return "PMSI	PMSI (Philippines )";
            if (networkId >= 0x003A && networkId <= 0x003A)
                return "Bharat Business Channel	Bharat Business Channel Limited";
            if (networkId >= 0x003B && networkId <= 0x003B) return "BBC BBC";
            if (networkId >= 0x003C && networkId <= 0x003C) return "ICO mim	ICO Satellite Services G.P.";
            if (networkId >= 0x003D && networkId <= 0x003D)
                return "Eutelsat satellite system at 3°East	Skylogic Italia S.A.";
            if (networkId >= 0x003E && networkId <= 0x003F) return "Eutelsat satellite system at 3°East	Eutelsat S.A.";
            if (networkId >= 0x0040 && networkId <= 0x0040) return "Hrvatski Telekom d.d Hrvatski Telekom d.d";
            if (networkId >= 0x0041 && networkId <= 0x0041) return "To be defined See Wim Mooij	Mindport  ";
            if (networkId >= 0x0042 && networkId <= 0x0042) return "DMG	DTV haber ve Gorsel yayýncilik";
            if (networkId >= 0x0044 && networkId <= 0x0044) return "VisionTV	VisionTV LLC";
            if (networkId >= 0x0045 && networkId <= 0x0045) return "Vision TV	SES-Sirius";
            if (networkId >= 0x0046 && networkId <= 0x0047) return "1 degree W	Telenor";
            if (networkId >= 0x0048 && networkId <= 0x0048) return "STAR DIGITAL	STAR DIGITAL A.S .";
            if (networkId >= 0x0049 && networkId <= 0x0049) return "Sentech Digital Satellite Sentech  ";
            if (networkId >= 0x004A && networkId <= 0x004B) return "Eutelsat satellite system at 4°East	Rambouillet ES";
            if (networkId >= 0x004C && networkId <= 0x004C) return "Eutelsat satellite system at 4°East	Skylogic S.A.";
            if (networkId >= 0x004D && networkId <= 0x004D) return "Eutelsat satellite system at 4°East	Skylogic S.A.";
            if (networkId >= 0x004E && networkId <= 0x004F) return "Eutelsat satellite system at 4°East	Eutelsat S.A.";
            if (networkId >= 0x0050 && networkId <= 0x0050)
                return "HRT – Croatian Radio and Television	HRT – Croatian Radio and Television";
            if (networkId >= 0x0051 && networkId <= 0x0051) return "Havas	Havas ";
            if (networkId >= 0x0052 && networkId <= 0x0052)
                return
                    "MEO, Serviços de Comunicações e Multimédia, S.A MEO, Serviços de Comunicações e Multimédia, S.A";
            if (networkId >= 0x0053 && networkId <= 0x0053)
                return
                    "MEO, Serviços de Comunicações e Multimédia, S.A MEO, Serviços de Comunicações e Multimédia, S.A";
            if (networkId >= 0x0054 && networkId <= 0x0054) return "Teracom Satellite Teracom AB Satellite Services";
            if (networkId >= 0x0055 && networkId <= 0x0055)
                return "Sirius Satellite System European Coverage NSAB (Teracom)";
            if (networkId >= 0x0056 && networkId <= 0x0056)
                return "Viasat Satellite Services AB Viasat Satellite Services AB";
            if (networkId >= 0x0058 && networkId <= 0x0058) return "Thiacom 1 & 2 co-located 78.5°E UBC Thailand ";
            if (networkId >= 0x005C && networkId <= 0x005C) return "TNL PCS	TNL PCS";
            if (networkId >= 0x005E && networkId <= 0x005E) return "Sirius Satellite System Nordic Coverage NSAB ";
            if (networkId >= 0x005F && networkId <= 0x005F) return "Sirius Satellite System FSS	NSAB ";
            if (networkId >= 0x0060 && networkId <= 0x0060) return "Kabel Deutschland Kabel Deutschland";
            if (networkId >= 0x0064 && networkId <= 0x0064) return "T-Kábel	T-Kábel Magyarország Kft.";
            if (networkId >= 0x0069 && networkId <= 0x0069) return "Optus B3 156°E Optus Communications";
            if (networkId >= 0x0070 && networkId <= 0x0070) return "BONUM1; 36 Degrees East	NTV+";
            if (networkId >= 0x0071 && networkId <= 0x0071) return "TV Polsat	Telewizja Polsat  ";
            if (networkId >= 0x0073 && networkId <= 0x0073) return "PanAmSat 4 68.5°E Pan American Satellite System";
            if (networkId >= 0x0074 && networkId <= 0x0074) return "GeoTel LMI	GeoTelecom Satellite Services";
            if (networkId >= 0x0075 && networkId <= 0x0075) return "GeoTel Express	GeoTelecom Satellite Services";
            if (networkId >= 0x0076 && networkId <= 0x0076) return "GeoTel 3 GeoTelecom Satellite Services";
            if (networkId >= 0x007D && networkId <= 0x007D) return "SkylogiC Skylogic Italia";
            if (networkId >= 0x007E && networkId <= 0x007F)
                return "Eutelsat Satellite System at 7°E European Telecommunications Satellite Organization";
            if (networkId >= 0x0085 && networkId <= 0x0085) return "	BetaTechnik";
            if (networkId >= 0x0085 && networkId <= 0x0085)
                return "Sky Deutschland Fernsehen GmbH & Co. KG	Sky Deutschland Fernsehen GmbH & Co. KG";
            if (networkId >= 0x0090 && networkId <= 0x0090) return "National network	TDF";
            if (networkId >= 0x009A && networkId <= 0x009B) return "Eutelsat satellite system at 9°East	Rambouillet ES";
            if (networkId >= 0x009C && networkId <= 0x009D) return "Eutelsat satellite system at 9°East	Skylogic S.A.";
            if (networkId >= 0x009E && networkId <= 0x009F) return "Eutelsat satellite system at 9°East	Eutelsat S.A.";
            if (networkId >= 0x00A0 && networkId <= 0x00A0)
                return "CyberStar	Loral Space and Communications  Ltd. (NDS)";
            if (networkId >= 0x00A1 && networkId <= 0x00A1)
                return "DigiSTAR	STAR Television Productions Ltd (HK) (NDS)";
            if (networkId >= 0x00A2 && networkId <= 0x00A2)
                return
                    "Sky Entertainment Services	NetSat Serviços Ltda (Brazil), Innova S. de R. L. (Mexico) and Multicountry Partnership L. P. (NDS)";
            if (networkId >= 0x00A3 && networkId <= 0x00A3) return "NDS Director Systems	Various (NDS)";
            if (networkId >= 0x00A4 && networkId <= 0x00A4) return "ISkyB STAR Television Productions Ltd (HK) (NDS)";
            if (networkId >= 0x00A5 && networkId <= 0x00A5)
                return "Indovision	PT. Matahari Lintas Cakrawala (MLC) (NDS)";
            if (networkId >= 0x00A6 && networkId <= 0x00A6) return "ART	ART";
            if (networkId >= 0x00A7 && networkId <= 0x00A7) return "Globecast	France Telecom (NDS)";
            if (networkId >= 0x00A8 && networkId <= 0x00A8) return "Foxtel	Foxtel (Australia) (NDS)";
            if (networkId >= 0x00A9 && networkId <= 0x00A9) return "Sky New Zealand Sky Network Television Ltd (NDS)";
            if (networkId >= 0x00AA && networkId <= 0x00AA) return "OTE OTE (Greece) (NDS)";
            if (networkId >= 0x00AB && networkId <= 0x00AB) return "Yes Satellite Services	DBS (Israel) (NDS)";
            if (networkId >= 0x00AC && networkId <= 0x00AC) return "(NDS satellite services)	(NDS to be allocated)";
            if (networkId >= 0x00AD && networkId <= 0x00AD) return "SkyLife Korea Digital Broadcasting (NDS)";
            if (networkId >= 0x00AE && networkId <= 0x00AF) return "(NDS satellite services)	(NDS to be allocated)";
            if (networkId >= 0x00B0 && networkId <= 0x00B0) return "Groupe CANAL+	Groupe CANAL+";
            if (networkId >= 0x00B1 && networkId <= 0x00B3) return "CANAL+ OVERSEAS	CANAL+ OVERSEAS";
            if (networkId >= 0x00B4 && networkId <= 0x00B4) return "Telesat 107.3°W	Telesat Canada";
            if (networkId >= 0x00B5 && networkId <= 0x00B5) return "Telesat 111.1°W	Telesat Canada";
            if (networkId >= 0x00B7 && networkId <= 0x00B7)
                return "REAL Digital EPG Services Limited REAL Digital EPG Services Limited";
            if (networkId >= 0x00B8 && networkId <= 0x00B8)
                return "W1 - Neterra White Label DTH	Neterra Communications Ltd.";
            if (networkId >= 0x00BB && networkId <= 0x00BB) return "Slovak Telekom, a.s.	Slovak Telekom, a.s.";
            if (networkId >= 0x00C0 && networkId <= 0x00CD) return "Canal +	Canal+";
            if (networkId >= 0x00D0 && networkId <= 0x00D0) return "CCTV	China Central Television (NDS)";
            if (networkId >= 0x00D1 && networkId <= 0x00D1) return "Galaxy	Galaxy Satellite Broadcasting (HK) (NDS)";
            if (networkId >= 0x00D0 && networkId <= 0x00DF)
                return "Päijät-Hämeen Puhelin Oyj	Päijät-Hämeen Puhelin Oyj";
            if (networkId >= 0x00D2 && networkId <= 0x00DF) return "(NDS satellite services)	(NDS to be allocated)";
            if (networkId >= 0x00E0 && networkId <= 0x00E0) return "Es’hailSat	Es’hailSat";
            if (networkId >= 0x00EB && networkId <= 0x00EB) return "Eurovision Network	European Broadcasting Union  ";
            if (networkId >= 0x0100 && networkId <= 0x0103) return "ExpressVu 1 - 4 ExpressVu Inc.";
            if (networkId >= 0x0104 && networkId <= 0x0104) return "MagtiSat	Magticom Ltd.";
            if (networkId >= 0x010D && networkId <= 0x010D) return "Skylogic Skylogic Italia";
            if (networkId >= 0x010E && networkId <= 0x010F)
                return "Eutelsat Satellite System at 10°E European Telecommunications Satellite Organization";
            if (networkId >= 0x0110 && networkId <= 0x0110) return "Mediaset	Mediaset ";
            if (networkId >= 0x011F && networkId <= 0x011F)
                return "visAvision Satellite Network	European Telecommunications Satellite Organization";
            if (networkId >= 0x013D && networkId <= 0x013D) return "Skylogic Skylogic Italia";
            if (networkId >= 0x013E && networkId <= 0x013F)
                return "Eutelsat Satellite System at 13°E European Telecommunications Satellite Organization";
            if (networkId >= 0x0167 && networkId <= 0x0167) return "ACTV	Neterra Ltd";
            if (networkId >= 0x016D && networkId <= 0x016D) return "Skylogic Skylogic Italia";
            if (networkId >= 0x016E && networkId <= 0x016F)
                return "Eutelsat Satellite System at 16°E European Telecommunications Satellite Organization";
            if (networkId >= 0x0170 && networkId <= 0x0171)
                return "Audio Visual Global Joint Stock Company	Audio Visual Global Joint Stock Company";
            if (networkId >= 0x022D && networkId <= 0x022D) return "Skylogic Skylogic Italia";
            if (networkId >= 0x022E && networkId <= 0x022F)
                return
                    "Eutelsat Satellite System at 21.5°E EUTELSAT – European Telecommunications Satellite Organization ";
            if (networkId >= 0x026D && networkId <= 0x026D) return "Skylogic Skylogic Italia";
            if (networkId >= 0x026E && networkId <= 0x026F)
                return
                    "Eutelsat Satellite System at 25.5°E EUTELSAT – European Telecommunications Satellite Organization ";
            if (networkId >= 0x029D && networkId <= 0x029D) return "Skylogic Skylogic Italia";
            if (networkId >= 0x029E && networkId <= 0x029F)
                return "Eutelsat Satellite System at 29°E European Telecommunications Satellite Organization";
            if (networkId >= 0x02BE && networkId <= 0x02BE)
                return "ARABSAT	ARABSAT - Arab Satellite Communications Organization";
            if (networkId >= 0x02C0 && networkId <= 0x02C0) return "MTV Networks Europe MTV Networks Europe";
            if (networkId >= 0x033D && networkId <= 0x033D) return "Skylogic at 33°E Skylogic Italia";
            if (networkId >= 0x033E && networkId <= 0x033F) return "Eutelsat Satellite System at 33°E Eutelsat";
            if (networkId >= 0x034E && networkId <= 0x034E) return "IRIB IRIB";
            if (networkId >= 0x036D && networkId <= 0x036D) return "Skylogic Skylogic Italia";
            if (networkId >= 0x036E && networkId <= 0x036F)
                return "Eutelsat Satellite System at 36°E European Telecommunications Satellite Organization";
            if (networkId >= 0x0378 && networkId <= 0x0378) return "Selectv	Selectv";
            if (networkId >= 0x03E8 && networkId <= 0x03E8) return "Telia Telia, Sweden";
            if (networkId >= 0x045D && networkId <= 0x045F) return "Eutelsat satellite system at 15°West	Eutelsat S.A.";
            if (networkId >= 0x047D && networkId <= 0x047D) return "Skylogic Skylogic Italia";
            if (networkId >= 0x047E && networkId <= 0x047F)
                return
                    "Eutelsat Satellite System at 12.5°W	EUTELSAT – European Telecommunications Satellite Organization ";
            if (networkId >= 0x048D && networkId <= 0x048D) return "Skylogic Skylogic Italia";
            if (networkId >= 0x048E && networkId <= 0x048F)
                return "Eutelsat Satellite System at 48°E European Telecommunications Satellite Organization";
            if (networkId >= 0x049D && networkId <= 0x049F) return "Eutelsat satellite system at 11°West	Eutelsat S.A.";
            if (networkId >= 0x0500 && networkId <= 0x0500) return "Vinasat Center	Vinasat Center";
            if (networkId >= 0x050A && networkId <= 0x050A) return "Real Vu	Real Vu";
            if (networkId >= 0x052D && networkId <= 0x052D) return "Skylogic Skylogic Italia";
            if (networkId >= 0x052E && networkId <= 0x052F)
                return
                    "Eutelsat Satellite System at 8°W	EUTELSAT – European Telecommunications Satellite Organization ";
            if (networkId >= 0x0530 && networkId <= 0x0530)
                return
                    "MEO, Serviços de Comunicações e Multimédia, S.A MEO, Serviços de Comunicações e Multimédia, S.A";
            if (networkId >= 0x0532 && networkId <= 0x0532)
                return
                    "MEO, Serviços de Comunicações e Multimédia, S.A MEO, Serviços de Comunicações e Multimédia, S.A";
            if (networkId >= 0x053D && networkId <= 0x053F) return "Eutelsat satellite system at 53°East	Eutelsat S.A.";
            if (networkId >= 0x055D && networkId <= 0x055D) return "Skylogic at 5°W	Skylogic Italia";
            if (networkId >= 0x055E && networkId <= 0x055F) return "Eutelsat Satellite System at 5°W	Eutelsat";
            if (networkId >= 0x0601 && networkId <= 0x0601) return "UPC Satellite UPC  ";
            if (networkId >= 0x0616 && networkId <= 0x0616)
                return "BellSouth Entertainment	BellSouth Entertainment, Atlanta, GA, USA  ";
            if (networkId >= 0x071D && networkId <= 0x071D)
                return "Skylogic Satellite System at 70.5°E Skylogic Italia";
            if (networkId >= 0x071E && networkId <= 0x071F) return "Eutelsat Satellite System at 70.5°E Eutelsat S.A.";
            if (networkId >= 0x077D && networkId <= 0x077D) return "Skylogic Satellite System at 7°W	Skylogic Italia";
            if (networkId >= 0x077E && networkId <= 0x077F) return "Eutelsat Satellite System at 7°W	Eutelsat S.A.";
            if (networkId >= 0x0800 && networkId <= 0x0800) return "Nilesat 101 Nilesat";
            if (networkId >= 0x0810 && networkId <= 0x0810) return "ZAP Moçambique Finstar S.A.";
            if (networkId >= 0x0880 && networkId <= 0x0880)
                return "MEASAT 1, 91.5°E MEASAT Broadcast Network Systems SDN. BHD. (Kuala Lumpur, Malaysia)";
            if (networkId >= 0x0882 && networkId <= 0x0882)
                return "MEASAT 2, 91.5°E MEASAT Broadcast Network Systems SDN. BHD. (Kuala Lumpur, Malaysia)";
            if (networkId >= 0x0883 && networkId <= 0x0883) return "MEASAT 2, 148.0°E Hsin Chi Broadcast Company Ltd .";
            if (networkId >= 0x088F && networkId <= 0x088F)
                return "MEASAT 3 MEASAT Broadcast Network Systems SDN. BHD. (Kuala Lumpur, Malaysia)";
            if (networkId >= 0x0BBC && networkId <= 0x0BBC) return "BBC World ServicE BBC World Service";
            if (networkId >= 0x0E26 && networkId <= 0x0E26) return "IRIB IRIB";
            if (networkId >= 0x0E30 && networkId <= 0x0E30) return "Telstra International	Telstra International";
            if (networkId >= 0x1000 && networkId <= 0x1000) return "Optus B3 156°E Optus Communications";
            if (networkId >= 0x1001 && networkId <= 0x1001) return "DISH Network	Echostar Communications";
            if (networkId >= 0x1002 && networkId <= 0x1002) return "Dish Network 61.5 W	Echostar Communications";
            if (networkId >= 0x1003 && networkId <= 0x1003) return "Dish Network 83 W	Echostar Communications";
            if (networkId >= 0x1004 && networkId <= 0x1004) return "Dish Network 119 W	Echostar Communications";
            if (networkId >= 0x1005 && networkId <= 0x1005) return "Dish Network 121 W	Echostar Communications";
            if (networkId >= 0x1006 && networkId <= 0x1006) return "Dish Network 148 W	Echostar Communications";
            if (networkId >= 0x1007 && networkId <= 0x1007) return "Dish Network 175 W	Echostar Communications";
            if (networkId >= 0x1008 && networkId <= 0x100B) return "Dish Network W - Z	Echostar Communications";
            if (networkId >= 0x1100 && networkId <= 0x110F) return "GE Americom	GE American Communications";
            if (networkId >= 0x1111 && networkId <= 0x1111) return "EASTERN SPACE SYSTEMS	EASTERN SPACE SYSTEMS";
            if (networkId >= 0x1256 && networkId <= 0x1256) return "NICTA NICTA";
            if (networkId >= 0x1700 && networkId <= 0x1700) return "Echostar 2A EchoStar Communications";
            if (networkId >= 0x1701 && networkId <= 0x1701) return "Echostar 2B EchoStar Communications";
            if (networkId >= 0x1702 && networkId <= 0x1702) return "Echostar 2C EchoStar Communications";
            if (networkId >= 0x1703 && networkId <= 0x1703) return "Echostar 2D EchoStar Communications";
            if (networkId >= 0x1704 && networkId <= 0x1704) return "Echostar 2E EchoStar Communications";
            if (networkId >= 0x1705 && networkId <= 0x1705) return "Echostar 2F EchoStar Communications";
            if (networkId >= 0x1706 && networkId <= 0x1706) return "Echostar 2G	EchoStar Communications";
            if (networkId >= 0x1707 && networkId <= 0x1707) return "Echostar 2H	EchoStar Communications";
            if (networkId >= 0x1708 && networkId <= 0x1708) return "Echostar 2I	EchoStar Communications";
            if (networkId >= 0x1709 && networkId <= 0x1709) return "Echostar 2J	EchoStar Communications";
            if (networkId >= 0x170A && networkId <= 0x170A) return "Echostar 2K	EchoStar Communications";
            if (networkId >= 0x170B && networkId <= 0x170B) return "Echostar 2L	EchoStar Communications";
            if (networkId >= 0x170C && networkId <= 0x170C) return "Echostar 2M	EchoStar Communications";
            if (networkId >= 0x170D && networkId <= 0x170D) return "Echostar 2N	EchoStar Communications";
            if (networkId >= 0x170E && networkId <= 0x170E) return "Echostar 2O	EchoStar Communications";
            if (networkId >= 0x170F && networkId <= 0x170F) return "Echostar 2P	EchoStar Communications";
            if (networkId >= 0x1710 && networkId <= 0x1710) return "Echostar 2Q	EchoStar Communications";
            if (networkId >= 0x1711 && networkId <= 0x1711) return "Echostar 2R	EchoStar Communications";
            if (networkId >= 0x1712 && networkId <= 0x1712) return "Echostar 2S	EchoStar Communications";
            if (networkId >= 0x1713 && networkId <= 0x1713) return "Echostar 2T	EchoStar Communications";
            if (networkId >= 0x1714 && networkId <= 0x1714) return "Platforma HD Platforma HD Ltd.";
            if (networkId >= 0x1715 && networkId <= 0x1715) return "Eutelsat W4 at 36 E Tricolor TV";
            if (networkId >= 0x1716 && networkId <= 0x1716) return "Skyway USA Skyway USA, LLC.";
            if (networkId >= 0x1717 && networkId <= 0x1717) return "France Telecom Orange France Telecom Orange";
            if (networkId >= 0x2000 && networkId <= 0x2000)
                return "Thiacom 1 & 2 co-located 78.5°E Shinawatra Satellite";
            if (networkId >= 0x2001 && networkId <= 0x2002)
                return "Osaka Yusen Terrestrial A - B StarGuide Digital Networks";
            if (networkId >= 0x2003 && networkId <= 0x2003) return "KPN	KPN Broadcast Services";
            if (networkId >= 0x2004 && networkId <= 0x2004)
                return "MiTV Networks Broadcast Terrestrial Network - DVB-H	MiTV Networks Sdn Bhd Malaysia";
            if (networkId >= 0x2005 && networkId <= 0x2005) return "PT MAC PT. Mediatama Anugrah Citra";
            if (networkId >= 0x2006 && networkId <= 0x2006) return "Dominanta DVB-H Service Dominanta LLC";
            if (networkId >= 0x2007 && networkId <= 0x2007) return "DVB-H Austria Media Broadcast GmbH";
            if (networkId >= 0x2008 && networkId <= 0x2008) return "Levira Mobile TV	Levira AS";
            if (networkId >= 0x2009 && networkId <= 0x2009) return "Mobision	Alsumaria TV";
            if (networkId >= 0x200A && networkId <= 0x200A) return "Trenmobile PT. Citra Karya Investasi";
            if (networkId >= 0x200B && networkId <= 0x200B) return "VTC Mobile TV	VTC Mobile TV";
            if (networkId >= 0x200C && networkId <= 0x200C) return "RMN Network	Radio Mindanao Network Inc.";
            if (networkId >= 0x200D && networkId <= 0x200D) return "Sudatel Mobile TV	Sudatel";
            if (networkId >= 0x200E && networkId <= 0x200F)
                return "Vovinet Entertainment Private Limited Vovinet Entertainment Private Limited";
            if (networkId >= 0x2010 && networkId <= 0x2016) return "CANAL+ OVERSEAS	CANAL+ OVERSEAS";
            if (networkId >= 0x20FA && networkId <= 0x20FA)
                return "French Digital Terrestrial Television	Conseil Supérier de l'Audiovisuel (CSA)";
            if (networkId >= 0x2A00 && networkId <= 0x2A00) return "Kentavr DVB-H Network	Kentavr";
            if (networkId >= 0x2B00 && networkId <= 0x2B00)
                return "DTT – Sky New Zealand ky Network Television Limited";
            if (networkId >= 0x3000 && networkId <= 0x3000) return "PanAmSat 4 68.5°E Pan American Satellite System";
            if (networkId >= 0x3001 && networkId <= 0x3100)
                return "UK Digital Terrestrial Television	Independant Television Comission";
            if (networkId >= 0x3001 && networkId <= 0x3100)
                return "German Digital Terrestrial Television	IRT on behalf of the German DVB-T broadcasts";
            if (networkId >= 0x3001 && networkId <= 0x3100) return "DTT Italy	Italian Telecommunications Ministry";
            if (networkId >= 0x3001 && networkId <= 0x3100)
                return
                    "DTT - South African Digital Terrestrial Television	South African Broadcasting Corporation Ltd. (SABC), pending formation of \"DZONGA\"";
            if (networkId >= 0x3001 && networkId <= 0x3100)
                return "DTT - Latvian Digital Terrestrial Television	SIA Lattelecom";
            if (networkId >= 0x3001 && networkId <= 0x3100) return "DTT - Serbia and Montenegro (provisional)	";
            if (networkId >= 0x3001 && networkId <= 0x3100) return "DTT - Slovak Republic (provisional)	";
            if (networkId >= 0x3001 && networkId <= 0x3100)
                return "DTT Serbia - JP Emisiona Tehnika i Veze DTT Serbia - JP Emisiona Tehnika i Veze";
            if (networkId >= 0x3001 && networkId <= 0x3100)
                return "Gibraltar Regulatory Authority	Gibraltar Regulatory Authority";
            if (networkId >= 0x3101 && networkId <= 0x3100)
                return
                    "Communications Regulatory Authority of Namibia (CRAN)	Communications Regulatory Authority of Namibia (CRAN)";
            if (networkId >= 0x3001 && networkId <= 0x3100) return "Ghana DTT	Ghana National Communications Authority";
            if (networkId >= 0x3001 && networkId <= 0x3100) return "DTT Seychelles	Seychelles Broadcasting Corporation";
            if (networkId >= 0x3001 && networkId <= 0x3100)
                return "DTT Tanzania Tanzania Communications Regulatory Authority";
            if (networkId >= 0x3001 && networkId <= 0x3100) return "DTT Afghanistan	ARX Communications LLC";
            if (networkId >= 0x3001 && networkId <= 0x3100)
                return "Georgian DTT	Georgian National Communications Commission (GNCC)";
            if (networkId >= 0x3101 && networkId <= 0x3101) return "ABS-CBN	ABS-CBN Broadcasting Corporation";
            if (networkId >= 0x3102 && networkId <= 0x3102)
                return "AMCARA Broadcasting Network	ABS-CBN Broadcasting Corporation";
            if (networkId >= 0x3101 && networkId <= 0x3200)
                return "Spanish Digital Terrestrial Television	CMT (Spanish Regulstor";
            if (networkId >= 0x3101 && networkId <= 0x3200)
                return "Swedish Digital Terrestrial Television	Post och Telestyrelsen";
            if (networkId >= 0x3101 && networkId <= 0x3200)
                return
                    "US Digital Terrestrial Television	BellSouth Entertainment, Atlanta, GA, USA (on behalf of US broadcasters)";
            if (networkId >= 0x3101 && networkId <= 0x3200) return "Netherlands Digital Terrestrial Television	Nozema";
            if (networkId >= 0x3101 && networkId <= 0x3200)
                return "Czech Republic Digital Terrestrial Television	Czech Digital Group";
            if (networkId >= 0x3103 && networkId <= 0x3200)
                return "DTT - Philippines Digital Terrestrial Television	NTA (pending - ABS-CBN in meantime)";
            if (networkId >= 0x3101 && networkId <= 0x3200) return "DTT - Croatia (provisional)	";
            if (networkId >= 0x3101 && networkId <= 0x3200)
                return
                    "Croatian Post and Electronic Communications Agency (HAKOM)	Croatian Post and Electronic Communications Agency (HAKOM)";
            if (networkId >= 0x3101 && networkId <= 0x3200)
                return "DTT Country of Curacao	Bureau Telecommunicatie en Post";
            if (networkId >= 0x3101 && networkId <= 0x3200)
                return
                    "Office of National Broadcasting and Telecommunications Commission	Office of National Broadcasting and Telecommunications Commission";
            if (networkId >= 0x3101 && networkId <= 0x3200) return "HACA HACA";
            if (networkId >= 0x3101 && networkId <= 0x3200) return "DTT Papua New Guinea NICTA";
            if (networkId >= 0x3101 && networkId <= 0x3200) return "DTT Swaziland Ministry of ICT";
            if (networkId >= 0x3101 && networkId <= 0x3200) return "DTT Uganda Uganda Commuications Commission";
            if (networkId >= 0x3201 && networkId <= 0x3210)
                return "Singapore Digital Terrestrial Television	Singapore Broadcasting Authority";
            if (networkId >= 0x3211 && networkId <= 0x3211) return "MediaCorp	MediaCorp Ltd.";
            if (networkId >= 0x3212 && networkId <= 0x3212) return "StarHub Cable Vision	StarHub Cable Vision Ltd.";
            if (networkId >= 0x3201 && networkId <= 0x3300)
                return "Australian Digital terrestrial Television	Australian Broadcasting Authority";
            if (networkId >= 0x3201 && networkId <= 0x3300) return "Irish Digital terrestrial Television	Irish OFCOM";
            if (networkId >= 0x3213 && networkId <= 0x3300)
                return "Singapore Digital Terrestrial Television	Singapore Broadcasting Authority";
            if (networkId >= 0x3201 && networkId <= 0x3300)
                return "Danish Digital Terrestrial Television	National Telecom Agency Denmark";
            if (networkId >= 0x3201 && networkId <= 0x3300)
                return "Estonian Digital Terrestrial Television	Estonian Terrestrial Regulator";
            if (networkId >= 0x3201 && networkId <= 0x3300) return "Swiss Digital Terrestrial Television	BAKOM";
            if (networkId >= 0x3201 && networkId <= 0x3300)
                return "DTT - Slovenian Digital Terrestrial Television	APEK";
            if (networkId >= 0x3201 && networkId <= 0x3300) return "DTT - Andorran Digital Terrestrial Television	STA";
            if (networkId >= 0x3201 && networkId <= 0x3300) return "DTT - Romania (provisional)	";
            if (networkId >= 0x3201 && networkId <= 0x3300) return "DTT MCTIC BENIN	MCTIC BENIN";
            if (networkId >= 0x3201 && networkId <= 0x3300) return "DTT Brunei	Radio Televisyen Brunei";
            if (networkId >= 0x3201 && networkId <= 0x3300) return "DTT Senegal	EXCAF TELECOM";
            if (networkId >= 0x3301 && networkId <= 0x3400)
                return "Israeli Digital Terrestrial Television	BEZEQ (The Israel Telecommunication Corp Ltd.)";
            if (networkId >= 0x3301 && networkId <= 0x3400)
                return "Finnish Digital Terrestrial Television	Telecommunicatoins Administratoin Centre, Finland";
            if (networkId >= 0x3301 && networkId <= 0x3400)
                return "French Digital Terrestrial Television	Conseil Superieur de l'AudioVisuel";
            if (networkId >= 0x3301 && networkId <= 0x3400)
                return "Taiwanese Digital Terrestrial Television	Directorate General of Telecommunications";
            if (networkId >= 0x3301 && networkId <= 0x3400) return "Austrian Digital Terrestrial Television	ORS";
            if (networkId >= 0x3301 && networkId <= 0x3400) return "DTT - Ukraine (provisional)	";
            if (networkId >= 0x3301 && networkId <= 0x3400)
                return "DTT Panama Autoridad Nacional de los Servicios Públicos";
            if (networkId >= 0x3301 && networkId <= 0x3400) return "DTT Lithuania Communications Regulatory Authority";
            if (networkId >= 0x3301 && networkId <= 0x3400) return "MYTV	MYTV";
            if (networkId >= 0x3301 && networkId <= 0x3400) return "Iceland DTT	Vodafone Iceland";
            if (networkId >= 0x3301 && networkId <= 0x3400) return "Myanmar DTT	Myanma Radio and Television";
            if (networkId >= 0x3301 && networkId <= 0x3400)
                return "Emirates Digital Terrestrial Television	Telecommunications Regulatory Authority (TRA) UAE";
            if (networkId >= 0x3301 && networkId <= 0x3400)
                return "DTT Bosnia and Herzegovina Communications Regulatory Agency";
            if (networkId >= 0x3301 && networkId <= 0x3400)
                return "DTT NIGERIA NIGERIA NATIONAL BROADCASTING COMMISSION";
            if (networkId >= 0x3401 && networkId <= 0x3500) return "Belgian Digital Terrestrial Television	";
            if (networkId >= 0x3401 && networkId <= 0x3500) return "Norwegian Digital Terrestrial Television	";
            if (networkId >= 0x3401 && networkId <= 0x3500)
                return "DTT - New Zealand Digital Terrestrial Television	TVNZ on behalf of Freeview (New Zealand)";
            if (networkId >= 0x3401 && networkId <= 0x3500)
                return "DTT- Portugal	ANACOM- National Communications Authority";
            if (networkId >= 0x3401 && networkId <= 0x3500)
                return "DTT- Hungarian Digital Terrestrial Television	National Communications Authority, Hungary";
            if (networkId >= 0x3401 && networkId <= 0x3500) return "DTT- Poland Office of Electronic Communications";
            if (networkId >= 0x3501 && networkId <= 0x3500) return "DTT - Russian Federation (provisional)	";
            if (networkId >= 0x3401 && networkId <= 0x3500) return "DTT Greece EETT";
            if (networkId >= 0x3501 && networkId <= 0x3600) return "DTT - Russian Federation	RTRN";
            if (networkId >= 0xA510 && networkId <= 0x589) return "Telefonica Cable Telefonica Cable SA";
            if (networkId >= 0xA001 && networkId <= 0xA001) return "RRDRRD Reti Radiotelevisive Digitali Spa";
            if (networkId >= 0xA001 && networkId <= 0xA001) return "H3G	3lettronica Industriale S.p.A";
            if (networkId >= 0xA010 && networkId <= 0xA010) return "Foxtel Cable Foxtel (Australia)";
            if (networkId >= 0xA011 && networkId <= 0xA011) return "Sichuan Cable TV Network	Sichuan Cable TV Network";
            if (networkId >= 0xA012 && networkId <= 0xA012) return "CNS	STAR Koos Finance Company (Taiwan)";
            if (networkId >= 0xA013 && networkId <= 0xA013) return "Versatel	Versatel";
            if (networkId >= 0xA014 && networkId <= 0xA014) return "New Vision Wave SKFC (Taiwan)";
            if (networkId >= 0xA015 && networkId <= 0xA015) return "Prosperity	SKFC (Taiwan)";
            if (networkId >= 0xA016 && networkId <= 0xA016) return "Shin Ho Ho (SHH)	SKFC (Taiwan)";
            if (networkId >= 0xA017 && networkId <= 0xA017) return "Gaho	SKFC (Taiwan)";
            if (networkId >= 0xA018 && networkId <= 0xA018) return "Wonderful	SKFC (Taiwan)";
            if (networkId >= 0xA019 && networkId <= 0xA019) return "Everlasting	SKFC (Taiwan)";
            if (networkId >= 0xA01A && networkId <= 0xA01A) return "Telefirst	SKFC (Taiwan)";
            if (networkId >= 0xA01B && networkId <= 0xA01B) return "Suncrown	SKFC (Taiwan)";
            if (networkId >= 0xA01C && networkId <= 0xA01C) return "Twin Star	SKFC (Taiwan)";
            if (networkId >= 0xA01D && networkId <= 0xA01D) return "Shing Lian	SKFC (Taiwan)";
            if (networkId >= 0xA01E && networkId <= 0xA01E) return "Clearvision	SKFC (Taiwan)";
            if (networkId >= 0xA01F && networkId <= 0xA01F) return "DAWS	SKFC (Taiwan)";
            if (networkId >= 0xA020 && networkId <= 0xA020) return "Chongqing	Chongqing (PRC)";
            if (networkId >= 0xA021 && networkId <= 0xA021) return "Guizhou	Guizhou (PRC)";
            if (networkId >= 0xA022 && networkId <= 0xA022) return "Hathway	Hathway (India)";
            if (networkId >= 0xA023 && networkId <= 0xA02C) return "RCN  1 - 10Rogers Cable (USA)";
            if (networkId >= 0xA02D && networkId <= 0xA040) return "(NDS services)	(NDS to be allocated)";
            if (networkId >= 0XA040 && networkId <= 0xA040) return "COMCOR-TV	COMCOR-TV";
            if (networkId >= 0xA041 && networkId <= 0xA043) return "Euskaltel TV On Line Euskaltel";
            if (networkId >= 0xA044 && networkId <= 0xA044) return "Primacom	Primacom A.G";
            if (networkId >= 0xA045 && networkId <= 0xA045)
                return "Hong Kong CABLE TV	Hong Kong Cable Television Limited";
            if (networkId >= 0xA046 && networkId <= 0xA046) return "Regional Cable Network	Wilhelm.Tel";
            if (networkId >= 0xA050 && networkId <= 0xA070) return "Cable & Wireless Optus	Cable & Wireless Optus";
            if (networkId >= 0xA050 && networkId <= 0xA070)
                return "Cable & Wireless Communications	Cable & Wireless Communications";
            if (networkId >= 0xA070 && networkId <= 0xA070) return "ewt Network	ewt gmbh";
            if (networkId >= 0xA071 && networkId <= 0xA071) return "Värnamo Energi ABVärnamo Energi AB";
            if (networkId >= 0xA080 && networkId <= 0xA080)
                return "mr. net services GmbH & Co. KG	mr. net services GmbH & Co. KG";
            if (networkId >= 0xA08C && networkId <= 0xA09B) return "Telenor Cable TV	Telenor Broadcast Holding AS";
            if (networkId >= 0xA12B && networkId <= 0xA12B) return "Telstra Saturn Cable TelstraSaturn Limited";
            if (networkId >= 0xA001 && networkId <= 0xA400) return "Tele Denmark	Tele Denmark";
            if (networkId >= 0xA401 && networkId <= 0xA401) return "ARDARD-Sternpunkte";
            if (networkId >= 0xA509 && networkId <= 0xA509) return "Welho Cable Network Helsinki	Welho";
            if (networkId >= 0xA510 && networkId <= 0xA510) return "NOBDutch Broadcast Facilities (NOB)";
            if (networkId >= 0xA511 && networkId <= 0xA511)
                return "Martens Multimedia - Cable Networks	Martens Antennen- und Kabelanlagen Gesellschaft mbH";
            if (networkId >= 0xA512 && networkId <= 0xA512) return "LIVAS Telecommunications Group	LIVAS Telecom Cable";
            if (networkId >= 0xA513 && networkId <= 0xA513) return "Hathway	Hathway Cable & Datacom Pvt Ltd";
            if (networkId >= 0xA600 && networkId <= 0xA600) return "Telstra HFC National Network	Telstra";
            if (networkId >= 0xA600 && networkId <= 0xA600) return "Madritel	Madritel ( Spain ) (NDS)";
            if (networkId >= 0xA602 && networkId <= 0xA602) return "Tevel	Tevel Cabe ( Israel ) (NDS)";
            if (networkId >= 0xA603 && networkId <= 0xA603) return "Globo Cabo	Globo Cabo ( Brazil ) (NDS)";
            if (networkId >= 0xA604 && networkId <= 0xA604) return "Cablemas	Cablemas ( Mexico ) (NDS)";
            if (networkId >= 0xA605 && networkId <= 0xA605)
                return "Information Network Centre (INC)	Information Network Centre ( China ) - SARFT (NDS)";
            if (networkId >= 0xA601 && networkId <= 0xA615) return "Rhône Vision Cable Rhône Vision Cable";
            if (networkId >= 0xA61F && networkId <= 0xA61F)
                return "BellSouth Entertainment	BellSouth Entertainment, Atlanta, GA, USA";
            if (networkId >= 0xA600 && networkId <= 0xA640) return "Cable Services de France Cable Service de France";
            if (networkId >= 0xA641 && networkId <= 0xA660) return "Dexys	Dexys";
            if (networkId >= 0xA661 && networkId <= 0xA663) return "Est Video Communication	Video Communication";
            if (networkId >= 0xA664 && networkId <= 0xA666)
                return "Est Video Communication Haut-Rhin	Video Communication Haut-Rhin";
            if (networkId >= 0xA670 && networkId <= 0xA68F) return "SUDCABLE Services	SUDCABLE Services";
            if (networkId >= 0xA697 && networkId <= 0xA69F) return "OMNE Communications	OMNE Communications Ltd.";
            if (networkId >= 0xA700 && networkId <= 0xA700) return "Madritel	Madritel Comunicaciones";
            if (networkId >= 0xA701 && networkId <= 0xA701) return "NTL Cable Network	NTL";
            if (networkId >= 0xA720 && networkId <= 0xA720) return "NSSLGlobal	NSSLGlobal";
            if (networkId >= 0xA750 && networkId <= 0xA750)
                return "Telewest Communications Cable Network	Telewest Communications Plc";
            if (networkId >= 0xA751 && networkId <= 0xA75F) return "TVCabo	TV Cabo";
            if (networkId >= 0xA800 && networkId <= 0xA8FF) return "UPC Cable UPC";
            if (networkId >= 0xA900 && networkId <= 0xA900)
                return "M-net Telekommunikations GmbH	M-net Telekommunikations GmbH";
            if (networkId >= 0xA910 && networkId <= 0xA910) return "TRICOM	TRICOM";
            if (networkId >= 0xA911 && networkId <= 0xA915) return "TRICOM	TRICOM";
            if (networkId >= 0xF001 && networkId <= 0xF01F) return "Kabel Deutschland Kabel Deutschland ";
            if (networkId >= 0xF020 && networkId <= 0xF020) return "Deutsche Telekom AG	Deutsche Telekom AG";
            if (networkId >= 0xF100 && networkId <= 0xF100) return "Casema Casema";
            if (networkId >= 0xF101 && networkId <= 0xF101) return "Tele Columbus AG	Tele Columbus AG";
            if (networkId >= 0xF11F && networkId <= 0xF11F)
                return "visAvision Cable Network	European Telecommunications Satellite Organization";
            if (networkId >= 0xFBFC && networkId <= 0xFBFC) return "MATAV	MATAV Israel (NDS)";
            if (networkId >= 0xFBFD && networkId <= 0xFBFD) return "Telia Kabel-TV	Telia, Sweden";
            if (networkId >= 0xFBFE && networkId <= 0xFBFE) return "TPS	la Télévision Par Satellite";
            if (networkId >= 0xFBFF && networkId <= 0xFBFF) return "Sky Italia Sky Italia SA.";
            if (networkId >= 0xFC00 && networkId <= 0xFCFF) return "France Telecom Cable France Telecom";
            if (networkId >= 0xFD00 && networkId <= 0xFDFF) return "National Cable Network	Lyonnaise Communications";
            if (networkId >= 0xFF01 && networkId <= 0xFFFF) return "Private_temporary_use User_defined";

            return "Undefined";
        }

        public static string GetOriginalNetworkIdName(ushort networkId)
        {
            if (networkId <= 0x0000 && networkId <= 0x0000) return "(Reserved)	(Reserved)";
            if (networkId <= 0x0001 && networkId <= 0x0001)
                return "Société Européenne des Satellites	Société Européenne des Satellites";
            if (networkId <= 0x0002 && networkId <= 0x0002)
                return "Société Européenne des Satellites	Société Européenne des Satellites";
            if (networkId <= 0x0003 && networkId <= 0x0019)
                return "Société Européenne des Satellites	Société Européenne des Satellites";
            if (networkId <= 0x001A && networkId <= 0x001A) return "Quiero Televisión  	Quiero Televisión  ";
            if (networkId <= 0x001B && networkId <= 0x001B) return "RAI	RAI";
            if (networkId <= 0x001C && networkId <= 0x001C) return "Hellas-Sat S.A.	Hellas-Sat S.A.";
            if (networkId <= 0x001D && networkId <= 0x001D)
                return "TELECOM ITALIA MEDIA BROADCASTING SRL	TELECOM ITALIA MEDIA BROADCASTING SRL";
            if (networkId <= 0x001F && networkId <= 0x001F)
                return "Europe Online Networks S.A  	Europe Online Networks S.A  ";
            if (networkId <= 0x0020 && networkId <= 0x0020)
                return "Société Européenne des Satellites	Société Européenne des Satellites";
            if (networkId <= 0x0021 && networkId <= 0x0021) return "Hispasat S.A .	Hispasat S.A .";
            if (networkId <= 0x0022 && networkId <= 0x0022) return "Hispasat S.A .	Hispasat S.A .";
            if (networkId <= 0x0023 && networkId <= 0x0023) return "Hispasat S.A .	Hispasat S.A .";
            if (networkId <= 0x0024 && networkId <= 0x0024) return "Hispasat S.A .	Hispasat S.A .";
            if (networkId <= 0x0025 && networkId <= 0x0025) return "Hispasat S.A .	Hispasat S.A .";
            if (networkId <= 0x0026 && networkId <= 0x0026) return "Hispasat S.A .	Hispasat S.A .";
            if (networkId <= 0x0027 && networkId <= 0x0027) return "Hispasat FSS	Hispasat FSS";
            if (networkId <= 0x0028 && networkId <= 0x0028) return "Hispasat DBS	Hispasat DBS";
            if (networkId <= 0x0029 && networkId <= 0x0029) return "Hispasat America	Hispasat America";
            if (networkId <= 0x002A && networkId <= 0x002A)
                return "Päijät-Hämeen Puhelin Oyj	Päijät-Hämeen Puhelin Oyj";
            if (networkId <= 0x002B && networkId <= 0x002B) return "Digita Oy	Digita Oy";
            if (networkId <= 0x002E && networkId <= 0x002E) return "Xantic BU Broadband	Xantic BU Broadband";
            if (networkId <= 0x002F && networkId <= 0x002F) return "TVNZ  	TVNZ  ";
            if (networkId <= 0x0030 && networkId <= 0x0030)
                return "Canal+ SA (for Intelsat 601-325°E)	Canal+ SA (for Intelsat 601-325°E)";
            if (networkId <= 0x0031 && networkId <= 0x0031) return "Hispasat S.A.	Hispasat S.A.";
            if (networkId <= 0x0032 && networkId <= 0x0032) return "Hispasat S.A.	Hispasat S.A.";
            if (networkId <= 0x0033 && networkId <= 0x0033) return "Hispasat S.A.	Hispasat S.A.";
            if (networkId <= 0x0034 && networkId <= 0x0034) return "Hispasat S.A.	Hispasat S.A.";
            if (networkId <= 0x0035 && networkId <= 0x0035) return "NetHold IMS	NetHold IMS";
            if (networkId <= 0x0036 && networkId <= 0x0036) return "TV Cabo Portugal  	TV Cabo Portugal  ";
            if (networkId <= 0x0037 && networkId <= 0x0037)
                return "France Telecom, CNES and DGA	France Telecom, CNES and DGA";
            if (networkId <= 0x0038 && networkId <= 0x0038)
                return "Hellenic Telecommunications Organization S.A .	Hellenic Telecommunications Organization S.A .";
            if (networkId <= 0x0039 && networkId <= 0x0039) return "Broadcast Australia Pty.	Broadcast Australia Pty.";
            if (networkId <= 0x003A && networkId <= 0x003A)
                return "GeoTelecom Satellite Services	GeoTelecom Satellite Services";
            if (networkId <= 0x003B && networkId <= 0x003B) return "BBC	BBC";
            if (networkId <= 0x003C && networkId <= 0x003C) return "KPN Broadcast Services	KPN Broadcast Services";
            if (networkId <= 0x003D && networkId <= 0x003D) return "Skylogic Italia S.A.	Skylogic Italia S.A.";
            if (networkId <= 0x003E && networkId <= 0x003E) return "Eutelsat S.A.	Eutelsat S.A.";
            if (networkId <= 0x003F && networkId <= 0x003F) return "Eutelsat S.A.	Eutelsat S.A.";
            if (networkId <= 0x0040 && networkId <= 0x0040) return "Hrvatski Telekom d.d	Hrvatski Telekom d.d";
            if (networkId <= 0x0041 && networkId <= 0x0041) return "Mindport  	Mindport  ";
            if (networkId <= 0x0042 && networkId <= 0x0042)
                return "DTV haber ve Gorsel yayýncilik	DTV haber ve Gorsel yayýncilik";
            if (networkId <= 0x0043 && networkId <= 0x0043)
                return "arena Sport Rechte und Marketing GmbH	arena Sport Rechte und Marketing GmbH";
            if (networkId <= 0x0044 && networkId <= 0x0044) return "VisionTV LLC	VisionTV LLC";
            if (networkId <= 0x0045 && networkId <= 0x0045) return "SES-Sirius	SES-Sirius";
            if (networkId <= 0x0046 && networkId <= 0x0046) return "Telenor 	Telenor ";
            if (networkId <= 0x0047 && networkId <= 0x0047) return "Telenor	Telenor";
            if (networkId <= 0x0048 && networkId <= 0x0048) return "STAR DIGITAL A.S .	STAR DIGITAL A.S .";
            if (networkId <= 0x0049 && networkId <= 0x0049) return "Sentech  	Sentech  ";
            if (networkId <= 0x004A && networkId <= 0x004B) return "Rambouillet ES	Rambouillet ES";
            if (networkId <= 0x004C && networkId <= 0x004C) return "Skylogic S.A.	Skylogic S.A.";
            if (networkId <= 0x004D && networkId <= 0x004D) return "Skylogic S.A.	Skylogic S.A.";
            if (networkId <= 0x004E && networkId <= 0x004F) return "Eutelsat S.A.	Eutelsat S.A.";
            if (networkId <= 0x0050 && networkId <= 0x0050)
                return "HRT – Croatian Radio and Television	HRT – Croatian Radio and Television";
            if (networkId <= 0x0051 && networkId <= 0x0051) return "Havas 	Havas ";
            if (networkId <= 0x0052 && networkId <= 0x0052)
                return "StarGuide Digital Networks 	StarGuide Digital Networks ";
            if (networkId <= 0x0053 && networkId <= 0x0053)
                return
                    "MEO, Serviços de Comunicações e Multimédia, S.A	MEO, Serviços de Comunicações e Multimédia, S.A";
            if (networkId <= 0x0054 && networkId <= 0x0054)
                return "Teracom AB Satellite Services	Teracom AB Satellite Services";
            if (networkId <= 0x0055 && networkId <= 0x0055) return "NSAB (Teracom)	NSAB (Teracom)";
            if (networkId <= 0x0056 && networkId <= 0x0056)
                return "Viasat Satellite Services AB	Viasat Satellite Services AB";
            if (networkId <= 0x0058 && networkId <= 0x0058) return "UBC Thailand 	UBC Thailand ";
            if (networkId <= 0x0059 && networkId <= 0x0059)
                return "Bharat Business Channel Limited	Bharat Business Channel Limited";
            if (networkId <= 0x005A && networkId <= 0x005A)
                return "ICO Satellite Services G.P.	ICO Satellite Services G.P.";
            if (networkId <= 0x005B && networkId <= 0x005B) return "ZON	ZON";
            if (networkId <= 0x005C && networkId <= 0x005C) return "TNL PCS	TNL PCS";
            if (networkId <= 0x005E && networkId <= 0x005E) return "NSAB 	NSAB ";
            if (networkId <= 0x005F && networkId <= 0x005F) return "NSAB 	NSAB ";
            if (networkId <= 0x0060 && networkId <= 0x0060) return "Kabel Deutschland 	Kabel Deutschland ";
            if (networkId <= 0x0064 && networkId <= 0x0064) return "T-Kábel	T-Kábel Magyarország Kft.";
            if (networkId <= 0x0065 && networkId <= 0x0065) return "France Telecom Orange	France Telecom Orange";
            if (networkId <= 0x0066 && networkId <= 0x0066)
                return "Zweites Deutsches Fernsehen - ZDF (cable contribution)	Zweites Deutsches Fernsehen - ZDF";
            if (networkId <= 0x0068 && networkId <= 0x0068) return "SinemaTV	SinemaTV";
            if (networkId <= 0x0069 && networkId <= 0x0069) return "Optus B3 156°E	Optus Communications";
            if (networkId <= 0x0070 && networkId <= 0x0070) return "BONUM1; 36 Degrees East	NTV+";
            if (networkId <= 0x0073 && networkId <= 0x0073) return "PanAmSat 4 68.5°E	Pan American Satellite System";
            if (networkId <= 0x007D && networkId <= 0x007D) return "Skylogic	Skylogic Italia";
            if (networkId <= 0x007E && networkId <= 0x007F)
                return
                    "Eutelsat Satellite System at 7°E	EUTELSAT – European Telecommunications Satellite Organization ";
            if (networkId <= 0x0085 && networkId <= 0x0085) return "BetaTechnik	BetaTechnik";
            if (networkId <= 0x0085 && networkId <= 0x0085)
                return "Sky Deutschland Fernsehen GmbH & Co. KG	Sky Deutschland Fernsehen GmbH & Co. KG";
            if (networkId <= 0x0088 && networkId <= 0x0088)
                return "Deutscher Televisionsklub Betriebs GmbH	Deutscher Televisionsklub Betriebs GmbH";
            if (networkId <= 0x0090 && networkId <= 0x0090) return "National network	TDF";
            if (networkId <= 0x009A && networkId <= 0x009B) return "Eutelsat satellite system at 9°East	Rambouillet ES";
            if (networkId <= 0x009C && networkId <= 0x009D) return "Eutelsat satellite system at 9°East	Skylogic S.A.";
            if (networkId <= 0x009E && networkId <= 0x009F) return "Eutelsat satellite system at 9°East	Eutelsat S.A.";
            if (networkId <= 0x00A0 && networkId <= 0x00A0) return "National Cable Network	News Datacom";
            if (networkId <= 0x00A1 && networkId <= 0x00A1)
                return "DigiSTAR	STAR Television Productions Ltd (HK) (NDS)";
            if (networkId <= 0x00A2 && networkId <= 0x00A2)
                return
                    "Sky Entertainment Services	NetSat Serviços Ltda (Brazil), Innova S. de R. L. (Mexico) and Multicountry Partnership L. P. (NDS)";
            if (networkId <= 0x00A3 && networkId <= 0x00A3)
                return "NDS Director systems	Various (product only sold by Tandberg TV) (NDS)";
            if (networkId <= 0x00A4 && networkId <= 0x00A4) return "ISkyB	STAR Television Productions Ltd (HK) (NDS)";
            if (networkId <= 0x00A5 && networkId <= 0x00A5)
                return "Indovision	PT. Matahari Lintas Cakrawala (MLC) (NDS)";
            if (networkId <= 0x00A6 && networkId <= 0x00A6) return "ART	ART (NDS)";
            if (networkId <= 0x00A7 && networkId <= 0x00A7) return "Globecast	France Telecom (NDS)";
            if (networkId <= 0x00A8 && networkId <= 0x00A8) return "Foxtel	Foxtel (Australia) (NDS)";
            if (networkId <= 0x00A9 && networkId <= 0x00A9) return "Sky New Zealand	Sky Network Television Ltd (NDS)";
            if (networkId <= 0x00AA && networkId <= 0x00AA) return "OTE	OTE (Greece) (NDS)";
            if (networkId <= 0x00AB && networkId <= 0x00AB) return "Yes Satellite Services	DBS (Israel) (NDS)";
            if (networkId <= 0x00AC && networkId <= 0x00AC) return "(NDS satellite services)	(NDS to be allocated)";
            if (networkId <= 0x00AD && networkId <= 0x00AD) return "SkyLife	Korea Digital Broadcasting";
            if (networkId <= 0x00AE && networkId <= 0x00AF) return "(NDS satellite services)	(NDS to be allocated)";
            if (networkId <= 0x00B0 && networkId <= 0x00B0) return "Groupe CANAL+	Groupe CANAL+";
            if (networkId <= 0x00B1 && networkId <= 0x00B3) return "CANAL+ OVERSEAS	CANAL+ OVERSEAS";
            if (networkId <= 0x00B4 && networkId <= 0x00B4) return "Telesat 107.3°W	Telesat Canada";
            if (networkId <= 0x00B5 && networkId <= 0x00B5) return "Telesat 111.1°W	Telesat Canada";
            if (networkId <= 0x00B6 && networkId <= 0x00B6) return "Telstra Saturn	TelstraSaturn Limited  ";
            if (networkId <= 0x00B7 && networkId <= 0x00B7) return "CANAL+ OVERSEAS	CANAL+ OVERSEAS";
            if (networkId <= 0x00BA && networkId <= 0x00BA) return "Satellite Express – 6 (80°E)	Satellite Express ";
            if (networkId <= 0x00BB && networkId <= 0x00BB) return "Slovak Telekom, a.s.	Slovak Telekom, a.s.";
            if (networkId <= 0x00C0 && networkId <= 0x00CD) return "Canal +	Canal+";
            if (networkId <= 0x00D0 && networkId <= 0x00D0) return "CCTV	China Central Television (NDS)";
            if (networkId <= 0x00D1 && networkId <= 0x00D1)
                return "Galaxy	Galaxy Satellite Broadcasting, Hong Kong (NDS)";
            if (networkId <= 0x00D2 && networkId <= 0x00D2)
                return "POVERKHNOST SPORT-TUR, LLC	POVERKHNOST SPORT-TUR, LLC";
            if (networkId <= 0x00D2 && networkId <= 0x00DF) return "(NDS satellite services)	(NDS to be allocated)";
            if (networkId <= 0x00E0 && networkId <= 0x00E0) return "Es’hailSat	Es’hailSat";
            if (networkId <= 0x00EB && networkId <= 0x00EB) return "Eurovision Network	European Broadcasting Union  ";
            if (networkId <= 0x00F0 && networkId <= 0x00F0) return "NPO	NPO";
            if (networkId <= 0x00FD && networkId <= 0x00FF) return "TricolorTV	CJSC National Satellite Company";
            if (networkId <= 0x0100 && networkId <= 0x0100) return "ExpressVu	ExpressVu Inc.";
            if (networkId <= 0x0101 && networkId <= 0x0101) return "Bulsatcom AD	Bulsatcom AD";
            if (networkId <= 0x0104 && networkId <= 0x0104) return "MagtiSat	Magticom Ltd.";
            if (networkId <= 0x010D && networkId <= 0x010D) return "Skylogic	Skylogic Italia";
            if (networkId <= 0x010E && networkId <= 0x010F)
                return "Eutelsat Satellite System at 10°E	European Telecommunications Satellite Organization";
            if (networkId <= 0x0110 && networkId <= 0x0110) return "Mediaset	Mediaset ";
            if (networkId <= 0x011F && networkId <= 0x011F)
                return "visAvision Network	European Telecommunications Satellite Organization";
            if (networkId <= 0x013D && networkId <= 0x013D) return "Skylogic	Skylogic Italia";
            if (networkId <= 0x013E && networkId <= 0x013F)
                return "Eutelsat Satellite System 13°E	European Telecommunications Satellite Organization";
            if (networkId <= 0x0167 && networkId <= 0x0167) return "ACTV	Neterra Ltd";
            if (networkId <= 0x016D && networkId <= 0x016D) return "Skylogic	Skylogic Italia";
            if (networkId <= 0x016E && networkId <= 0x016F)
                return "Eutelsat Satellite System at 16°E	European Telecommunications Satellite Organization";
            if (networkId <= 0x0170 && networkId <= 0x0170)
                return "Audio Visual Global Joint Stock Company	Audio Visual Global Joint Stock Company";
            if (networkId <= 0x01F4 && networkId <= 0x01F4) return "MediaKabel B.V	";
            if (networkId <= 0x022D && networkId <= 0x022D) return "Skylogic	Skylogic Italia";
            if (networkId <= 0x022E && networkId <= 0x022F)
                return
                    "Eutelsat Satellite System at 21.5°E	EUTELSAT – European Telecommunications Satellite Organization ";
            if (networkId <= 0x026D && networkId <= 0x026D) return "Skylogic	Skylogic Italia";
            if (networkId <= 0x026E && networkId <= 0x026F)
                return
                    "Eutelsat Satellite System at 25.5°E	EUTELSAT – European Telecommunications Satellite Organization ";
            if (networkId <= 0x029D && networkId <= 0x029D) return "Skylogic	Skylogic Italia";
            if (networkId <= 0x029E && networkId <= 0x029F)
                return "Eutelsat Satellite System at 29°E	European Telecommunications Satellite Organization";
            if (networkId <= 0x02BE && networkId <= 0x02BE)
                return "ARABSAT	ARABSAT - Arab Satellite Communications Organization";
            if (networkId <= 0x02C0 && networkId <= 0x02C0) return "MTV Networks Europe	MTV Networks Europe";
            if (networkId <= 0x031E && networkId <= 0x031E) return "Turksat A.S.	Turksat A.S.";
            if (networkId <= 0x033D && networkId <= 0x033D) return "Skylogic at 33°E	Skylogic Italia";
            if (networkId <= 0x033E && networkId <= 0x033F) return "Eutelsat Satellite System at 33°E	Eutelsat";
            if (networkId <= 0x034E && networkId <= 0x034E) return "IRIB	IRIB";
            if (networkId <= 0x036D && networkId <= 0x036D) return "Skylogic	Skylogic Italia";
            if (networkId <= 0x036E && networkId <= 0x036F)
                return "Eutelsat Satellite System at 36°E	European Telecommunications Satellite Organization";
            if (networkId <= 0x03E8 && networkId <= 0x03E8) return "Telia	Telia, Sweden";
            if (networkId <= 0x042E && networkId <= 0x042E) return "TURKSAT A.S	TURKSAT A.S";
            if (networkId <= 0x045D && networkId <= 0x045F) return "Eutelsat satellite system at 15°West	Eutelsat S.A.";
            if (networkId <= 0x047D && networkId <= 0x047D) return "Skylogic	Skylogic Italia";
            if (networkId <= 0x047E && networkId <= 0x047F)
                return
                    "Eutelsat Satellite System at 12.5°W	EUTELSAT – European Telecommunications Satellite Organization ";
            if (networkId <= 0x048D && networkId <= 0x048D) return "Skylogic	Skylogic Italia";
            if (networkId <= 0x048E && networkId <= 0x048F)
                return "Eutelsat Satellite System at 48°E	European Telecommunications Satellite Organization";
            if (networkId <= 0x049D && networkId <= 0x049F) return "Eutelsat satellite system at 11°West	Eutelsat S.A.";
            if (networkId <= 0x0500 && networkId <= 0x0500) return "Vinasat Center	Vinasat Center";
            if (networkId <= 0x050E && networkId <= 0x050E) return "Turksat A.S.	Turksat A.S";
            if (networkId <= 0x0510 && networkId <= 0x0510) return "Almajd	Almajd Satellite Broadcasting FZ LLC";
            if (networkId <= 0x052D && networkId <= 0x052D) return "Skylogic	Skylogic Italia";
            if (networkId <= 0x052E && networkId <= 0x052F)
                return
                    "Eutelsat Satellite System at 8°W	EUTELSAT – European Telecommunications Satellite Organization ";
            if (networkId <= 0x0531 && networkId <= 0x0531)
                return
                    "MEO, Serviços de Comunicações e Multimédia, S.A	MEO, Serviços de Comunicações e Multimédia, S.A";
            if (networkId <= 0x053D && networkId <= 0x053F) return "Eutelsat satellite system at 53°East	Eutelsat S.A.";
            if (networkId <= 0x055D && networkId <= 0x055D) return "Skylogic at 5°W	Skylogic Italia";
            if (networkId <= 0x055E && networkId <= 0x055F) return "Eutelsat Satellite System at 5°W	Eutelsat";
            if (networkId <= 0x0600 && networkId <= 0x0600) return "UPC Satellite	UPC  ";
            if (networkId <= 0x0601 && networkId <= 0x0601) return "UPC Cable	UPC  ";
            if (networkId <= 0x0602 && networkId <= 0x0602) return "Tevel	Tevel Cable (Israel )";
            if (networkId <= 0x071D && networkId <= 0x071D) return "Skylogic at 70.5°E	Skylogic Italia";
            if (networkId <= 0x071E && networkId <= 0x071F) return "Eutelsat Satellite System at 70.5°E	Eutelsat S.A.";
            if (networkId <= 0x077D && networkId <= 0x077D) return "Skylogic Satellite System at 7°W	Skylogic Italia";
            if (networkId <= 0x077E && networkId <= 0x077F) return "Eutelsat Satellite System at 7°W	Eutelsat S.A.";
            if (networkId <= 0x0800 && networkId <= 0x0801) return "Nilesat 101	Nilesat";
            if (networkId <= 0x0880 && networkId <= 0x0880)
                return "MEASAT 1, 91.5°E	MEASAT Broadcast Network Systems SDN. BHD. (Kuala Lumpur, Malaysia)";
            if (networkId <= 0x0882 && networkId <= 0x0882)
                return "MEASAT 2, 91.5°E	MEASAT Broadcast Network Systems SDN. BHD. (Kuala Lumpur, Malaysia)";
            if (networkId <= 0x0883 && networkId <= 0x0883) return "MEASAT 2, 148.0°E	Hsin Chi Broadcast Company Ltd .";
            if (networkId <= 0x088F && networkId <= 0x088F)
                return "MEASAT 3	MEASAT Broadcast Network Systems SDN. BHD. (Kuala Lumpur, Malaysia)";
            if (networkId <= 0x08A0 && networkId <= 0x08A0)
                return "Mainland Television Limited	Mainland Television Limited";
            if (networkId <= 0x0BBC && networkId <= 0x0BBC) return "BBC World Service	BBC World Service";
            if (networkId <= 0x0E26 && networkId <= 0x0E26) return "IRIB	IRIB";
            if (networkId <= 0x0FFF && networkId <= 0x0FFF) return "Optus Networks	Optus Networks";
            if (networkId <= 0x1000 && networkId <= 0x1000) return "Optus B3 156°E	Optus Communications";
            if (networkId <= 0x1001 && networkId <= 0x1001) return "DISH Network	Echostar Communications";
            if (networkId <= 0x1002 && networkId <= 0x1002) return "Dish Network 61.5 W	Echostar Communications";
            if (networkId <= 0x1003 && networkId <= 0x1003) return "Dish Network 83 W	Echostar Communications";
            if (networkId <= 0x1004 && networkId <= 0x1004) return "Dish Network 119 W	Echostar Communications";
            if (networkId <= 0x1005 && networkId <= 0x1005) return "Dish Network 121 W	Echostar Communications";
            if (networkId <= 0x1006 && networkId <= 0x1006) return "Dish Network 148 W	Echostar Communications";
            if (networkId <= 0x1007 && networkId <= 0x1007) return "Dish Network 175 W	Echostar Communications";
            if (networkId <= 0x1008 && networkId <= 0x100B) return "Dish Network W - Z	Echostar Communications";
            if (networkId <= 0x1010 && networkId <= 0x1010) return "ABC TV	Australian Broadcasting Corporation  ";
            if (networkId <= 0x1011 && networkId <= 0x1011) return "SBS	SBS Australia  ";
            if (networkId <= 0x1012 && networkId <= 0x1012) return "Nine Network Australia	Nine Network Australia  ";
            if (networkId <= 0x1013 && networkId <= 0x1013) return "Seven Network Australia	Seven Network Limited  ";
            if (networkId <= 0x1014 && networkId <= 0x1014) return "Network TEN Australia	Network TEN Limited  ";
            if (networkId <= 0x1015 && networkId <= 0x1015) return "WIN Television Australia	WIN Television Pty Ltd  ";
            if (networkId <= 0x1016 && networkId <= 0x1016)
                return "Prime Television Australia	Prime Television Limited  ";
            if (networkId <= 0x1017 && networkId <= 0x1017)
                return "Southern Cross Broadcasting Australia	Southern Cross Broadcasting (Australia) Limited  ";
            if (networkId <= 0x1018 && networkId <= 0x1018)
                return "Telecasters Australia	Telecasters Australia Limited  ";
            if (networkId <= 0x1019 && networkId <= 0x1019) return "NBN TV	NBN Television Limited";
            if (networkId <= 0x101A && networkId <= 0x101A)
                return "Imparja Television Australia	Imparja Television Australia ";
            if (networkId <= 0x101B && networkId <= 0x101F)
                return "(Reserved for Australian broadcaster)	(Reserved for Australian broadcasters)";
            if (networkId <= 0x1100 && networkId <= 0x1100) return "GE Americom	GE American Communications";
            if (networkId <= 0x1101 && networkId <= 0x1101)
                return "MiTV Networks Broadcast Terrestrial Network - DVB-H	MiTV Networks Sdn Bhd Malaysia";
            if (networkId <= 0x1102 && networkId <= 0x1102)
                return "Dream Mobile TV	Philippines Multimedia System, Inc.";
            if (networkId <= 0x1103 && networkId <= 0x1103) return "PT MAC	PT. Mediatama Anugrah Citra";
            if (networkId <= 0x1104 && networkId <= 0x1104) return "Levira Mobile TV	Levira AS";
            if (networkId <= 0x1105 && networkId <= 0x1105) return "Mobision	Alsumaria TV";
            if (networkId <= 0x1106 && networkId <= 0x1106) return "Trenmobile	PT. Citra Karya Investasi";
            if (networkId <= 0x1107 && networkId <= 0x1107) return "VTC Mobile TV	VTC Mobile TV";
            if (networkId <= 0x1111 && networkId <= 0x1111) return "EASTERN SPACE SYSTEMS	EASTERN SPACE SYSTEMS";
            if (networkId <= 0x1700 && networkId <= 0x1700) return "Echostar 2A	EchoStar Communications";
            if (networkId <= 0x1701 && networkId <= 0x1701) return "Echostar 2B	EchoStar Communications";
            if (networkId <= 0x1702 && networkId <= 0x1702) return "Echostar 2C	EchoStar Communications";
            if (networkId <= 0x1703 && networkId <= 0x1703) return "Echostar 2D	EchoStar Communications";
            if (networkId <= 0x1704 && networkId <= 0x1704) return "Echostar 2E	EchoStar Communications";
            if (networkId <= 0x1705 && networkId <= 0x1705) return "Echostar 2F	EchoStar Communications";
            if (networkId <= 0x1706 && networkId <= 0x1706) return "Echostar 2G	EchoStar Communications";
            if (networkId <= 0x1707 && networkId <= 0x1707) return "Echostar 2H	EchoStar Communications";
            if (networkId <= 0x1708 && networkId <= 0x1708) return "Echostar 2I	EchoStar Communications";
            if (networkId <= 0x1709 && networkId <= 0x1709) return "Echostar 2J	EchoStar Communications";
            if (networkId <= 0x170A && networkId <= 0x170A) return "Echostar 2K	EchoStar Communications";
            if (networkId <= 0x170B && networkId <= 0x170B) return "Echostar 2L	EchoStar Communications";
            if (networkId <= 0x170C && networkId <= 0x170C) return "Echostar 2M	EchoStar Communications";
            if (networkId <= 0x170D && networkId <= 0x170D) return "Echostar 2N	EchoStar Communications";
            if (networkId <= 0x170E && networkId <= 0x170E) return "Echostar 2O	EchoStar Communications";
            if (networkId <= 0x170F && networkId <= 0x170F) return "Echostar 2P	EchoStar Communications";
            if (networkId <= 0x1710 && networkId <= 0x1710) return "Echostar 2Q	EchoStar Communications";
            if (networkId <= 0x1711 && networkId <= 0x1711) return "Echostar 2R	EchoStar Communications";
            if (networkId <= 0x1712 && networkId <= 0x1712) return "Echostar 2S	EchoStar Communications";
            if (networkId <= 0x1713 && networkId <= 0x1713) return "Echostar 2T	EchoStar Communications";
            if (networkId <= 0x1714 && networkId <= 0x1714) return "Platforma HD	Platforma HD Ltd.";
            if (networkId <= 0x1715 && networkId <= 0x1715) return "Profit Group Terrestrial	Profit Group SpA";
            if (networkId <= 0x1716 && networkId <= 0x1716) return "JSC Mostelekom	JSC Mostelekom";
            if (networkId <= 0x2000 && networkId <= 0x2000)
                return "Thiacom 1 & 2 co-located 78.5°E	Shinawatra Satellite";
            if (networkId <= 0x2004 && networkId <= 0x2004) return "DTT Afghanistan	ARX Communications LLC";
            if (networkId <= 0x2014 && networkId <= 0x2014) return "DTT - Andorran Digital Terrestrial Television	STA";
            if (networkId <= 0x2024 && networkId <= 0x2024)
                return "Australian Digital Terrestrial Television	Australian Broadcasting Authority ";
            if (networkId <= 0x2028 && networkId <= 0x2028)
                return "Austrian Digital Terrestrial Television	ORS - Austrian Broadcasting Services";
            if (networkId <= 0x2038 && networkId <= 0x2038) return "Belgian Digital Terrestrial Television	BIPT";
            if (networkId <= 0x2046 && networkId <= 0x2046)
                return "DTT Bosnia and Herzegovina	Communications Regulatory Agency";
            if (networkId <= 0x2060 && networkId <= 0x2060) return "DTT Brunei	Radio Televisyen Brunei";
            if (networkId <= 0x2068 && networkId <= 0x2068) return "DTT Myanmar	Myanma Radio and Television";
            if (networkId <= 0x209E && networkId <= 0x209E)
                return "Taiwanese Digital Terrestrial Television	Directorate General of Telecommunications";
            if (networkId <= 0x20AA && networkId <= 0x20AA)
                return "DTT Colombia	Comision de Regulacion de Comunicaciones";
            if (networkId <= 0x20BF && networkId <= 0x20BF)
                return
                    "Croatian Post and Electronic Communications Agency (HAKOM)	Croatian Post and Electronic Communications Agency (HAKOM)";
            if (networkId <= 0x20C4 && networkId <= 0x20C4)
                return
                    "Office Of the Commissioner of Electronic Communications and Postal Regulation	Office Of the Commissioner of Electronic Communications and Postal Regulation";
            if (networkId <= 0x20CB && networkId <= 0x20CB)
                return "Czech Republic Digital Terrestrial Television	Czech Digital Group  ";
            if (networkId <= 0x20CC && networkId <= 0x20CC) return "DTT MCTIC BENIN	MCTIC BENIN";
            if (networkId <= 0x20D0 && networkId <= 0x20D0)
                return "Danish Digital Terrestrial Television	National Telecom Agency Denmark  ";
            if (networkId <= 0x20E9 && networkId <= 0x20E9)
                return "Estonian Digital Terrestrial Television	Estonian National Communications Board";
            if (networkId <= 0x20F6 && networkId <= 0x20F6)
                return "Finnish Digital Terrestrial Television	Telecommunicatoins Administratoin Centre, Finland  ";
            if (networkId <= 0x20FA && networkId <= 0x20FA)
                return "French Digital Terrestrial Television	Conseil Superieur de l'AudioVisuel";
            if (networkId <= 0x210C && networkId <= 0x210C)
                return "Georgien DTT	Georgian National Communications Commission (GNCC)";
            if (networkId <= 0x2114 && networkId <= 0x2114)
                return "German Digital Terrestrial Television	IRT on behalf of the German DVB-T broadcasts ";
            if (networkId <= 0x2120 && networkId <= 0x2120) return "Ghana DTT	Ghana National Communications Authority";
            if (networkId <= 0x2124 && networkId <= 0x2124)
                return "Gibraltar Regulatory Authority	Gibraltar Regulatory Authority";
            if (networkId <= 0x212C && networkId <= 0x212C) return "DTT Greece	EETT";
            if (networkId <= 0x2160 && networkId <= 0x2160) return "Iceland DTT	Vodafone Iceland";
            if (networkId <= 0x2168 && networkId <= 0x2168)
                return
                    "Digital Terrestrial Network of Indonesia	Ministry of Communication and Information Technology of the Republic of Indonesia";
            if (networkId <= 0x2174 && networkId <= 0x2174)
                return "Irish Digital Terrestrial Television	Irish Telecommunications Regulator  ";
            if (networkId <= 0x2178 && networkId <= 0x2178)
                return "Israeli Digital Terrestrial Television	BEZEQ (The Israel Telecommunication Corp Ltd .)";
            if (networkId <= 0x217C && networkId <= 0x217C) return "Italian Digital Terrestrial Television	";
            if (networkId <= 0x2180 && networkId <= 0x2180) return "DTT Cote D\'Ivoire	HACA";
            if (networkId <= 0x21AC && networkId <= 0x21AC)
                return "DTT - Latvian Digital Terrestrial Television	Electronic Communications Office";
            if (networkId <= 0x21B8 && networkId <= 0x21B8) return "DTT Lithuania	Communications Regulatory Authority";
            if (networkId <= 0x21CA && networkId <= 0x21CA) return "MYTV	MYTV";
            if (networkId <= 0x2204 && networkId <= 0x2204)
                return
                    "Communications Regulatory Authority of Namibia (CRAN)	Communications Regulatory Authority of Namibia (CRAN)";
            if (networkId <= 0x2210 && networkId <= 0x2210)
                return "Netherlands Digital Terrestrial Television	Nozema  ";
            if (networkId <= 0x2213 && networkId <= 0x2213)
                return "DTT for Country of Curacao	Bureau Telecommunicatie en Post";
            if (networkId <= 0x222A && networkId <= 0x222A)
                return "DTT - New Zealand Digial Terrestrial Television	TVNZ on behalf of Freeview New Zealand";
            if (networkId <= 0x2236 && networkId <= 0x2236)
                return "DTT NIGERIA	NIGERIA NATIONAL BROADCASTING COMMISSION";
            if (networkId <= 0x2242 && networkId <= 0x2242)
                return "Norwegian Digital Terrestrial Television	Norwegian Regulator";
            if (networkId <= 0x224F && networkId <= 0x224F)
                return "Autoridad Nacional de los Servicios Públicos	Autoridad Nacional de los Servicios Públicos";
            if (networkId <= 0x2256 && networkId <= 0x2256) return "DTT Papua New Guinea	NICTA";
            if (networkId <= 0x2260 && networkId <= 0x2260)
                return "DTT - Philippines Digital Terrestrial Television	NTA (porivionally ABS-CBN)";
            if (networkId <= 0x2268 && networkId <= 0x2268) return "DTT Poland	Office of Electronic Communications";
            if (networkId <= 0x2283 && networkId <= 0x2283) return "DTT - Russian Federation	RTRN";
            if (networkId <= 0x22AE && networkId <= 0x22AE) return "DTT Senegal	EXCAF TELECOM";
            if (networkId <= 0x22B0 && networkId <= 0x22B0)
                return "DTT - Serbia JP Emisiona Tehnika i Veze	JP Emisiona Tehnika i Veze";
            if (networkId <= 0x22B2 && networkId <= 0x22B2) return "DTT Seychelles	Seychelles Broadcasting Corporation";
            if (networkId <= 0x22BE && networkId <= 0x22BE)
                return "Singapore Digital Terrestrial Television	Singapore Broadcasting Authority  ";
            if (networkId <= 0x22BF && networkId <= 0x22BF)
                return
                    "Telecommunications office of the Slovak republic	Telecommunications office of the Slovak republic";
            if (networkId <= 0x22C1 && networkId <= 0x22C1)
                return "DTT - Slovenian Digital Terrestrial Television	APEK";
            if (networkId <= 0x22C6 && networkId <= 0x22C6)
                return
                    "DTT - South African Digital Terrestrial Television	South African Broadcasting Corporation Ltd. (SABC), pending formation of";
            if (networkId <= 0x22C7 && networkId <= 0x22C7)
                return "DTT- Hungarian Digital Terrestrial Television	National Communications Authority, Hungary";
            if (networkId <= 0x22C8 && networkId <= 0x22C8)
                return "DTT- Portugal Digital Terrestrial Television	ANACOM- National Communications Authority";
            if (networkId <= 0x22D4 && networkId <= 0x22D4)
                return "Spanish Digital Terrestrial Television	“Spanish Broadcasting Regulator ";
            if (networkId <= 0x22EC && networkId <= 0x22EC) return "DTT Swaziland	Ministry of ICT";
            if (networkId <= 0x22F1 && networkId <= 0x22F1)
                return "Swedish Digital Terrestrial Television	“Swedish Broadcasting Regulator ”";
            if (networkId <= 0x22F4 && networkId <= 0x22F4) return "Swiss Digital Terrestrial Television	OFCOM";
            if (networkId <= 0x22FC && networkId <= 0x22FC)
                return
                    "Office of National Broadcasting and Telecommunications Commission	Office of National Broadcasting and Telecommunications Commission";
            if (networkId <= 0x2310 && networkId <= 0x2310)
                return "Emirates Digital Terrestrial Television	Telecommunications Regulatory Authority (TRA) UAE";
            if (networkId <= 0x2320 && networkId <= 0x2320) return "DTT Uganda	Uganda Commuications Commission";
            if (networkId <= 0x233A && networkId <= 0x233A)
                return "UK Digital Terrestrial Television	Independent Television Commission ";
            if (networkId <= 0x2342 && networkId <= 0x2342)
                return "DTT Tanzania	Tanzania Communications Regulatory Authority";
            if (networkId <= 0x2B00 && networkId <= 0x2B00)
                return "DTT – Sky New Zealand	Sky Network Television Limited";
            if (networkId <= 0x3000 && networkId <= 0x3000) return "PanAmSat 4 68.5°E	Pan American Satellite System";
            if (networkId <= 0x3010 && networkId <= 0x3010) return "Grant Investrade Limited	Grant Investrade Limited";
            if (networkId <= 0x5000 && networkId <= 0x5000) return "Irdeto Mux System	Irdeto Test Laboratories";
            if (networkId <= 0x616D && networkId <= 0x616D)
                return "BellSouth Entertainment	BellSouth Entertainment, Atlanta, GA, USA  ";
            if (networkId <= 0x6600 && networkId <= 0x6600) return "UPC Satellite	UPC  ";
            if (networkId <= 0x6601 && networkId <= 0x6601) return "UPC Cable	UPC  ";
            if (networkId <= 0x6602 && networkId <= 0x6602) return "Comcast Media Center	Comcast Media Center";
            if (networkId <= 0xA011 && networkId <= 0xA011)
                return "Sichuan Cable TV Network	Sichuan Cable TV Network (PRC)";
            if (networkId <= 0xA012 && networkId <= 0xA012)
                return "China Network Systems	STAR Koos Finance Company (Taiwan)";
            if (networkId <= 0xA013 && networkId <= 0xA013) return "Versatel	Versatel (Russia)";
            if (networkId <= 0xA014 && networkId <= 0xA014) return "Chongqing Cable	Chongqing Municipality, PRC";
            if (networkId <= 0XA015 && networkId <= 0xA015) return "Guizhou Cable	Guizhou Province, PRC";
            if (networkId <= 0xA016 && networkId <= 0xA016) return "Hathway Cable	Hathway Cable and Datacom, India";
            if (networkId <= 0xA017 && networkId <= 0xA017) return "RCN	Rogers Cable Network, USA";
            if (networkId <= 0xA018 && networkId <= 0xA040) return "(NDS satellite services)	(NDS to be allocated)";
            if (networkId <= 0xA401 && networkId <= 0xA401) return "ARD	ARD-Sternpunkte";
            if (networkId <= 0xA509 && networkId <= 0xA509) return "Welho Cable Network Helsinki	Welho";
            if (networkId <= 0xA600 && networkId <= 0xA600) return "Madritel	Madritel (Spain)";
            if (networkId <= 0xA602 && networkId <= 0xA602) return "Tevel	Tevel (Israel) (NDS)";
            if (networkId <= 0xA603 && networkId <= 0xA603)
                return "Globo Cabo (to be recycled)	Globo Cabo (Brazil) (NDS)";
            if (networkId <= 0xA604 && networkId <= 0xA604) return "Cablemas (to be recycled)	Cablemas (Mexico) (NDS)";
            if (networkId <= 0xA605 && networkId <= 0xA605)
                return "INC National Cable Network	Information Network Centre of SARFT (PRC) (NDS)";
            if (networkId <= 0xA606 && networkId <= 0xA607) return "Pepcom GmbH	Pepcom GmbH";
            if (networkId <= 0xA751 && networkId <= 0xA751) return "Grant Investrade Limited	Grant Investrade Limited";
            if (networkId <= 0xA900 && networkId <= 0xA900)
                return "M-net Telekommunikations GmbH	M-net Telekommunikations GmbH";
            if (networkId <= 0xA910 && networkId <= 0xA910) return "TRICOM	TRICOM";
            if (networkId <= 0xF000 && networkId <= 0xF000)
                return "SMALL CABLE NETWORKS	(Small cable network network operators)";
            if (networkId <= 0xF001 && networkId <= 0xF001) return "Kabel Deutschland	Kabel Deutschland";
            if (networkId <= 0xF002 && networkId <= 0xF002) return "Deutsche Telekom AG	Deutsche Telekom AG";
            if (networkId <= 0xF010 && networkId <= 0xF010) return "Telefónica Cable	Telefónica Cable SA";
            if (networkId <= 0xF020 && networkId <= 0xF020)
                return "Cable and Wireless Communication	Cable and Wireless Communications";
            if (networkId <= 0xF100 && networkId <= 0xF100) return "Casema	Casema N.V .";
            if (networkId <= 0xF750 && networkId <= 0xF750)
                return "Telewest Communications Cable Network	Telewest Communications Plc  ";
            if (networkId <= 0xF751 && networkId <= 0xF751) return "OMNE Communications	OMNE Communications Ltd.";
            if (networkId <= 0xFBFC && networkId <= 0xFBFC) return "MATAV	MATAV (Israel ) (NDS)";
            if (networkId <= 0xFBFD && networkId <= 0xFBFD) return "Com Hem ab	Com Hem ab";
            if (networkId <= 0xFBFE && networkId <= 0xFBFE) return "TPS	La Télévision Par Satellite";
            if (networkId <= 0xFBFF && networkId <= 0xFBFF) return "Sky Italia	Sky Italia Spa.";
            if (networkId <= 0xFC10 && networkId <= 0xFC10) return "Rhône Vision Cable	Rhône Vision Cable";
            if (networkId <= 0xFC41 && networkId <= 0xFC41) return "France Telecom Cable	France Telecom  ";
            if (networkId <= 0xFD00 && networkId <= 0xFD00) return "National Cable Network	Lyonnaise Communications";
            if (networkId <= 0xFE00 && networkId <= 0xFE00) return "TeleDenmark Cable TV	TeleDenmark";
            if (networkId <= 0xFEC0 && networkId <= 0xFEFF) return "Network Interface Modules	Common Interface  ";
            if (networkId <= 0xFF00 && networkId <= 0xFFFA) return "Private_temporary_use	ETSI";

            return "Undefined";
        }
    }
}