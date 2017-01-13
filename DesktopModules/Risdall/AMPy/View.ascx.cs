#region Copyright

// 
// Copyright (c) 2017
// by Risdall Marketing Group
// 

/*
TODO
=============
1. Finish removing wrapping divs for content. div class="DnnModule" class="DNNContainer_noTitle"
2. Remove empty <div class="clear"></div>
3. Auto size & height images based on style tags
4. Add AMP nav, logo
5. CSS styling - Create an AMP CSS file. Place in portal root?? Read file and import into doc

6. Module Settings
--URL override
--Module chooser
--Other AMP features per module maybe (https://ampbyexample.com/#components)
>> Accordion
>> light box
>> iframe
>> analytics

7. Portal Settings
--CSS, templating
-- amp font

8. Build settings into DNN 9 toolbar

 */



#endregion

#region Using Statements

using System;
using DotNetNuke.Entities.Modules;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.IO;
using System.Text;
using System.Net;
using System.Linq;
#endregion

namespace Risdall.AMPy
{

	public partial class View : PortalModuleBase
	{

		#region Event Handlers

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			
			if (!Page.IsPostBack)
			{
                           
			}
		}
		
		protected void cmdSave_Click(object sender, EventArgs e)
		{
            HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.OptionFixNestedTags = true;

            //Get current page URL
            string url = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.RawUrl;
            //url = "http://www.tolomatic.com/products/product-details/pb-pneumatic-linear-thruster#/";

            //Pretend we are a browser
            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:31.0) Gecko/20100101 Firefox/31.0";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-us,en;q=0.5");
            //Load page
            WebResponse response = request.GetResponse();
            htmlDoc.Load(response.GetResponseStream(), true);

            if (htmlDoc.DocumentNode != null)
            {
                //Get content pane
                var content = htmlDoc.DocumentNode
                                    .SelectSingleNode("//div[@id='dnn_ContentPane']");
                content.ParentNode.RemoveChild(content, true); //<--remove content div but keep inner

                string parsedHTML = GoogleAmpConverter.Convert(content.OuterHtml);
                
                var sbNewAMPPage = new StringBuilder();
                //grab template for start of document
                sbNewAMPPage.Append(File.ReadAllText(Server.MapPath("~/DesktopModules/Risdall/AMPy/templates/start.html")));
                //token replace for template
                sbNewAMPPage.Replace("[ItemUrl]", url);


                sbNewAMPPage.Append(parsedHTML);
                sbNewAMPPage.Append("</body></html>");

                string strFileName = HttpContext.Current.Server.MapPath("~/AMP/amptest");
                strFileName = strFileName + "_" + DateTime.Now.ToString("yyyyMMdd") + ".html";
                FileStream fs = new FileStream(strFileName, FileMode.Create);
                StreamWriter writer = new StreamWriter(fs, Encoding.UTF8);
                writer.Write(sbNewAMPPage.ToString());
                writer.Close();
            }

        }

       #endregion

    }
}

