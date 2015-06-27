using ImpulseApp.Models.Utilites;
using ImpulseApp.Outbound.ContractTypes;
using ImpulseApp.Outbound.DBService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ImpulseApp.Outbound
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class OutboundService : IOutboundService
    {
        IDBService db = new DBServiceClient();
        [ReferencePreservingDataContractFormatAttribute]
        VMAP IOutboundService.GetVMAP(int id)
        {
            ImpulseApp.Models.AdModels.SimpleAdModel adGBO = db.GetAdById(id);


            VMAP vmap = new VMAP();
            VMAPAdBreak vmapAd = new VMAPAdBreak();
            VMAPAdBreakAdSource adSource = new VMAPAdBreakAdSource();
            List<VMAPAdBreakAdSourceVASTAdData> ads = new List<VMAPAdBreakAdSourceVASTAdData>();
            foreach (var state in adGBO.AdStates)
            {
                VMAPAdBreakAdSourceVASTAdData vast = new VMAPAdBreakAdSourceVASTAdData();
                vast.VAST = GetVastFromAdState(state);
                ads.Add(vast);
            }
            adSource.VASTAdData = ads.ToArray();
            adSource.allowMultipleAds = true;
            adSource.followRedirects = true;
            vmapAd.AdSource = new VMAPAdBreakAdSource[1];
            vmapAd.AdSource[0] = adSource;
            vmap.AdBreak = new VMAPAdBreak[1];
            vmap.AdBreak[0] = vmapAd;
            return vmap;
        }

        VAST GetVastFromAdState(Models.AdModels.AdState AdState)
        {
            VAST v = new VAST();
            VASTAD ad = new VASTAD();
            ad.id = AdState.Id.ToString();
            VASTADInLine inline = new VASTADInLine();
            v.version = "1.0";
            inline.AdTitle = AdState.Name;
            inline.Description = "Impulse ad";
            inline.Creatives = new VASTADInLineCreative[AdState.UserElements.Count];
            List<VASTADInLineCreative> creativeList = new List<VASTADInLineCreative>();
            foreach (var elem in AdState.UserElements)
            {
                VASTADInLineCreative creative = new VASTADInLineCreative();
                creative.sequence = "1";
                creative.AdID = AdState.AdId.ToString();
                NonLinear_type nonlinearAd = new NonLinear_type();
                NonLinear_typeStaticResource resource = new NonLinear_typeStaticResource();
                nonlinearAd.height = elem.Height;
                nonlinearAd.width = elem.Width;
                nonlinearAd.apiFramework = "mpls-framework";
                resource.creativeType = "image/png";
                resource.Value = elem.Text;
                //resource.Value = "<![CDATA[<div style='"+elem.HtmlStyle+"'>" + elem.Text + "</div>]]>";
                creativeList.Add(creative);
            }
            inline.Creatives = creativeList.ToArray();
            Impression_type impression = new Impression_type();
            impression.id = AdState.VideoUnitId.ToString();
            impression.Value = AdState.VideoUnit.FullPath;
            //impression.Value = "<![CDATA[" + AdState.VideoUnit.FullPath + "]]>";
            inline.Impression = new Impression_type[1];
            inline.Impression[0] = impression;
            ad.Item = inline;
            v.Ad = new VASTAD[1];
            v.Ad[0] = ad;
            return v;
        }
    }
}
