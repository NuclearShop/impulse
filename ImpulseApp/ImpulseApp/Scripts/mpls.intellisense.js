var server = server || {};
/// <summary>The ABTest class as defined in ImpulseApp.Models.AdModels.ABTest</summary>
ABTest = function () {
    /// <field name="Id" type="Number">The Id property as defined in ImpulseApp.Models.AdModels.ABTest</field>
    this.Id = 0;
    /// <field name="AdAId" type="Number">The AdAId property as defined in ImpulseApp.Models.AdModels.ABTest</field>
    this.AdAId = 0;
    /// <field name="AdBId" type="Number">The AdBId property as defined in ImpulseApp.Models.AdModels.ABTest</field>
    this.AdBId = 0;
    /// <field name="DateStart" type="Date">The DateStart property as defined in ImpulseApp.Models.AdModels.ABTest</field>
    this.DateStart = new Date();
    /// <field name="ChangeHours" type="Number">The ChangeHours property as defined in ImpulseApp.Models.AdModels.ABTest</field>
    this.ChangeHours = 0;
    /// <field name="ChangeCount" type="Number">The ChangeCount property as defined in ImpulseApp.Models.AdModels.ABTest</field>
    this.ChangeCount = 0;
    /// <field name="ActiveAd" type="Number">The ActiveAd property as defined in ImpulseApp.Models.AdModels.ABTest</field>
    this.ActiveAd = 0;
    /// <field name="Url" type="String">The Url property as defined in ImpulseApp.Models.AdModels.ABTest</field>
    this.Url = '';
};

/// <summary>The SimpleAdModelDTO class as defined in ImpulseApp.Models.DTO.SimpleAdModelDTO</summary>
SimpleAdModelDTO = function () {
    /// <field name="Id" type="Number">The Id property as defined in ImpulseApp.Models.DTO.SimpleAdModelDTO</field>
    this.Id = 0;
    /// <field name="Name" type="String">The Name property as defined in ImpulseApp.Models.DTO.SimpleAdModelDTO</field>
    this.Name = '';
    /// <field name="HtmlStartSource" type="String">The HtmlStartSource property as defined in ImpulseApp.Models.DTO.SimpleAdModelDTO</field>
    this.HtmlStartSource = '';
    /// <field name="HtmlEndSource" type="String">The HtmlEndSource property as defined in ImpulseApp.Models.DTO.SimpleAdModelDTO</field>
    this.HtmlEndSource = '';
    /// <field name="HtmlSource" type="String">The HtmlSource property as defined in ImpulseApp.Models.DTO.SimpleAdModelDTO</field>
    this.HtmlSource = '';
    /// <field name="ShortUrlKey" type="String">The ShortUrlKey property as defined in ImpulseApp.Models.DTO.SimpleAdModelDTO</field>
    this.ShortUrlKey = '';
    /// <field name="DateTime" type="Date">The DateTime property as defined in ImpulseApp.Models.DTO.SimpleAdModelDTO</field>
    this.DateTime = new Date();
    /// <field name="IsRoot" type="Boolean">The IsRoot property as defined in ImpulseApp.Models.DTO.SimpleAdModelDTO</field>
    this.IsRoot = false;
    /// <field name="IsActive" type="Boolean">The IsActive property as defined in ImpulseApp.Models.DTO.SimpleAdModelDTO</field>
    this.IsActive = false;
    /// <field name="Videos" type="Object[]">The Videos property as defined in ImpulseApp.Models.DTO.SimpleAdModelDTO</field>
    this.Videos = [];
    /// <field name="StateGraph" type="Object[]">The StateGraph property as defined in ImpulseApp.Models.DTO.SimpleAdModelDTO</field>
    this.StateGraph = [];
};



AdStateDTO = function () {
    /// <field name="Id" type="Number">The Id property as defined in ImpulseApp.Models.DTO.AdStateDTO</field>
    this.Id = 0;
    /// <field name="EndTime" type="Number">The EndTime property as defined in ImpulseApp.Models.DTO.AdStateDTO</field>
    this.EndTime = 0;
    /// <field name="IsFullPlay" type="Boolean">The IsFullPlay property as defined in ImpulseApp.Models.DTO.AdStateDTO</field>
    this.IsFullPlay = false;
    /// <field name="Name" type="String">The Name property as defined in ImpulseApp.Models.DTO.AdStateDTO</field>
    this.Name = '';
    /// <field name="ChainedHtml" type="String">The ChainedHtml property as defined in ImpulseApp.Models.DTO.AdStateDTO</field>
    this.ChainedHtml = '';
    /// <field name="FullPath" type="String">The FullPath property as defined in ImpulseApp.Models.DTO.AdStateDTO</field>
    this.FullPath = '';
    /// <field name="MimeType" type="String">The MimeType property as defined in ImpulseApp.Models.DTO.AdStateDTO</field>
    this.MimeType = '';
    /// <field name="IsStart" type="Boolean">The IsStart property as defined in ImpulseApp.Models.DTO.AdStateDTO</field>
    this.IsStart = false;
    /// <field name="IsEnd" type="Boolean">The IsEnd property as defined in ImpulseApp.Models.DTO.AdStateDTO</field>
    this.IsEnd = false;
    /// <field name="VideoUnitId" type="Number">The VideoUnitId property as defined in ImpulseApp.Models.DTO.AdStateDTO</field>
    this.VideoUnitId = 0;
    /// <field name="DefaultNext" type="Number">The DefaultNext property as defined in ImpulseApp.Models.DTO.AdStateDTO</field>
    this.DefaultNext = 0;
    /// <field name="DefaultNextTime" type="Number">The DefaultNextTime property as defined in ImpulseApp.Models.DTO.AdStateDTO</field>
    this.DefaultNextTime = 0;
    /// <field name="UserElements" type="Object[]">The UserElements property as defined in ImpulseApp.Models.DTO.AdStateDTO</field>
    this.UserElements = [];
};


/// <summary>The UserElementDTO class as defined in ImpulseApp.Models.DTO.UserElementDTO</summary>
UserElementDTO = function () {
    /// <field name="Id" type="Number">The Id property as defined in ImpulseApp.Models.DTO.UserElementDTO</field>
    this.Id = 0;
    /// <field name="HtmlId" type="String">The HtmlId property as defined in ImpulseApp.Models.DTO.UserElementDTO</field>
    this.HtmlId = '';
    /// <field name="HtmlClass" type="String">The HtmlClass property as defined in ImpulseApp.Models.DTO.UserElementDTO</field>
    this.HtmlClass = '';
    /// <field name="HtmlType" type="String">The HtmlType property as defined in ImpulseApp.Models.DTO.UserElementDTO</field>
    this.HtmlType = '';
    /// <field name="UseDefaultStyle" type="Boolean">The UseDefaultStyle property as defined in ImpulseApp.Models.DTO.UserElementDTO</field>
    this.UseDefaultStyle = false;
    /// <field name="HtmlStyle" type="String">The HtmlStyle property as defined in ImpulseApp.Models.DTO.UserElementDTO</field>
    this.HtmlStyle = '';
    /// <field name="Text" type="String">The Text property as defined in ImpulseApp.Models.DTO.UserElementDTO</field>
    this.Text = '';
    /// <field name="X" type="Number">The X property as defined in ImpulseApp.Models.DTO.UserElementDTO</field>
    this.X = 0;
    /// <field name="Y" type="Number">The Y property as defined in ImpulseApp.Models.DTO.UserElementDTO</field>
    this.Y = 0;
    /// <field name="Width" type="String">The Width property as defined in ImpulseApp.Models.DTO.UserElementDTO</field>
    this.Width = '';
    /// <field name="Height" type="String">The Height property as defined in ImpulseApp.Models.DTO.UserElementDTO</field>
    this.Height = '';
    /// <field name="Action" type="String">The Action property as defined in ImpulseApp.Models.DTO.UserElementDTO</field>
    this.Action = '';
    /// <field name="TimeAppear" type="Number">The TimeAppear property as defined in ImpulseApp.Models.DTO.UserElementDTO</field>
    this.TimeAppear = 0;
    /// <field name="TimeDisappear" type="Number">The TimeDisappear property as defined in ImpulseApp.Models.DTO.UserElementDTO</field>
    this.TimeDisappear = 0;
    /// <field name="CurrentId" type="Number">The CurrentId property as defined in ImpulseApp.Models.DTO.UserElementDTO</field>
    this.CurrentId = 0;
    /// <field name="NextId" type="Number">The NextId property as defined in ImpulseApp.Models.DTO.UserElementDTO</field>
    this.NextId = 0;
    /// <field name="NextTime" type="Number">The NextTime property as defined in ImpulseApp.Models.DTO.UserElementDTO</field>
    this.NextTime = 0;
    /// <field name="FormName" type="String">The FormName property as defined in ImpulseApp.Models.DTO.UserElementDTO</field>
    this.FormName = '';
    /// <field name="HtmlTags" type="Object[]">The HtmlTags property as defined in ImpulseApp.Models.DTO.UserElementDTO</field>
    this.HtmlTags = [];
};

/// <summary>The HtmlTagDTO class as defined in ImpulseApp.Models.DTO.HtmlTagDTO</summary>
HtmlTagDTO = function () {
    /// <field name="Key" type="String">The Key property as defined in ImpulseApp.Models.DTO.HtmlTagDTO</field>
    this.Key = '';
    /// <field name="Value" type="String">The Value property as defined in ImpulseApp.Models.DTO.HtmlTagDTO</field>
    this.Value = '';
};


NodeLinkDTO = function () {
    /// <field name="V1" type="Number">The V1 property as defined in ImpulseApp.Models.DTO.NodeLinkDTO</field>
    this.V1 = 0;
    /// <field name="V2" type="Number">The V2 property as defined in ImpulseApp.Models.DTO.NodeLinkDTO</field>
    this.V2 = 0;
    /// <field name="T" type="Number">The T property as defined in ImpulseApp.Models.DTO.NodeLinkDTO</field>
    this.T = 0;
};
