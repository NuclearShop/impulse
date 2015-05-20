var server = server || {};
/// <summary>The ABTest class as defined in ImpulseApp.Models.AdModels.ABTest</summary>
server.ABTest = function() {
	/// <field name="Id" type="Number">The Id property as defined in ImpulseApp.Models.AdModels.ABTest</field>
	this.Id = 0;
	/// <field name="AdAId" type="Number">The AdAId property as defined in ImpulseApp.Models.AdModels.ABTest</field>
	this.AdAId = 0;
	/// <field name="AdBId" type="Number">The AdBId property as defined in ImpulseApp.Models.AdModels.ABTest</field>
	this.AdBId = 0;
	/// <field name="DateStart" type="Date">The DateStart property as defined in ImpulseApp.Models.AdModels.ABTest</field>
	this.DateStart = new Date();
	/// <field name="DateEnd" type="Date">The DateEnd property as defined in ImpulseApp.Models.AdModels.ABTest</field>
	this.DateEnd = new Date();
	/// <field name="ChangeHours" type="Number">The ChangeHours property as defined in ImpulseApp.Models.AdModels.ABTest</field>
	this.ChangeHours = 0;
	/// <field name="ChangeCount" type="Number">The ChangeCount property as defined in ImpulseApp.Models.AdModels.ABTest</field>
	this.ChangeCount = 0;
	/// <field name="ActiveAd" type="Number">The ActiveAd property as defined in ImpulseApp.Models.AdModels.ABTest</field>
	this.ActiveAd = 0;
	/// <field name="Url" type="String">The Url property as defined in ImpulseApp.Models.AdModels.ABTest</field>
	this.Url = '';
	/// <field name="AdA" type="Object">The AdA property as defined in ImpulseApp.Models.AdModels.ABTest</field>
	this.AdA = { };
	/// <field name="AdB" type="Object">The AdB property as defined in ImpulseApp.Models.AdModels.ABTest</field>
	this.AdB = { };
};

