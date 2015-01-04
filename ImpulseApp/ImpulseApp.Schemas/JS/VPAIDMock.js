function VPAIDMock() { };


VPAIDMock.prototype.viewMode = "";
VPAIDMock.prototype.desiredBitrate = 0;


//fields
VPAIDMock.prototype.adWidth = 0;
VPAIDMock.prototype.adHeight = 0;
VPAIDMock.prototype.adLinear = true;
VPAIDMock.prototype.adExpanded = false;
VPAIDMock.prototype.adSkippableState = false;
VPAIDMock.prototype.adRemainingTime = new Date();
VPAIDMock.prototype.adDuration = new Date();
VPAIDMock.prototype.adCompanions = "";
VPAIDMock.prototype.adVolume = 0.0;
VPAIDMock.prototype.adIcons = false;


//Getters and Setters

VPAIDMock.prototype.getAdWidth = function () {
    return this.adWidth;
};
VPAIDMock.prototype.getAdHeight = function () {
    return this.adHeight;
};
VPAIDMock.prototype.getAdExpanded = function () {
    return this.adExpanded;
};
VPAIDMock.prototype.getAdSkippableState = function () {
    return this.adSkippableState;
};
VPAIDMock.prototype.getAdRemainingTime = function () {
    return this.adRemainingTime;
};
VPAIDMock.prototype.getAdDuration = function () {
    return this.adDuration;
};
VPAIDMock.prototype.getAdCompanions = function () {
    return this.adCompanions;
};
VPAIDMock.prototype.getAdVolume = function () {
    return this.adVolume;
};
VPAIDMock.prototype.setAdVolume = function (val) {
    this.adVolume = val;
};
VPAIDMock.prototype.getRemainingRime = function () {
    return this.adIcons;
};


//functions
VPAIDMock.prototype.handshakeVersion = function (version) {
    return version;
};

VPAIDMock.prototype.initAd = function (width, height, viewMode, desiredBitrate, creativeData, environmentVars) {
    this.adWidth = width;
    this.adHeight = height;
    this.viewMode = viewMode;
    this.desiredBitrate = desiredBitrate;
    this._slot = environmentVars.slot;
    this._videoSlot = environmentVars.videoSlot;
    this.initCallback("AdLoaded");
};

VPAIDMock.prototype.startAd = function () { };

VPAIDMock.prototype.stopAd = function () { };

VPAIDMock.prototype.resizeAd = function (width, height, viewMode) {
    this.adWidth = width;
    this.adHeight = height;
    this.viewMode = viewMode;
};
VPAIDMock.prototype.pauseAd = function () { };
VPAIDMock.prototype.resumeAd = function () { };
VPAIDMock.prototype.expandAd = function () { };
VPAIDMock.prototype.collapseAd = function () { };
VPAIDMock.prototype.skipAd = function () { };

//EventListener

VPAIDMock.prototype.AdStarted = function () { };
VPAIDMock.prototype.AdStopped = function () { };
VPAIDMock.prototype.AdSkipped = function () { };
VPAIDMock.prototype.AdLoaded = function () { };
VPAIDMock.prototype.AdLinearChange = function () { };
VPAIDMock.prototype.AdSizeChange = function () { };
VPAIDMock.prototype.AdExpandedChange = function () { };
VPAIDMock.prototype.AdSkippableStateChange = function () { };
VPAIDMock.prototype.AdDurationChange = function () { };
VPAIDMock.prototype.AdRemainingTimeChange = function () { };
VPAIDMock.prototype.AdVolumeChange = function () { };
VPAIDMock.prototype.AdImpression = function () { };
VPAIDMock.prototype.AdClickThru = function () { };
VPAIDMock.prototype.AdInteraction = function () { };
VPAIDMock.prototype.AdVideoStart = function () { };
VPAIDMock.prototype.AdVideoFirstQuartile = function () { };
VPAIDMock.prototype.AdVideoMidpoint = function () { };
VPAIDMock.prototype.AdVideoThirdQuartile = function () { };
VPAIDMock.prototype.AdVideoComplete = function () { };
VPAIDMock.prototype.AdUserAcceptInvitation = function () { };
VPAIDMock.prototype.AdUserMinimize = function () { };
VPAIDMock.prototype.AdUserClose = function () { };
VPAIDMock.prototype.AdPaused = function () { };
VPAIDMock.prototype.AdPlaying = function () { };
VPAIDMock.prototype.AdError = function () { };
VPAIDMock.prototype.AdLog = function () { };

VPAIDMock.prototype.callbacks = {
    AdStarted: null,
    AdStopped: null,
    AdSkipped: null,
    AdLoaded: null,
    AdLinearChange: null,
    AdSizeChange: null,
    AdExpandedChange: null,
    AdSkippableStateChange: null,
    AdDurationChange: null,
    AdRemainingTimeChange: null,
    AdVolumeChange: null,
    AdImpression: null,
    AdClickThru: null,
    AdInteraction: null,
    AdVideoStart: null,
    AdVideoFirstQuartile: null,
    AdVideoMidpoint: null,
    AdVideoThirdQuartile: null,
    AdVideoComplete: null,
    AdUserAcceptInvitation: null,
    AdUserMinimize: null,
    AdUserClose: null,
    AdPaused: null,
    AdPlaying: null,
    AdError: null,
    AdLog: null
};

VPAIDMock.prototype.initCallback = function (name) {
    var func = VPAIDMock.prototype[name];
    var callbackFunc = VPAIDMock.prototype.callbacks[name];
    func();
    if (callbackFunc !== null) {
        callbackFunc();
    }
};



VPAIDMock.prototype.subscribe = function (Callback, eventName, Context) {
    this.callbacks[eventName] = Callback;
};
VPAIDMock.prototype.unsubscribe = function (eventName) {
    this.callbacks[eventName] = null;
};

//Main function
getVPAIDAd = function () {
    return new VPAIDMock();
};

