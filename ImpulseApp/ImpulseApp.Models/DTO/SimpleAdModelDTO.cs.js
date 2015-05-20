var server = server || {};
/// <summary>The SimpleAdModelDTO class as defined in ImpulseApp.Models.DTO.SimpleAdModelDTO</summary>
server.SimpleAdModelDTO = function() {
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
	/// <field name="AdStates" type="Object[]">The AdStates property as defined in ImpulseApp.Models.DTO.SimpleAdModelDTO</field>
	this.AdStates = [];
	/// <field name="StateGraph" type="Object[]">The StateGraph property as defined in ImpulseApp.Models.DTO.SimpleAdModelDTO</field>
	this.StateGraph = [];
};

